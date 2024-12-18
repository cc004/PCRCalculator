﻿using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using AssetsTools.LZ4;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.OpenSsl;

namespace LibConeshell
{
    public class ConeshellV2 : Coneshell
    {
        public X25519PublicKeyParameters? ServerPublicKey { get; set; }

        public ConeshellV2(byte[] deviceUdid, X25519PublicKeyParameters? serverPublicKey = null)
        {
            ServerPublicKey = serverPublicKey;
        }

        public ConeshellV2()
        {
        }

        #region Coneshell VFS Functions

        protected const ulong TransformConstant = 0x5851F42D4C957F2D;
        protected virtual uint VfsHeaderMagic => 0x02007ADA;

        private static readonly uint[] VfsCertConstants = {
            0xA6D3137Cu, 0xAA02BB19u, 0xEF3635A3u, 0xA5582A32u, 0x542D973Eu, 0x815A36DFu, 0xCD785F45u, 0xAC83658Au,
            0x42BFAE14u, 0x08614929u, 0x86133A68u, 0x89D7C415u, 0xB8B303ACu, 0x6E7CEEB5u, 0x9DC7367Bu, 0x3579BB20u,
            0x061C9807u, 0x2C13FFCDu, 0xBE449765u, 0x6D9118F3u, 0x950CA8B2u, 0x99F8FC64u, 0x4D7F0584u, 0x0D67A7E6u,
            0x39CDB4A5u, 0x1E408A1Du, 0x3C02AEBDu, 0xD686B9ACu, 0x2E22883Bu, 0xB45CD96Bu, 0xA03A1F1Au, 0x6473AA4Eu,
            0x4439AFE6u, 0x49BC8398u, 0xD1ADE5FEu, 0x2EB04BE2u, 0x88D94B0Fu, 0x7F4CF164u, 0xE4D8A217u, 0x859B9087u,
            0xA397DC52u, 0x7ACF276Eu, 0x96AB85E7u, 0x023530A4u, 0xFEE6AC6Bu, 0x80F2A6FEu, 0xC89F2D8Fu, 0x8115C9A0u,
            0xD9859AE8u, 0x4ABC8347u, 0xF165888Fu, 0x8501B547u, 0x5BAE6F91u, 0xEEF88E91u, 0xB6D92571u, 0x68F361E8u,
            0x1A6195B0u, 0xFA739F4Bu, 0x5E939B01u, 0xCE112E18u, 0x267828E5u, 0x55FB6252u, 0xE822DFB7u, 0x6B9B7B54u,
            0xD2BA1EEBu, 0x4B737FD0u, 0x184FB715u, 0x3C430912u, 0x9E33A9EDu, 0x12897909u, 0xBC2A4AEEu, 0x953F3ABBu,
            0x5E7136D5u, 0x5187BDDBu, 0xAC5430E8u, 0x03CD645Du, 0xACBA200Cu, 0xE1E3EDC1u, 0x077DB1ACu, 0x91E0F0B8u,
            0x913F230Du, 0x411ADB08u, 0x001F7A6Eu, 0x2F4C3C66u, 0x48B4676Du, 0x6D0DAE76u, 0x48AD72A8u, 0x32224BF7u,
            0x4D553DCDu, 0x35CE98F2u, 0x88B8C1FAu, 0xFA957EC6u, 0x76C98E88u, 0x47505B5Au, 0x31B0D018u, 0x97B2F4E5u,
            0xF9DCEF6Fu, 0x99CB24BEu, 0xEBBE76B2u, 0x30B9953Bu, 0x07F95F8Cu, 0x75813038u, 0xB556CDF4u, 0x0C5F9555u,
            0xACAA0380u, 0x9A53A8B6u, 0xE85EE132u, 0xFEED78E4u, 0x15497261u, 0x36CA63E6u, 0x47ECDEA1u, 0x912D20C4u,
            0x1C3AF78Eu
        };

        public virtual byte[] DecryptVfs(byte[] dbData, bool skipVerification = false)
        {
            var inputStream = new MemoryStream(dbData);
            var inputReader = new BinaryReader(inputStream);

            if (inputReader.ReadUInt32() != VfsHeaderMagic)
                throw new IOException("Invalid database header.");

            return DecryptVfsInternal(dbData, inputReader, skipVerification, !skipVerification ? DeriveVfsPublicKey(VfsCertConstants) : "");
        }

