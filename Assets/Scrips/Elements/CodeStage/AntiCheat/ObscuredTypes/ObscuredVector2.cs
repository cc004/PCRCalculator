// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredVector2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredVector2
  {
    private static int cryptoKey = 120206;
    private static readonly Vector2 initialFakeValue = Vector2.zero;
    [SerializeField]
    private int currentCryptoKey;
    [SerializeField]
    private ObscuredVector2.RawEncryptedVector2 hiddenValue;
    [SerializeField]
    private bool inited;

    private ObscuredVector2(ObscuredVector2.RawEncryptedVector2 PDGAOEAMDCL)
    {
      this.currentCryptoKey = ObscuredVector2.cryptoKey;
      this.hiddenValue = PDGAOEAMDCL;
      this.inited = true;
    }

    public ObscuredVector2(float DEGHPOFNMGC, float KPJCDBGDMIK)
    {
      this.currentCryptoKey = ObscuredVector2.cryptoKey;
      this.hiddenValue = ObscuredVector2.Encrypt(DEGHPOFNMGC, KPJCDBGDMIK, this.currentCryptoKey);
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

    public float this[int ELNABNNHPPH]
    {
      get
      {
        if (ELNABNNHPPH == 0)
          return this.x;
        if (ELNABNNHPPH == 1)
          return this.y;
        throw new IndexOutOfRangeException("Invalid ObscuredVector2 index!");
      }
      set
      {
        if (ELNABNNHPPH != 0)
        {
          if (ELNABNNHPPH != 1)
            throw new IndexOutOfRangeException("Invalid ObscuredVector2 index!");
          this.y = value;
        }
        else
          this.x = value;
      }
    }

    public static void SetNewCryptoKey(int DCCMJMPNCDO) => ObscuredVector2.cryptoKey = DCCMJMPNCDO;

    public static ObscuredVector2.RawEncryptedVector2 Encrypt(Vector2 PDGAOEAMDCL) => ObscuredVector2.Encrypt(PDGAOEAMDCL, 0);

    public static ObscuredVector2.RawEncryptedVector2 Encrypt(
      Vector2 PDGAOEAMDCL,
      int HDAJOEOLHGG) => ObscuredVector2.Encrypt(PDGAOEAMDCL.x, PDGAOEAMDCL.y, HDAJOEOLHGG);

    public static ObscuredVector2.RawEncryptedVector2 Encrypt(
      float DEGHPOFNMGC,
      float KPJCDBGDMIK,
      int HDAJOEOLHGG)
    {
      if (HDAJOEOLHGG == 0)
        HDAJOEOLHGG = ObscuredVector2.cryptoKey;
      ObscuredVector2.RawEncryptedVector2 encryptedVector2;
      encryptedVector2.x = ObscuredFloat.Encrypt(DEGHPOFNMGC, HDAJOEOLHGG);
      encryptedVector2.y = ObscuredFloat.Encrypt(KPJCDBGDMIK, HDAJOEOLHGG);
      return encryptedVector2;
    }

    public static Vector2 Decrypt(ObscuredVector2.RawEncryptedVector2 PDGAOEAMDCL) => ObscuredVector2.Decrypt(PDGAOEAMDCL, 0);

    public static Vector2 Decrypt(
      ObscuredVector2.RawEncryptedVector2 PDGAOEAMDCL,
      int HDAJOEOLHGG)
    {
      if (HDAJOEOLHGG == 0)
        HDAJOEOLHGG = ObscuredVector2.cryptoKey;
      Vector2 vector2;
      vector2.x = ObscuredFloat.Decrypt(PDGAOEAMDCL.x, HDAJOEOLHGG);
      vector2.y = ObscuredFloat.Decrypt(PDGAOEAMDCL.y, HDAJOEOLHGG);
      return vector2;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredVector2.cryptoKey)
        return;
      this.hiddenValue = ObscuredVector2.Encrypt(this.InternalDecrypt(), ObscuredVector2.cryptoKey);
      this.currentCryptoKey = ObscuredVector2.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      Vector2 PDGAOEAMDCL = this.InternalDecrypt();
      do
      {
        this.currentCryptoKey = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
      }
      while (this.currentCryptoKey == 0);
      this.hiddenValue = ObscuredVector2.Encrypt(PDGAOEAMDCL, this.currentCryptoKey);
    }

    public ObscuredVector2.RawEncryptedVector2 GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(ObscuredVector2.RawEncryptedVector2 LEBNEIHLING)
    {
      this.inited = true;
      this.hiddenValue = LEBNEIHLING;
    }

    public Vector2 GetDecrypted() => this.InternalDecrypt();

    private Vector2 InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredVector2.cryptoKey;
        this.hiddenValue = ObscuredVector2.Encrypt(ObscuredVector2.initialFakeValue);
        this.inited = true;
      }
      Vector2 vector2;
      vector2.x = ObscuredFloat.Decrypt(this.hiddenValue.x, this.currentCryptoKey);
      vector2.y = ObscuredFloat.Decrypt(this.hiddenValue.y, this.currentCryptoKey);
      return vector2;
    }

    private float InternalDecryptField(int LEBNEIHLING)
    {
      int HDAJOEOLHGG = ObscuredVector2.cryptoKey;
      if (this.currentCryptoKey != ObscuredVector2.cryptoKey)
        HDAJOEOLHGG = this.currentCryptoKey;
      return ObscuredFloat.Decrypt(LEBNEIHLING, HDAJOEOLHGG);
    }

    private int InternalEncryptField(float LEBNEIHLING) => ObscuredFloat.Encrypt(LEBNEIHLING, ObscuredVector2.cryptoKey);

    public static implicit operator ObscuredVector2(Vector2 PDGAOEAMDCL) => new ObscuredVector2(ObscuredVector2.Encrypt(PDGAOEAMDCL));

    public static implicit operator Vector2(ObscuredVector2 PDGAOEAMDCL) => PDGAOEAMDCL.InternalDecrypt();

    public static implicit operator Vector3(ObscuredVector2 PDGAOEAMDCL)
    {
      Vector2 vector2 = PDGAOEAMDCL.InternalDecrypt();
      return new Vector3(vector2.x, vector2.y, 0.0f);
    }

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string JKDBJBLHONP) => this.InternalDecrypt().ToString(JKDBJBLHONP);

    [Serializable]
    public struct RawEncryptedVector2
    {
      public int x;
      public int y;
    }
  }
}
