// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredShort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredShort : IEquatable<ObscuredShort>, IFormattable
  {
    private static short cryptoKey = 214;
    [SerializeField]
    private short currentCryptoKey;
    [SerializeField]
    private short hiddenValue;
    [SerializeField]
    private bool inited;

    private ObscuredShort(short PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredShort.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.inited = true;
    }

    public static void SetNewCryptoKey(short DCCMJMPNCDO) => ObscuredShort.cryptoKey = DCCMJMPNCDO;

    public static short EncryptDecrypt(short PDGAOEAMDCL) => ObscuredShort.EncryptDecrypt(PDGAOEAMDCL, (short) 0);

    public static short EncryptDecrypt(short PDGAOEAMDCL, short HDAJOEOLHGG) => HDAJOEOLHGG == (short) 0 ? (short) ((int) PDGAOEAMDCL ^ (int) ObscuredShort.cryptoKey) : (short) ((int) PDGAOEAMDCL ^ (int) HDAJOEOLHGG);

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredShort.cryptoKey)
        return;
      this.hiddenValue = ObscuredShort.EncryptDecrypt(this.InternalDecrypt(), ObscuredShort.cryptoKey);
      this.currentCryptoKey = ObscuredShort.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      short PDGAOEAMDCL = this.InternalDecrypt();
      do
      {
        this.currentCryptoKey = (short) UnityEngine.Random.Range((int) short.MinValue, (int) short.MaxValue);
      }
      while (this.currentCryptoKey == (short) 0);
      this.hiddenValue = ObscuredShort.EncryptDecrypt(PDGAOEAMDCL, this.currentCryptoKey);
    }

    public short GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(short LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = LEBNEIHLING;
    }

    public short GetDecrypted() => this.InternalDecrypt();

    private short InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredShort.cryptoKey;
        this.hiddenValue = ObscuredShort.EncryptDecrypt((short) 0);
        this.inited = true;
      }
      return ObscuredShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
    }

    public static implicit operator ObscuredShort(short PDGAOEAMDCL) => new ObscuredShort(ObscuredShort.EncryptDecrypt(PDGAOEAMDCL));

    public static implicit operator short(ObscuredShort PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public static ObscuredShort operator ++(ObscuredShort PBAIIOCIFDP)
    {
      short PDGAOEAMDCL = (short) ((int) PBAIIOCIFDP.InternalDecrypt() + 1);
      PBAIIOCIFDP.hiddenValue = ObscuredShort.EncryptDecrypt(PDGAOEAMDCL);
      return PBAIIOCIFDP;
    }

    public static ObscuredShort operator --(ObscuredShort PBAIIOCIFDP)
    {
      short PDGAOEAMDCL = (short) ((int) PBAIIOCIFDP.InternalDecrypt() - 1);
      PBAIIOCIFDP.hiddenValue = ObscuredShort.EncryptDecrypt(PDGAOEAMDCL);
      return PBAIIOCIFDP;
    }

    public override bool Equals(object EACOJCAGIDK) => EACOJCAGIDK is ObscuredShort EACOJCAGIDK1 && this.Equals(EACOJCAGIDK1);

    public bool Equals(ObscuredShort EACOJCAGIDK) => (int) this.currentCryptoKey == (int) EACOJCAGIDK.currentCryptoKey ? (int) this.hiddenValue == (int) EACOJCAGIDK.hiddenValue : (int) ObscuredShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredShort.EncryptDecrypt(EACOJCAGIDK.hiddenValue, EACOJCAGIDK.currentCryptoKey);

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string JKDBJBLHONP) => this.InternalDecrypt().ToString(JKDBJBLHONP);

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public string ToString(IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(BOEPAAMPIBF);

    public string ToString(string JKDBJBLHONP, IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(JKDBJBLHONP, BOEPAAMPIBF);
  }
}
