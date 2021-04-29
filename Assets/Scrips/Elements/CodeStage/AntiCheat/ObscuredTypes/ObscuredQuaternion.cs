// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredQuaternion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredQuaternion
  {
    private static int cryptoKey = 120205;
    private static readonly Quaternion initialFakeValue = Quaternion.identity;
    [SerializeField]
    private int currentCryptoKey;
    [SerializeField]
    private ObscuredQuaternion.RawEncryptedQuaternion hiddenValue;
    [SerializeField]
    private bool inited;

    private ObscuredQuaternion(
      ObscuredQuaternion.RawEncryptedQuaternion PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.inited = true;
    }

    public ObscuredQuaternion(
      float DEGHPOFNMGC,
      float KPJCDBGDMIK,
      float NIGMDNILPKN,
      float HKGGPJIALKL)
    {
      this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
      this.hiddenValue = ObscuredQuaternion.Encrypt(DEGHPOFNMGC, KPJCDBGDMIK, NIGMDNILPKN, HKGGPJIALKL, this.currentCryptoKey);
      this.inited = true;
    }

    public static void SetNewCryptoKey(int DCCMJMPNCDO) => ObscuredQuaternion.cryptoKey = DCCMJMPNCDO;

    public static ObscuredQuaternion.RawEncryptedQuaternion Encrypt(
      Quaternion PDGAOEAMDCL) => ObscuredQuaternion.Encrypt(PDGAOEAMDCL, 0);

    public static ObscuredQuaternion.RawEncryptedQuaternion Encrypt(
      Quaternion PDGAOEAMDCL,
      int HDAJOEOLHGG) => ObscuredQuaternion.Encrypt(PDGAOEAMDCL.x, PDGAOEAMDCL.y, PDGAOEAMDCL.z, PDGAOEAMDCL.w, HDAJOEOLHGG);

    public static ObscuredQuaternion.RawEncryptedQuaternion Encrypt(
      float DEGHPOFNMGC,
      float KPJCDBGDMIK,
      float NIGMDNILPKN,
      float HKGGPJIALKL,
      int HDAJOEOLHGG)
    {
      if (HDAJOEOLHGG == 0)
        HDAJOEOLHGG = ObscuredQuaternion.cryptoKey;
      ObscuredQuaternion.RawEncryptedQuaternion encryptedQuaternion;
      encryptedQuaternion.x = ObscuredFloat.Encrypt(DEGHPOFNMGC, HDAJOEOLHGG);
      encryptedQuaternion.y = ObscuredFloat.Encrypt(KPJCDBGDMIK, HDAJOEOLHGG);
      encryptedQuaternion.z = ObscuredFloat.Encrypt(NIGMDNILPKN, HDAJOEOLHGG);
      encryptedQuaternion.w = ObscuredFloat.Encrypt(HKGGPJIALKL, HDAJOEOLHGG);
      return encryptedQuaternion;
    }

    public static Quaternion Decrypt(
      ObscuredQuaternion.RawEncryptedQuaternion PDGAOEAMDCL) => ObscuredQuaternion.Decrypt(PDGAOEAMDCL, 0);

    public static Quaternion Decrypt(
      ObscuredQuaternion.RawEncryptedQuaternion PDGAOEAMDCL,
      int HDAJOEOLHGG)
    {
      if (HDAJOEOLHGG == 0)
        HDAJOEOLHGG = ObscuredQuaternion.cryptoKey;
      Quaternion quaternion;
      quaternion.x = ObscuredFloat.Decrypt(PDGAOEAMDCL.x, HDAJOEOLHGG);
      quaternion.y = ObscuredFloat.Decrypt(PDGAOEAMDCL.y, HDAJOEOLHGG);
      quaternion.z = ObscuredFloat.Decrypt(PDGAOEAMDCL.z, HDAJOEOLHGG);
      quaternion.w = ObscuredFloat.Decrypt(PDGAOEAMDCL.w, HDAJOEOLHGG);
      return quaternion;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredQuaternion.cryptoKey)
        return;
      this.hiddenValue = ObscuredQuaternion.Encrypt(this.InternalDecrypt(), ObscuredQuaternion.cryptoKey);
      this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      Quaternion PDGAOEAMDCL = this.InternalDecrypt();
      do
      {
        this.currentCryptoKey = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
      }
      while (this.currentCryptoKey == 0);
      this.hiddenValue = ObscuredQuaternion.Encrypt(PDGAOEAMDCL, this.currentCryptoKey);
    }

    public ObscuredQuaternion.RawEncryptedQuaternion GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(
      ObscuredQuaternion.RawEncryptedQuaternion LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = LEBNEIHLING;
    }

    public Quaternion GetDecrypted() => this.InternalDecrypt();

    private Quaternion InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
        this.hiddenValue = ObscuredQuaternion.Encrypt(ObscuredQuaternion.initialFakeValue);
        this.inited = true;
      }
      Quaternion quaternion;
      quaternion.x = ObscuredFloat.Decrypt(this.hiddenValue.x, this.currentCryptoKey);
      quaternion.y = ObscuredFloat.Decrypt(this.hiddenValue.y, this.currentCryptoKey);
      quaternion.z = ObscuredFloat.Decrypt(this.hiddenValue.z, this.currentCryptoKey);
      quaternion.w = ObscuredFloat.Decrypt(this.hiddenValue.w, this.currentCryptoKey);
      return quaternion;
    }

    public static implicit operator ObscuredQuaternion(Quaternion PDGAOEAMDCL) => new ObscuredQuaternion(ObscuredQuaternion.Encrypt(PDGAOEAMDCL));

    public static implicit operator Quaternion(ObscuredQuaternion PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string JKDBJBLHONP) => this.InternalDecrypt().ToString(JKDBJBLHONP);

    [Serializable]
    public struct RawEncryptedQuaternion
    {
      public int x;
      public int y;
      public int z;
      public int w;
    }
  }
}
