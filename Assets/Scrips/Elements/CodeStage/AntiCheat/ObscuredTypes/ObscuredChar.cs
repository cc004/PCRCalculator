// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredChar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredChar : IEquatable<ObscuredChar>
  {
    private static char cryptoKey = '—';
    private char currentCryptoKey;
    private char hiddenValue;
    private bool inited;

    private ObscuredChar(char PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredChar.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.inited = true;
    }

    public static void SetNewCryptoKey(char DCCMJMPNCDO) => ObscuredChar.cryptoKey = DCCMJMPNCDO;

    public static char EncryptDecrypt(char PDGAOEAMDCL) => ObscuredChar.EncryptDecrypt(PDGAOEAMDCL, char.MinValue);

    public static char EncryptDecrypt(char PDGAOEAMDCL, char HDAJOEOLHGG) => HDAJOEOLHGG == char.MinValue ? (char) ((uint) PDGAOEAMDCL ^ (uint) ObscuredChar.cryptoKey) : (char) ((uint) PDGAOEAMDCL ^ (uint) HDAJOEOLHGG);

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredChar.cryptoKey)
        return;
      this.hiddenValue = ObscuredChar.EncryptDecrypt(this.InternalDecrypt(), ObscuredChar.cryptoKey);
      this.currentCryptoKey = ObscuredChar.cryptoKey;
    }

    /*public void RandomizeCryptoKey()
    {
      char PDGAOEAMDCL = this.InternalDecrypt();
      this.currentCryptoKey = (char) UnityEngine.Random.Range(1, (int) ushort.MaxValue);
      this.hiddenValue = ObscuredChar.EncryptDecrypt(PDGAOEAMDCL, this.currentCryptoKey);
    }*/

    public char GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(char LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = LEBNEIHLING;
    }

    public char GetDecrypted() => this.InternalDecrypt();

    private char InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredChar.cryptoKey;
        this.hiddenValue = ObscuredChar.EncryptDecrypt(char.MinValue);
        this.inited = true;
      }
      return ObscuredChar.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
    }

    public static implicit operator ObscuredChar(char PDGAOEAMDCL) => new ObscuredChar(ObscuredChar.EncryptDecrypt(PDGAOEAMDCL));

    public static implicit operator char(ObscuredChar PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public static ObscuredChar operator ++(ObscuredChar PBAIIOCIFDP)
    {
      char PDGAOEAMDCL = (char) ((uint) PBAIIOCIFDP.InternalDecrypt() + 1U);
      PBAIIOCIFDP.hiddenValue = ObscuredChar.EncryptDecrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public static ObscuredChar operator --(ObscuredChar PBAIIOCIFDP)
    {
      char PDGAOEAMDCL = (char) ((uint) PBAIIOCIFDP.InternalDecrypt() - 1U);
      PBAIIOCIFDP.hiddenValue = ObscuredChar.EncryptDecrypt(PDGAOEAMDCL, PBAIIOCIFDP.currentCryptoKey);
      return PBAIIOCIFDP;
    }

    public override bool Equals(object EACOJCAGIDK) => EACOJCAGIDK is ObscuredChar EACOJCAGIDK1 && this.Equals(EACOJCAGIDK1);

    public bool Equals(ObscuredChar EACOJCAGIDK) => (int) this.currentCryptoKey == (int) EACOJCAGIDK.currentCryptoKey ? (int) this.hiddenValue == (int) EACOJCAGIDK.hiddenValue : (int) ObscuredChar.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredChar.EncryptDecrypt(EACOJCAGIDK.hiddenValue, EACOJCAGIDK.currentCryptoKey);

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(IFormatProvider BOEPAAMPIBF) => this.InternalDecrypt().ToString(BOEPAAMPIBF);

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();
  }
}