        protected static byte[] DecryptVfsInternal(byte[] dbData, BinaryReader inputReader, bool skipVerification, string publicKey = "", int headerOffset = 0)
        {
            const int fullHeaderSize = 0x4 + 0x4 + 0x10 + 0x10 + 0x4 + 0x10 + 0x100;
            var headerSize = fullHeaderSize - headerOffset;

            if (dbData.Length < headerSize)
                throw new IOException("Encrypted database too short.");

            var gcmAdd1 = inputReader.ReadUInt32();
            var gcmKey = inputReader.ReadBytes(0x10);
            var gcmIv = inputReader.ReadBytes(0x10);
            var gcmAdd2 = inputReader.ReadUInt32();
            var gcmTag = inputReader.ReadBytes(0x10);
            var signature = inputReader.ReadBytes(0x100);

            var gcmAdd = BitConverter.GetBytes(gcmAdd1).Concat(BitConverter.GetBytes(gcmAdd2)).ToArray();

            var encryptedLength = dbData.Length - headerSize;
            var encryptedData = new byte[encryptedLength + gcmTag.Length];
            if (inputReader.Read(encryptedData, 0, encryptedLength) != encryptedLength)
                throw new IOException("Failed to read encrypted data from database.");

            Buffer.BlockCopy(gcmTag, 0, encryptedData, encryptedLength, gcmTag.Length);

            inputReader.Dispose();

            if (!skipVerification)
            {
                /*
                var signedData = gcmTag.Concat(BitConverter.GetBytes(encryptedLength - 4)).ToArray();
    
    
                var rsa = RSA.Create();
                rsa.ImportFromPem(publicKey);
                var sigResult = rsa.VerifyHash(signedData, signature, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
                if (!sigResult)
                    throw new CryptographicException("Failed to verify VFS signature.");*/
            }

            var gcm = new GcmBlockCipher(new AesEngine());
            gcm.Init(false, new AeadParameters(new KeyParameter(gcmKey), gcmTag.Length * 8, gcmIv, gcmAdd));

            var decryptedData = new byte[gcm.GetOutputSize(encryptedData.Length)];

            try
            {
                var decryptedLen = gcm.ProcessBytes(encryptedData, 0, encryptedData.Length, decryptedData, 0);
                gcm.DoFinal(decryptedData, decryptedLen); // This already verifies the tag for us
            }
            catch (Exception ex)
            {
                throw new CryptographicException($"Failed to decrypt database: {ex.Message}");
            }

            var decompressedLength = decryptedData[0] | (decryptedData[1] << 8) | (decryptedData[2] << 16) | (decryptedData[3] << 24);
            var bodyData = decryptedData.Skip(4).ToArray();

            return decompressedLength != 0
                ? Decode(bodyData, decompressedLength)
                : bodyData;
        }

        private static byte[] Decode(byte[] compressed, int decompressedLen)
        {
            var result = new byte[decompressedLen];
            LZ4Codec.Decode(compressed, 0, compressed.Length, result, 0, result.Length);
            return result;
        }

        protected static string DeriveVfsPublicKey(uint[] encCert, ulong seed = 0x8BE53A46A921AF07, ulong add = 0x31D7038E3C2AB8B)
        {
            var round = seed;
            var result = new byte[0x1c4];
            using var outputStream = new MemoryStream(result);
            using var outputWriter = new BinaryWriter(outputStream);

            unchecked
            {
                for (int i = 0; i < 0x1c4 / 4; i++)
                {
                    var rk = encCert[i];
                    var rg = (round ^ (round >> 18)) >> 27;
                    var rv = BitOperations.RotateRight((uint)rg, (int)(round >> 59)) ^ rk;
                    outputWriter.Write(rv);
                    round = TransformConstant * round + add;
                }
            }

            return Encoding.UTF8.GetString(result.Reverse().ToArray());
        }

        #endregion
    }
    public static class BitOperations
    {
        public static uint RotateRight(uint value, int offset)
            => (value >> offset) | (value << (32 - offset));
    }
}