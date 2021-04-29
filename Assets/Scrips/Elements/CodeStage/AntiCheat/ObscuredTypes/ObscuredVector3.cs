// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredVector3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredVector3
  {
    private static int cryptoKey = 120207;
    private static readonly Vector3 initialFakeValue = Vector3.zero;
    [SerializeField]
    private int currentCryptoKey;
    [SerializeField]
    private ObscuredVector3.RawEncryptedVector3 hiddenValue;
    [SerializeField]
    private bool inited;

    private ObscuredVector3(ObscuredVector3.RawEncryptedVector3 LEBNEIHLING)
    {
      this.currentCryptoKey = ObscuredVector3.cryptoKey;
      this.hiddenValue = LEBNEIHLING;
      this.inited = true;
    }

    public ObscuredVector3(float DEGHPOFNMGC, float KPJCDBGDMIK, float NIGMDNILPKN)
    {
      this.currentCryptoKey = ObscuredVector3.cryptoKey;
      this.hiddenValue = ObscuredVector3.Encrypt(DEGHPOFNMGC, KPJCDBGDMIK, NIGMDNILPKN, this.currentCryptoKey);
      this.inited = true;
    }

    public float x
    {
      get => this.InternalDecryptField(this.hiddenValue.x);
      set => this.hiddenValue.x = this.InternalEncryptField(value);
    }

    public float y
    {
      get => this.InternalDecryptField(this.hiddenValue.y);
      set => this.hiddenValue.y = this.InternalEncryptField(value);
    }

    public float z
    {
      get => this.InternalDecryptField(this.hiddenValue.z);
      set => this.hiddenValue.z = this.InternalEncryptField(value);
    }

    public float this[int ELNABNNHPPH]
    {
      get
      {
        switch (ELNABNNHPPH)
        {
          case 0:
            return this.x;
          case 1:
            return this.y;
          case 2:
            return this.z;
          default:
            throw new IndexOutOfRangeException("Invalid ObscuredVector3 index!");
        }
      }
      set
      {
        switch (ELNABNNHPPH)
        {
          case 0:
            this.x = value;
            break;
          case 1:
            this.y = value;
            break;
          case 2:
            this.z = value;
            break;
          default:
            throw new IndexOutOfRangeException("Invalid ObscuredVector3 index!");
        }
      }
    }

    public static void SetNewCryptoKey(int DCCMJMPNCDO) => ObscuredVector3.cryptoKey = DCCMJMPNCDO;

    public static ObscuredVector3.RawEncryptedVector3 Encrypt(Vector3 PDGAOEAMDCL) => ObscuredVector3.Encrypt(PDGAOEAMDCL, 0);

    public static ObscuredVector3.RawEncryptedVector3 Encrypt(
      Vector3 PDGAOEAMDCL,
      int HDAJOEOLHGG) => ObscuredVector3.Encrypt(PDGAOEAMDCL.x, PDGAOEAMDCL.y, PDGAOEAMDCL.z, HDAJOEOLHGG);

    public static ObscuredVector3.RawEncryptedVector3 Encrypt(
      float DEGHPOFNMGC,
      float KPJCDBGDMIK,
      float NIGMDNILPKN,
      int HDAJOEOLHGG)
    {
      if (HDAJOEOLHGG == 0)
        HDAJOEOLHGG = ObscuredVector3.cryptoKey;
      ObscuredVector3.RawEncryptedVector3 encryptedVector3;
      encryptedVector3.x = ObscuredFloat.Encrypt(DEGHPOFNMGC, HDAJOEOLHGG);
      encryptedVector3.y = ObscuredFloat.Encrypt(KPJCDBGDMIK, HDAJOEOLHGG);
      encryptedVector3.z = ObscuredFloat.Encrypt(NIGMDNILPKN, HDAJOEOLHGG);
      return encryptedVector3;
    }

    public static Vector3 Decrypt(ObscuredVector3.RawEncryptedVector3 PDGAOEAMDCL) => ObscuredVector3.Decrypt(PDGAOEAMDCL, 0);

    public static Vector3 Decrypt(
      ObscuredVector3.RawEncryptedVector3 PDGAOEAMDCL,
      int HDAJOEOLHGG)
    {
      if (HDAJOEOLHGG == 0)
        HDAJOEOLHGG = ObscuredVector3.cryptoKey;
      Vector3 vector3;
      vector3.x = ObscuredFloat.Decrypt(PDGAOEAMDCL.x, HDAJOEOLHGG);
      vector3.y = ObscuredFloat.Decrypt(PDGAOEAMDCL.y, HDAJOEOLHGG);
      vector3.z = ObscuredFloat.Decrypt(PDGAOEAMDCL.z, HDAJOEOLHGG);
      return vector3;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredVector3.cryptoKey)
        return;
      this.hiddenValue = ObscuredVector3.Encrypt(this.InternalDecrypt(), ObscuredVector3.cryptoKey);
      this.currentCryptoKey = ObscuredVector3.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      Vector3 PDGAOEAMDCL = this.InternalDecrypt();
      do
      {
        this.currentCryptoKey = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
      }
      while (this.currentCryptoKey == 0);
      this.hiddenValue = ObscuredVector3.Encrypt(PDGAOEAMDCL, this.currentCryptoKey);
    }

    public ObscuredVector3.RawEncryptedVector3 GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(ObscuredVector3.RawEncryptedVector3 LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = LEBNEIHLING;
    }

    public Vector3 GetDecrypted() => this.InternalDecrypt();

    private Vector3 InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredVector3.cryptoKey;
        this.hiddenValue = ObscuredVector3.Encrypt(ObscuredVector3.initialFakeValue, ObscuredVector3.cryptoKey);
        this.inited = true;
      }
      Vector3 vector3;
      vector3.x = ObscuredFloat.Decrypt(this.hiddenValue.x, this.currentCryptoKey);
      vector3.y = ObscuredFloat.Decrypt(this.hiddenValue.y, this.currentCryptoKey);
      vector3.z = ObscuredFloat.Decrypt(this.hiddenValue.z, this.currentCryptoKey);
      return vector3;
    }

    private float InternalDecryptField(int LEBNEIHLING)
    {
      int HDAJOEOLHGG = ObscuredVector3.cryptoKey;
      if (this.currentCryptoKey != ObscuredVector3.cryptoKey)
        HDAJOEOLHGG = this.currentCryptoKey;
      return ObscuredFloat.Decrypt(LEBNEIHLING, HDAJOEOLHGG);
    }

    private int InternalEncryptField(float LEBNEIHLING) => ObscuredFloat.Encrypt(LEBNEIHLING, ObscuredVector3.cryptoKey);

    public static implicit operator ObscuredVector3(Vector3 PDGAOEAMDCL) => new ObscuredVector3(ObscuredVector3.Encrypt(PDGAOEAMDCL, ObscuredVector3.cryptoKey));

    public static implicit operator Vector3(ObscuredVector3 PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public static ObscuredVector3 operator +(
      ObscuredVector3 IPJGCOBNHLB,
      ObscuredVector3 IMMPDMOKFGC) => (ObscuredVector3) (IPJGCOBNHLB.InternalDecrypt() + IMMPDMOKFGC.InternalDecrypt());

    public static ObscuredVector3 operator +(
      Vector3 IPJGCOBNHLB,
      ObscuredVector3 IMMPDMOKFGC) => (ObscuredVector3) (IPJGCOBNHLB + IMMPDMOKFGC.InternalDecrypt());

    public static ObscuredVector3 operator +(
      ObscuredVector3 IPJGCOBNHLB,
      Vector3 IMMPDMOKFGC) => (ObscuredVector3) (IPJGCOBNHLB.InternalDecrypt() + IMMPDMOKFGC);

    public static ObscuredVector3 operator -(
      ObscuredVector3 IPJGCOBNHLB,
      ObscuredVector3 IMMPDMOKFGC) => (ObscuredVector3) (IPJGCOBNHLB.InternalDecrypt() - IMMPDMOKFGC.InternalDecrypt());

    public static ObscuredVector3 operator -(
      Vector3 IPJGCOBNHLB,
      ObscuredVector3 IMMPDMOKFGC) => (ObscuredVector3) (IPJGCOBNHLB - IMMPDMOKFGC.InternalDecrypt());

    public static ObscuredVector3 operator -(
      ObscuredVector3 IPJGCOBNHLB,
      Vector3 IMMPDMOKFGC) => (ObscuredVector3) (IPJGCOBNHLB.InternalDecrypt() - IMMPDMOKFGC);

        public static ObscuredVector3 operator -(ObscuredVector3 IPJGCOBNHLB) => (ObscuredVector3) (-IPJGCOBNHLB.InternalDecrypt());

    public static ObscuredVector3 operator *(
      ObscuredVector3 IPJGCOBNHLB,
      float OANBBIFPFBF) => (ObscuredVector3) (IPJGCOBNHLB.InternalDecrypt() * OANBBIFPFBF);

    public static ObscuredVector3 operator *(
      float OANBBIFPFBF,
      ObscuredVector3 IPJGCOBNHLB) => (ObscuredVector3) (OANBBIFPFBF * IPJGCOBNHLB.InternalDecrypt());

    public static ObscuredVector3 operator /(
      ObscuredVector3 IPJGCOBNHLB,
      float OANBBIFPFBF) => (ObscuredVector3) (IPJGCOBNHLB.InternalDecrypt() / OANBBIFPFBF);

    public static bool operator ==(ObscuredVector3 OCDCFGBIICE, ObscuredVector3 JPACPKNCENF) => OCDCFGBIICE.InternalDecrypt() == JPACPKNCENF.InternalDecrypt();

    public static bool operator ==(Vector3 OCDCFGBIICE, ObscuredVector3 JPACPKNCENF) => OCDCFGBIICE == JPACPKNCENF.InternalDecrypt();

    public static bool operator ==(ObscuredVector3 OCDCFGBIICE, Vector3 JPACPKNCENF) => OCDCFGBIICE.InternalDecrypt() == JPACPKNCENF;

    public static bool operator !=(ObscuredVector3 OCDCFGBIICE, ObscuredVector3 JPACPKNCENF) => OCDCFGBIICE.InternalDecrypt() != JPACPKNCENF.InternalDecrypt();

    public static bool operator !=(Vector3 OCDCFGBIICE, ObscuredVector3 JPACPKNCENF) => OCDCFGBIICE != JPACPKNCENF.InternalDecrypt();

    public static bool operator !=(ObscuredVector3 OCDCFGBIICE, Vector3 JPACPKNCENF) => OCDCFGBIICE.InternalDecrypt() != JPACPKNCENF;

    public override bool Equals(object DILMKMMLAIM) => this.InternalDecrypt().Equals(DILMKMMLAIM);

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string JKDBJBLHONP) => this.InternalDecrypt().ToString(JKDBJBLHONP);

    [Serializable]
    public struct RawEncryptedVector3
    {
      public int x;
      public int y;
      public int z;
    }
  }
}
