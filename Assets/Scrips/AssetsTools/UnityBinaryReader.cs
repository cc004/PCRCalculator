﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AssetsTools {
    public class UnityBinaryReader {
        byte[] file = null;
        int offset = 0;
        int bound = 0;
        int start = 0;

        public int Position {
            get {
                return offset - start;
            }
            set {
                int num = value + start;
                if (bound < num) {
                    throw new IndexOutOfRangeException();
                }
                offset = num;
            }
        }

        #region Ctor
        public UnityBinaryReader(string filename) {
            CheckEndianness();
            start = 0;
            using (FileStream fileStream = new FileStream(filename, FileMode.Open)) {
                file = new byte[new FileInfo(filename).Length];
                bound = fileStream.Read(file, 0, file.Length);
                offset = 0;
            }
        }

        public UnityBinaryReader(byte[] bin) {
            CheckEndianness();
            file = bin ?? throw new NullReferenceException("bin");
            offset = 0;
            start = 0;
            bound = bin.Length;
        }

        public UnityBinaryReader(byte[] bin, int offset, int length) {
            CheckEndianness();
            file = bin ?? throw new NullReferenceException("bin");
            int num = bin.Length;
            if (num <= offset || offset < 0) {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (num < length + offset || length < 0) {
                throw new ArgumentOutOfRangeException("length");
            }
            this.offset = offset;
            start = offset;
            bound = length + offset;
        }

        private static void CheckEndianness() {
            if (!BitConverter.IsLittleEndian) {
                throw new NotSupportedException("BigEndian platform is not supported");
            }
        }
        #endregion

        #region Slice
        public UnityBinaryReader Slice(int offset, int length) {
            return new UnityBinaryReader(file, offset, length);
        }

        public UnityBinaryReader Slice(int offset) {
            return new UnityBinaryReader(file, offset, bound - offset);
        }
        #endregion

        #region Byte
        public byte ReadByte() {
            byte result = file[offset];
            offset += 1;
            return result;
        }

        public sbyte ReadSByte() {
            byte result = file[offset];
            offset += 1;
            return (sbyte)result;
        }

        public bool ReadBool() {
            bool result = file[offset] != 0;
            offset += 1;
            return result;
        }
        #endregion

        #region LittleEndian
        public unsafe short ReadShort() {
            if (bound < offset + 2) {
                throw new IndexOutOfRangeException();
            }
            fixed (byte* p = &file[offset]) {
                offset += 2;
                return *(short*)p;
            }
        }

        public unsafe int ReadInt() {
            if (bound < offset + 4) {
                throw new IndexOutOfRangeException();
            }
            fixed (byte* p = &file[offset]) {
                offset += 4;
                return *(int*)p;
            }
        }

        public unsafe long ReadLong() {
            if (bound < offset + 8) {
                throw new IndexOutOfRangeException();
            }
            fixed (byte* p = &file[offset]) {
                offset += 8;
                return *(long*)p;
            }
        }

        public unsafe float ReadFloat() {
            if (bound < offset + 4) {
                throw new IndexOutOfRangeException();
            }
            fixed (byte* p = &file[offset]) {
                offset += 4;
                return *(float*)p;
            }
        }

        public unsafe double ReadDouble() {
            if (bound < offset + 8) {
                throw new IndexOutOfRangeException();
            }
            fixed (byte* p = &file[offset]) {
                offset += 8;
                return *(double*)p;
            }
        }

        public unsafe ushort ReadUShort() {
            if (bound < offset + 2) {
                throw new IndexOutOfRangeException();
            }
            fixed (byte* p = &file[offset]) {
                offset += 2;
                return *(ushort*)p;
            }
        }

        public unsafe uint ReadUInt() {
            if (bound < offset + 4) {
                throw new IndexOutOfRangeException();
            }
            fixed (byte* p = &file[offset]) {
                offset += 4;
                return *(uint*)p;
            }
        }

        public unsafe ulong ReadULong() {
            if (bound < offset + 8) {
                throw new IndexOutOfRangeException();
            }
            fixed (byte* p = &file[offset]) {
                offset += 8;
                return *(ulong*)p;
            }
        }
        #endregion

        #region BigEndian
        public unsafe short ReadShortBE() {
            if (bound < offset + 2) {
                throw new IndexOutOfRangeException();
            }
            short result = default(short);
            fixed (byte* q = &file[offset]) {
                byte* p = (byte*)&result;
                p[0] = q[1];
                p[1] = q[0];
                offset +=  2;
                return result;
            }
        }

        public unsafe int ReadIntBE() {
            if (bound < offset + 4) {
                throw new IndexOutOfRangeException();
            }
            int result = default(int);
            fixed (byte* q = &file[offset]) {
                byte* p = (byte*)&result;
                p[0] = q[3];
                p[1] = q[2];
                p[2] = q[1];
                p[3] = q[0];
                offset += 4;
                return result;
            }
        }

        public unsafe long ReadLongBE() {
            if (bound < offset + 8) {
                throw new IndexOutOfRangeException();
            }
            long result = default(long);
            fixed (byte* q = &file[offset]) {
                byte* p = (byte*)&result;
                p[0] = q[7];
                p[1] = q[6];
                p[2] = q[5];
                p[3] = q[4];
                p[4] = q[3];
                p[5] = q[2];
                p[6] = q[1];
                p[7] = q[0];
                offset += 8;
                return result;
            }
        }

        public unsafe float ReadFloatBE() {
            if (bound < offset + 4) {
                throw new IndexOutOfRangeException();
            }
            float result = default(float);
            fixed (byte* q = &file[offset]) {
                byte* p = (byte*)&result;
                p[0] = q[3];
                p[1] = q[2];
                p[2] = q[1];
                p[3] = q[0];
                offset += 4;
                return result;
            }
        }

        public unsafe double ReadDoubleBE() {
            if (bound < offset + 8) {
                throw new IndexOutOfRangeException();
            }
            double result = default(double);
            fixed (byte* q = &file[offset]) {
                byte* p = (byte*)&result;
                p[0] = q[7];
                p[1] = q[6];
                p[2] = q[5];
                p[3] = q[4];
                p[4] = q[3];
                p[5] = q[2];
                p[6] = q[1];
                p[7] = q[0];
                offset += 8;
                return result;
            }
        }

        public unsafe ushort ReadUShortBE() {
            if (bound < offset + 2) {
                throw new IndexOutOfRangeException();
            }
            ushort result = default(ushort);
            fixed (byte* q = &file[offset]) {
                byte* p = (byte*)&result;
                p[0] = q[1];
                p[1] = q[0];
                offset += 2;
                return result;
            }
        }

        public unsafe uint ReadUIntBE() {
            if (bound < offset + 4) {
                throw new IndexOutOfRangeException();
            }
            uint result = default(uint);
            fixed (byte* q = &file[offset]) {
                byte* p = (byte*)&result;
                p[0] = q[3];
                p[1] = q[2];
                p[2] = q[1];
                p[3] = q[0];
                offset += 4;
                return result;
            }
        }

        public unsafe ulong ReadULongBE() {
            if (bound < offset + 8) {
                throw new IndexOutOfRangeException();
            }
            ulong result;
            fixed (byte* q = &file[offset]) {
                byte* p = (byte*)&result;
                p[0] = q[7];
                p[1] = q[6];
                p[2] = q[5];
                p[3] = q[4];
                p[4] = q[3];
                p[5] = q[2];
                p[6] = q[1];
                p[7] = q[0];
                offset += 8;
                return result;
            }
        }
        #endregion

        #region VarLength Data
        public unsafe void ReadBytes(byte[] dest, int offset, int length) {
            if (dest == null) {
                throw new NullReferenceException("dest");
            }
            if (offset < 0 || offset >= dest.Length) {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (length < 0 || length > dest.Length - offset) {
                throw new ArgumentOutOfRangeException("length");
            }
            if (length + this.offset > bound) {
                throw new IndexOutOfRangeException();
            }
            fixed (byte* p = &file[this.offset]) {
            fixed (byte* q = &dest[offset])
                Buffer.MemoryCopy(p, q, length, length);
                this.offset += length;
            }
        }

        public byte[] ReadBytes(int length) {
            byte[] array = new byte[length];
            ReadBytes(array, 0, length);
            return array;
        }

        public string ReadString(int length) {
            if (length < 0) {
                throw new ArgumentOutOfRangeException("length");
            }
            if (offset + length > bound) {
                throw new IndexOutOfRangeException();
            }
            if (length != 0) {
                string @string = Encoding.UTF8.GetString(file, offset, length);
                offset += length;
                return @string;
            }
            return "";
        }

        public unsafe string ReadStringToNull() {
            byte* ptr;
            byte* endptr;
            int length;
            string @string;
            if (offset >= bound)
                throw new IndexOutOfRangeException();
            fixed (byte* p = &file[0]) {
                ptr = p + offset;
                endptr = p + bound;
                while (*ptr != 0) {
                    ptr++;
                    if (ptr >= endptr) {
                        throw new IndexOutOfRangeException();
                    }
                }
                length = (int)(ptr - p) - offset;
                @string = Encoding.UTF8.GetString(file, offset, length);
                offset = length + 1 + offset;
                return @string;
            }
        }

        public unsafe T[] ReadValueArray<T>() where T : unmanaged {
            int num = ReadInt();
            if (num != 0) {
                int num2;
                T[] result = new T[num];
                fixed (byte* p = &file[offset])
                fixed (void* q = &result[0]) { 
                    num2 =  sizeof(T) * result.Length;
                    if (num2 + offset > bound) {
                        throw new IndexOutOfRangeException();
                    }
                    Buffer.MemoryCopy(p, q, num2, num2);
                    offset += (int)num2;
                    return result;
                }
            }
            return new T[0];
        }

        public int ReadLZ4Data(int compressed_size, int uncompressed_size, byte[] dest, int dest_offset) {
            int result = LZ4.LZ4Codec.Decode(file, offset, compressed_size, dest, dest_offset, uncompressed_size);
            offset += compressed_size;
            return result;

        }
        #endregion
    }
}
