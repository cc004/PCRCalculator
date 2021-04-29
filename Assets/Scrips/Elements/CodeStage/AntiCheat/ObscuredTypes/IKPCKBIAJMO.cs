// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.IKPCKBIAJMO
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using CodeStage.AntiCheat.Utils;
using System;
using System.Text;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  public static class IKPCKBIAJMO
  {
    private const byte VERSION = 2;
    private const string RAW_NOT_FOUND = "{not_found}";
    private const string DATA_SEPARATOR = "|";
    private static bool foreignSavesReported;
    private static string cryptoKey = "e806f6";
    private static string deviceId;
    private static uint deviceIdHash;
    public static Action onAlterationDetected;
    public static bool preservePlayerPrefs = false;
    public static Action onPossibleForeignSavesDetected = (Action) null;
    public static IKPCKBIAJMO.ODIOCLPJGAG lockToDevice = IKPCKBIAJMO.ODIOCLPJGAG.None;
    public static bool readForeignSaves = false;
    public static bool emergencyMode = false;
    private const char DEPRECATED_RAW_SEPARATOR = ':';
    private static string deprecatedDeviceId;

    public static string DIJCLMFENCE
    {
      set => IKPCKBIAJMO.cryptoKey = value;
      get => IKPCKBIAJMO.cryptoKey;
    }

    public static string BOFDLLABFNL
    {
      get
      {
        if (string.IsNullOrEmpty(IKPCKBIAJMO.deviceId))
          IKPCKBIAJMO.deviceId = IKPCKBIAJMO.GetDeviceId();
        return IKPCKBIAJMO.deviceId;
      }
      set => IKPCKBIAJMO.deviceId = value;
    }

    [Obsolete("This property is obsolete, please use DeviceId instead.")]
    internal static string OPODKIDDBMI
    {
      get => IKPCKBIAJMO.BOFDLLABFNL;
      set => IKPCKBIAJMO.BOFDLLABFNL = value;
    }

    private static uint NINDOMAMBAD
    {
      get
      {
        if (IKPCKBIAJMO.deviceIdHash == 0U)
          IKPCKBIAJMO.deviceIdHash = IKPCKBIAJMO.CalculateChecksum(IKPCKBIAJMO.BOFDLLABFNL);
        return IKPCKBIAJMO.deviceIdHash;
      }
    }

    public static void ForceLockToDeviceInit()
    {
      if (!string.IsNullOrEmpty(IKPCKBIAJMO.deviceId))
        return;
      IKPCKBIAJMO.deviceId = IKPCKBIAJMO.GetDeviceId();
      IKPCKBIAJMO.deviceIdHash = IKPCKBIAJMO.CalculateChecksum(IKPCKBIAJMO.deviceId);
    }

    [Obsolete("This method is obsolete, use property CryptoKey instead")]
    internal static void SetNewCryptoKey(string DCCMJMPNCDO) => IKPCKBIAJMO.DIJCLMFENCE = DCCMJMPNCDO;

    public static void SetInt(string HDAJOEOLHGG, int PDGAOEAMDCL) => PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), IKPCKBIAJMO.EncryptIntValue(HDAJOEOLHGG, PDGAOEAMDCL));

    public static int GetInt(string HDAJOEOLHGG) => IKPCKBIAJMO.GetInt(HDAJOEOLHGG, 0);

    public static int GetInt(string HDAJOEOLHGG, int JLLDHLJOAOI)
    {
      string str = IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG);
      if (!PlayerPrefs.HasKey(str) && PlayerPrefs.HasKey(HDAJOEOLHGG))
      {
        int PDGAOEAMDCL = PlayerPrefs.GetInt(HDAJOEOLHGG, JLLDHLJOAOI);
        if (!IKPCKBIAJMO.preservePlayerPrefs)
        {
          IKPCKBIAJMO.SetInt(HDAJOEOLHGG, PDGAOEAMDCL);
          PlayerPrefs.DeleteKey(HDAJOEOLHGG);
        }
        return PDGAOEAMDCL;
      }
      string encryptedPrefsString = IKPCKBIAJMO.GetEncryptedPrefsString(HDAJOEOLHGG, str);
      return !(encryptedPrefsString == "{not_found}") ? IKPCKBIAJMO.DecryptIntValue(HDAJOEOLHGG, encryptedPrefsString, JLLDHLJOAOI) : JLLDHLJOAOI;
    }

    internal static string EncryptIntValue(string HDAJOEOLHGG, int PDGAOEAMDCL)
    {
      byte[] bytes = BitConverter.GetBytes(PDGAOEAMDCL);
      return IKPCKBIAJMO.EncryptData(HDAJOEOLHGG, bytes, IKPCKBIAJMO.AIHKHLNLINM.Int);
    }

    internal static int DecryptIntValue(string HDAJOEOLHGG, string JHJLBKDKCIB, int JLLDHLJOAOI)
    {
      if (JHJLBKDKCIB.IndexOf(':') > -1)
      {
        string s = IKPCKBIAJMO.DeprecatedDecryptValue(JHJLBKDKCIB);
        if (s == "")
          return JLLDHLJOAOI;
        int result;
        int.TryParse(s, out result);
        IKPCKBIAJMO.SetInt(HDAJOEOLHGG, result);
        return result;
      }
      byte[] numArray = IKPCKBIAJMO.DecryptData(HDAJOEOLHGG, JHJLBKDKCIB);
      return numArray == null ? JLLDHLJOAOI : BitConverter.ToInt32(numArray, 0);
    }

    public static void SetUInt(string HDAJOEOLHGG, uint PDGAOEAMDCL) => PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), IKPCKBIAJMO.EncryptUIntValue(HDAJOEOLHGG, PDGAOEAMDCL));

    public static uint GetUInt(string HDAJOEOLHGG) => IKPCKBIAJMO.GetUInt(HDAJOEOLHGG, 0U);

    public static uint GetUInt(string HDAJOEOLHGG, uint JLLDHLJOAOI)
    {
      string encryptedPrefsString = IKPCKBIAJMO.GetEncryptedPrefsString(HDAJOEOLHGG, IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG));
      return !(encryptedPrefsString == "{not_found}") ? IKPCKBIAJMO.DecryptUIntValue(HDAJOEOLHGG, encryptedPrefsString, JLLDHLJOAOI) : JLLDHLJOAOI;
    }

    private static string EncryptUIntValue(string HDAJOEOLHGG, uint PDGAOEAMDCL)
    {
      byte[] bytes = BitConverter.GetBytes(PDGAOEAMDCL);
      return IKPCKBIAJMO.EncryptData(HDAJOEOLHGG, bytes, IKPCKBIAJMO.AIHKHLNLINM.UInt);
    }

    private static uint DecryptUIntValue(string HDAJOEOLHGG, string JHJLBKDKCIB, uint JLLDHLJOAOI)
    {
      if (JHJLBKDKCIB.IndexOf(':') > -1)
      {
        string s = IKPCKBIAJMO.DeprecatedDecryptValue(JHJLBKDKCIB);
        if (s == "")
          return JLLDHLJOAOI;
        uint result;
        uint.TryParse(s, out result);
        IKPCKBIAJMO.SetUInt(HDAJOEOLHGG, result);
        return result;
      }
      byte[] numArray = IKPCKBIAJMO.DecryptData(HDAJOEOLHGG, JHJLBKDKCIB);
      return numArray == null ? JLLDHLJOAOI : BitConverter.ToUInt32(numArray, 0);
    }

    public static void SetString(string HDAJOEOLHGG, string PDGAOEAMDCL) => PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), IKPCKBIAJMO.EncryptStringValue(HDAJOEOLHGG, PDGAOEAMDCL));

    public static string GetString(string HDAJOEOLHGG) => IKPCKBIAJMO.GetString(HDAJOEOLHGG, "");

    public static string GetString(string HDAJOEOLHGG, string JLLDHLJOAOI)
    {
      string str = IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG);
      if (!PlayerPrefs.HasKey(str) && PlayerPrefs.HasKey(HDAJOEOLHGG))
      {
        string PDGAOEAMDCL = PlayerPrefs.GetString(HDAJOEOLHGG, JLLDHLJOAOI);
        if (!IKPCKBIAJMO.preservePlayerPrefs)
        {
          IKPCKBIAJMO.SetString(HDAJOEOLHGG, PDGAOEAMDCL);
          PlayerPrefs.DeleteKey(HDAJOEOLHGG);
        }
        return PDGAOEAMDCL;
      }
      string encryptedPrefsString = IKPCKBIAJMO.GetEncryptedPrefsString(HDAJOEOLHGG, str);
      return !(encryptedPrefsString == "{not_found}") ? IKPCKBIAJMO.DecryptStringValue(HDAJOEOLHGG, encryptedPrefsString, JLLDHLJOAOI) : JLLDHLJOAOI;
    }

    internal static string EncryptStringValue(string HDAJOEOLHGG, string PDGAOEAMDCL)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(PDGAOEAMDCL);
      return IKPCKBIAJMO.EncryptData(HDAJOEOLHGG, bytes, IKPCKBIAJMO.AIHKHLNLINM.String);
    }

    internal static string DecryptStringValue(
      string HDAJOEOLHGG,
      string JHJLBKDKCIB,
      string JLLDHLJOAOI)
    {
      if (JHJLBKDKCIB.IndexOf(':') > -1)
      {
        string PDGAOEAMDCL = IKPCKBIAJMO.DeprecatedDecryptValue(JHJLBKDKCIB);
        if (PDGAOEAMDCL == "")
          return JLLDHLJOAOI;
        IKPCKBIAJMO.SetString(HDAJOEOLHGG, PDGAOEAMDCL);
        return PDGAOEAMDCL;
      }
      byte[] bytes = IKPCKBIAJMO.DecryptData(HDAJOEOLHGG, JHJLBKDKCIB);
      return bytes == null ? JLLDHLJOAOI : Encoding.UTF8.GetString(bytes, 0, bytes.Length);
    }

    public static void SetFloat(string HDAJOEOLHGG, float PDGAOEAMDCL) => PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), IKPCKBIAJMO.EncryptFloatValue(HDAJOEOLHGG, PDGAOEAMDCL));

    public static float GetFloat(string HDAJOEOLHGG) => IKPCKBIAJMO.GetFloat(HDAJOEOLHGG, 0.0f);

    public static float GetFloat(string HDAJOEOLHGG, float JLLDHLJOAOI)
    {
      string str = IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG);
      if (!PlayerPrefs.HasKey(str) && PlayerPrefs.HasKey(HDAJOEOLHGG))
      {
        float PDGAOEAMDCL = PlayerPrefs.GetFloat(HDAJOEOLHGG, JLLDHLJOAOI);
        if (!IKPCKBIAJMO.preservePlayerPrefs)
        {
          IKPCKBIAJMO.SetFloat(HDAJOEOLHGG, PDGAOEAMDCL);
          PlayerPrefs.DeleteKey(HDAJOEOLHGG);
        }
        return PDGAOEAMDCL;
      }
      string encryptedPrefsString = IKPCKBIAJMO.GetEncryptedPrefsString(HDAJOEOLHGG, str);
      return !(encryptedPrefsString == "{not_found}") ? IKPCKBIAJMO.DecryptFloatValue(HDAJOEOLHGG, encryptedPrefsString, JLLDHLJOAOI) : JLLDHLJOAOI;
    }

    internal static string EncryptFloatValue(string HDAJOEOLHGG, float PDGAOEAMDCL)
    {
      byte[] bytes = BitConverter.GetBytes(PDGAOEAMDCL);
      return IKPCKBIAJMO.EncryptData(HDAJOEOLHGG, bytes, IKPCKBIAJMO.AIHKHLNLINM.Float);
    }

    internal static float DecryptFloatValue(
      string HDAJOEOLHGG,
      string JHJLBKDKCIB,
      float JLLDHLJOAOI)
    {
      if (JHJLBKDKCIB.IndexOf(':') > -1)
      {
        string s = IKPCKBIAJMO.DeprecatedDecryptValue(JHJLBKDKCIB);
        if (s == "")
          return JLLDHLJOAOI;
        float result;
        float.TryParse(s, out result);
        IKPCKBIAJMO.SetFloat(HDAJOEOLHGG, result);
        return result;
      }
      byte[] numArray = IKPCKBIAJMO.DecryptData(HDAJOEOLHGG, JHJLBKDKCIB);
      return numArray == null ? JLLDHLJOAOI : BitConverter.ToSingle(numArray, 0);
    }

    public static void SetDouble(string HDAJOEOLHGG, double PDGAOEAMDCL) => PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), IKPCKBIAJMO.EncryptDoubleValue(HDAJOEOLHGG, PDGAOEAMDCL));

    public static double GetDouble(string HDAJOEOLHGG) => IKPCKBIAJMO.GetDouble(HDAJOEOLHGG, 0.0);

    public static double GetDouble(string HDAJOEOLHGG, double JLLDHLJOAOI)
    {
      string encryptedPrefsString = IKPCKBIAJMO.GetEncryptedPrefsString(HDAJOEOLHGG, IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG));
      return !(encryptedPrefsString == "{not_found}") ? IKPCKBIAJMO.DecryptDoubleValue(HDAJOEOLHGG, encryptedPrefsString, JLLDHLJOAOI) : JLLDHLJOAOI;
    }

    private static string EncryptDoubleValue(string HDAJOEOLHGG, double PDGAOEAMDCL)
    {
      byte[] bytes = BitConverter.GetBytes(PDGAOEAMDCL);
      return IKPCKBIAJMO.EncryptData(HDAJOEOLHGG, bytes, IKPCKBIAJMO.AIHKHLNLINM.Double);
    }

    private static double DecryptDoubleValue(
      string HDAJOEOLHGG,
      string JHJLBKDKCIB,
      double JLLDHLJOAOI)
    {
      if (JHJLBKDKCIB.IndexOf(':') > -1)
      {
        string s = IKPCKBIAJMO.DeprecatedDecryptValue(JHJLBKDKCIB);
        if (s == "")
          return JLLDHLJOAOI;
        double result;
        double.TryParse(s, out result);
        IKPCKBIAJMO.SetDouble(HDAJOEOLHGG, result);
        return result;
      }
      byte[] numArray = IKPCKBIAJMO.DecryptData(HDAJOEOLHGG, JHJLBKDKCIB);
      return numArray == null ? JLLDHLJOAOI : BitConverter.ToDouble(numArray, 0);
    }

    public static void SetDecimal(string HDAJOEOLHGG, Decimal PDGAOEAMDCL) => PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), IKPCKBIAJMO.EncryptDecimalValue(HDAJOEOLHGG, PDGAOEAMDCL));

    public static Decimal GetDecimal(string HDAJOEOLHGG) => IKPCKBIAJMO.GetDecimal(HDAJOEOLHGG, 0M);

    public static Decimal GetDecimal(string HDAJOEOLHGG, Decimal JLLDHLJOAOI)
    {
      string encryptedPrefsString = IKPCKBIAJMO.GetEncryptedPrefsString(HDAJOEOLHGG, IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG));
      return !(encryptedPrefsString == "{not_found}") ? IKPCKBIAJMO.DecryptDecimalValue(HDAJOEOLHGG, encryptedPrefsString, JLLDHLJOAOI) : JLLDHLJOAOI;
    }

    private static string EncryptDecimalValue(string HDAJOEOLHGG, Decimal PDGAOEAMDCL)
    {
      byte[] bytes = OMJDMPIJJDN.GetBytes(PDGAOEAMDCL);
      return IKPCKBIAJMO.EncryptData(HDAJOEOLHGG, bytes, IKPCKBIAJMO.AIHKHLNLINM.Decimal);
    }

    private static Decimal DecryptDecimalValue(
      string HDAJOEOLHGG,
      string JHJLBKDKCIB,
      Decimal JLLDHLJOAOI)
    {
      if (JHJLBKDKCIB.IndexOf(':') > -1)
      {
        string s = IKPCKBIAJMO.DeprecatedDecryptValue(JHJLBKDKCIB);
        if (s == "")
          return JLLDHLJOAOI;
        Decimal result;
        Decimal.TryParse(s, out result);
        IKPCKBIAJMO.SetDecimal(HDAJOEOLHGG, result);
        return result;
      }
      byte[] DOJDGDHBMIP = IKPCKBIAJMO.DecryptData(HDAJOEOLHGG, JHJLBKDKCIB);
      return DOJDGDHBMIP == null ? JLLDHLJOAOI : OMJDMPIJJDN.ToDecimal(DOJDGDHBMIP);
    }

    public static void SetLong(string HDAJOEOLHGG, long PDGAOEAMDCL) => PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), IKPCKBIAJMO.EncryptLongValue(HDAJOEOLHGG, PDGAOEAMDCL));

    public static long GetLong(string HDAJOEOLHGG) => IKPCKBIAJMO.GetLong(HDAJOEOLHGG, 0L);

    public static long GetLong(string HDAJOEOLHGG, long JLLDHLJOAOI)
    {
      string encryptedPrefsString = IKPCKBIAJMO.GetEncryptedPrefsString(HDAJOEOLHGG, IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG));
      return !(encryptedPrefsString == "{not_found}") ? IKPCKBIAJMO.DecryptLongValue(HDAJOEOLHGG, encryptedPrefsString, JLLDHLJOAOI) : JLLDHLJOAOI;
    }

    private static string EncryptLongValue(string HDAJOEOLHGG, long PDGAOEAMDCL)
    {
      byte[] bytes = BitConverter.GetBytes(PDGAOEAMDCL);
      return IKPCKBIAJMO.EncryptData(HDAJOEOLHGG, bytes, IKPCKBIAJMO.AIHKHLNLINM.Long);
    }

    private static long DecryptLongValue(string HDAJOEOLHGG, string JHJLBKDKCIB, long JLLDHLJOAOI)
    {
      if (JHJLBKDKCIB.IndexOf(':') > -1)
      {
        string s = IKPCKBIAJMO.DeprecatedDecryptValue(JHJLBKDKCIB);
        if (s == "")
          return JLLDHLJOAOI;
        long result;
        long.TryParse(s, out result);
        IKPCKBIAJMO.SetLong(HDAJOEOLHGG, result);
        return result;
      }
      byte[] numArray = IKPCKBIAJMO.DecryptData(HDAJOEOLHGG, JHJLBKDKCIB);
      return numArray == null ? JLLDHLJOAOI : BitConverter.ToInt64(numArray, 0);
    }

    public static void SetULong(string HDAJOEOLHGG, ulong PDGAOEAMDCL) => PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), IKPCKBIAJMO.EncryptULongValue(HDAJOEOLHGG, PDGAOEAMDCL));

    public static ulong GetULong(string HDAJOEOLHGG) => IKPCKBIAJMO.GetULong(HDAJOEOLHGG, 0UL);

    public static ulong GetULong(string HDAJOEOLHGG, ulong JLLDHLJOAOI)
    {
      string encryptedPrefsString = IKPCKBIAJMO.GetEncryptedPrefsString(HDAJOEOLHGG, IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG));
      return !(encryptedPrefsString == "{not_found}") ? IKPCKBIAJMO.DecryptULongValue(HDAJOEOLHGG, encryptedPrefsString, JLLDHLJOAOI) : JLLDHLJOAOI;
    }

    private static string EncryptULongValue(string HDAJOEOLHGG, ulong PDGAOEAMDCL)
    {
      byte[] bytes = BitConverter.GetBytes(PDGAOEAMDCL);
      return IKPCKBIAJMO.EncryptData(HDAJOEOLHGG, bytes, IKPCKBIAJMO.AIHKHLNLINM.ULong);
    }

    private static ulong DecryptULongValue(
      string HDAJOEOLHGG,
      string JHJLBKDKCIB,
      ulong JLLDHLJOAOI)
    {
      if (JHJLBKDKCIB.IndexOf(':') > -1)
      {
        string s = IKPCKBIAJMO.DeprecatedDecryptValue(JHJLBKDKCIB);
        if (s == "")
          return JLLDHLJOAOI;
        ulong result;
        ulong.TryParse(s, out result);
        IKPCKBIAJMO.SetULong(HDAJOEOLHGG, result);
        return result;
      }
      byte[] numArray = IKPCKBIAJMO.DecryptData(HDAJOEOLHGG, JHJLBKDKCIB);
      return numArray == null ? JLLDHLJOAOI : BitConverter.ToUInt64(numArray, 0);
    }

    public static void SetBool(string HDAJOEOLHGG, bool PDGAOEAMDCL) => PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), IKPCKBIAJMO.EncryptBoolValue(HDAJOEOLHGG, PDGAOEAMDCL));

    public static bool GetBool(string HDAJOEOLHGG) => IKPCKBIAJMO.GetBool(HDAJOEOLHGG, false);

    public static bool GetBool(string HDAJOEOLHGG, bool JLLDHLJOAOI)
    {
      string encryptedPrefsString = IKPCKBIAJMO.GetEncryptedPrefsString(HDAJOEOLHGG, IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG));
      return !(encryptedPrefsString == "{not_found}") ? IKPCKBIAJMO.DecryptBoolValue(HDAJOEOLHGG, encryptedPrefsString, JLLDHLJOAOI) : JLLDHLJOAOI;
    }

    private static string EncryptBoolValue(string HDAJOEOLHGG, bool PDGAOEAMDCL)
    {
      byte[] bytes = BitConverter.GetBytes(PDGAOEAMDCL);
      return IKPCKBIAJMO.EncryptData(HDAJOEOLHGG, bytes, IKPCKBIAJMO.AIHKHLNLINM.Bool);
    }

    private static bool DecryptBoolValue(string HDAJOEOLHGG, string JHJLBKDKCIB, bool JLLDHLJOAOI)
    {
      if (JHJLBKDKCIB.IndexOf(':') > -1)
      {
        string s = IKPCKBIAJMO.DeprecatedDecryptValue(JHJLBKDKCIB);
        if (s == "")
          return JLLDHLJOAOI;
        int result;
        int.TryParse(s, out result);
        IKPCKBIAJMO.SetBool(HDAJOEOLHGG, result == 1);
        return result == 1;
      }
      byte[] numArray = IKPCKBIAJMO.DecryptData(HDAJOEOLHGG, JHJLBKDKCIB);
      return numArray == null ? JLLDHLJOAOI : BitConverter.ToBoolean(numArray, 0);
    }

    public static void SetByteArray(string HDAJOEOLHGG, byte[] PDGAOEAMDCL) => PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), IKPCKBIAJMO.EncryptByteArrayValue(HDAJOEOLHGG, PDGAOEAMDCL));

    public static byte[] GetByteArray(string HDAJOEOLHGG) => IKPCKBIAJMO.GetByteArray(HDAJOEOLHGG, (byte) 0, 0);

    public static byte[] GetByteArray(string HDAJOEOLHGG, byte JLLDHLJOAOI, int KFNFEDEIOCI)
    {
      string encryptedPrefsString = IKPCKBIAJMO.GetEncryptedPrefsString(HDAJOEOLHGG, IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG));
      return encryptedPrefsString == "{not_found}" ? IKPCKBIAJMO.ConstructByteArray(JLLDHLJOAOI, KFNFEDEIOCI) : IKPCKBIAJMO.DecryptByteArrayValue(HDAJOEOLHGG, encryptedPrefsString, JLLDHLJOAOI, KFNFEDEIOCI);
    }

    private static string EncryptByteArrayValue(string HDAJOEOLHGG, byte[] PDGAOEAMDCL) => IKPCKBIAJMO.EncryptData(HDAJOEOLHGG, PDGAOEAMDCL, IKPCKBIAJMO.AIHKHLNLINM.ByteArray);

    private static byte[] DecryptByteArrayValue(
      string HDAJOEOLHGG,
      string JHJLBKDKCIB,
      byte JLLDHLJOAOI,
      int KFNFEDEIOCI)
    {
      if (JHJLBKDKCIB.IndexOf(':') <= -1)
        return IKPCKBIAJMO.DecryptData(HDAJOEOLHGG, JHJLBKDKCIB) ?? IKPCKBIAJMO.ConstructByteArray(JLLDHLJOAOI, KFNFEDEIOCI);
      string s = IKPCKBIAJMO.DeprecatedDecryptValue(JHJLBKDKCIB);
      if (s == "")
        return IKPCKBIAJMO.ConstructByteArray(JLLDHLJOAOI, KFNFEDEIOCI);
      byte[] bytes = Encoding.UTF8.GetBytes(s);
      IKPCKBIAJMO.SetByteArray(HDAJOEOLHGG, bytes);
      return bytes;
    }

    private static byte[] ConstructByteArray(byte PDGAOEAMDCL, int DODMEMAIKJJ)
    {
      byte[] numArray = new byte[DODMEMAIKJJ];
      for (int index = 0; index < DODMEMAIKJJ; ++index)
        numArray[index] = PDGAOEAMDCL;
      return numArray;
    }

    public static void SetVector2(string HDAJOEOLHGG, Vector2 PDGAOEAMDCL) => PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), IKPCKBIAJMO.EncryptVector2Value(HDAJOEOLHGG, PDGAOEAMDCL));

    public static Vector2 GetVector2(string HDAJOEOLHGG) => IKPCKBIAJMO.GetVector2(HDAJOEOLHGG, Vector2.zero);

    public static Vector2 GetVector2(string HDAJOEOLHGG, Vector2 JLLDHLJOAOI)
    {
      string encryptedPrefsString = IKPCKBIAJMO.GetEncryptedPrefsString(HDAJOEOLHGG, IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG));
      return !(encryptedPrefsString == "{not_found}") ? IKPCKBIAJMO.DecryptVector2Value(HDAJOEOLHGG, encryptedPrefsString, JLLDHLJOAOI) : JLLDHLJOAOI;
    }

    private static string EncryptVector2Value(string HDAJOEOLHGG, Vector2 PDGAOEAMDCL)
    {
      byte[] LHEJEHJHFMM = new byte[8];
      Buffer.BlockCopy((Array) BitConverter.GetBytes(PDGAOEAMDCL.x), 0, (Array) LHEJEHJHFMM, 0, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(PDGAOEAMDCL.y), 0, (Array) LHEJEHJHFMM, 4, 4);
      return IKPCKBIAJMO.EncryptData(HDAJOEOLHGG, LHEJEHJHFMM, IKPCKBIAJMO.AIHKHLNLINM.Vector2);
    }

    private static Vector2 DecryptVector2Value(
      string HDAJOEOLHGG,
      string JHJLBKDKCIB,
      Vector2 JLLDHLJOAOI)
    {
      if (JHJLBKDKCIB.IndexOf(':') > -1)
      {
        string str = IKPCKBIAJMO.DeprecatedDecryptValue(JHJLBKDKCIB);
        if (str == "")
          return JLLDHLJOAOI;
        string[] strArray = str.Split("|"[0]);
        float result1;
        float.TryParse(strArray[0], out result1);
        float result2;
        float.TryParse(strArray[1], out result2);
        Vector2 PDGAOEAMDCL = new Vector2(result1, result2);
        IKPCKBIAJMO.SetVector2(HDAJOEOLHGG, PDGAOEAMDCL);
        return PDGAOEAMDCL;
      }
      byte[] numArray = IKPCKBIAJMO.DecryptData(HDAJOEOLHGG, JHJLBKDKCIB);
      if (numArray == null)
        return JLLDHLJOAOI;
      Vector2 vector2;
      vector2.x = BitConverter.ToSingle(numArray, 0);
      vector2.y = BitConverter.ToSingle(numArray, 4);
      return vector2;
    }

    public static void SetVector3(string HDAJOEOLHGG, Vector3 PDGAOEAMDCL) => PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), IKPCKBIAJMO.EncryptVector3Value(HDAJOEOLHGG, PDGAOEAMDCL));

    public static Vector3 GetVector3(string HDAJOEOLHGG) => IKPCKBIAJMO.GetVector3(HDAJOEOLHGG, Vector3.zero);

    public static Vector3 GetVector3(string HDAJOEOLHGG, Vector3 JLLDHLJOAOI)
    {
      string encryptedPrefsString = IKPCKBIAJMO.GetEncryptedPrefsString(HDAJOEOLHGG, IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG));
      return !(encryptedPrefsString == "{not_found}") ? IKPCKBIAJMO.DecryptVector3Value(HDAJOEOLHGG, encryptedPrefsString, JLLDHLJOAOI) : JLLDHLJOAOI;
    }

    private static string EncryptVector3Value(string HDAJOEOLHGG, Vector3 PDGAOEAMDCL)
    {
      byte[] LHEJEHJHFMM = new byte[12];
      Buffer.BlockCopy((Array) BitConverter.GetBytes(PDGAOEAMDCL.x), 0, (Array) LHEJEHJHFMM, 0, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(PDGAOEAMDCL.y), 0, (Array) LHEJEHJHFMM, 4, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(PDGAOEAMDCL.z), 0, (Array) LHEJEHJHFMM, 8, 4);
      return IKPCKBIAJMO.EncryptData(HDAJOEOLHGG, LHEJEHJHFMM, IKPCKBIAJMO.AIHKHLNLINM.Vector3);
    }

    private static Vector3 DecryptVector3Value(
      string HDAJOEOLHGG,
      string JHJLBKDKCIB,
      Vector3 JLLDHLJOAOI)
    {
      if (JHJLBKDKCIB.IndexOf(':') > -1)
      {
        string str = IKPCKBIAJMO.DeprecatedDecryptValue(JHJLBKDKCIB);
        if (str == "")
          return JLLDHLJOAOI;
        string[] strArray = str.Split("|"[0]);
        float result1;
        float.TryParse(strArray[0], out result1);
        float result2;
        float.TryParse(strArray[1], out result2);
        float result3;
        float.TryParse(strArray[2], out result3);
        Vector3 PDGAOEAMDCL = new Vector3(result1, result2, result3);
        IKPCKBIAJMO.SetVector3(HDAJOEOLHGG, PDGAOEAMDCL);
        return PDGAOEAMDCL;
      }
      byte[] numArray = IKPCKBIAJMO.DecryptData(HDAJOEOLHGG, JHJLBKDKCIB);
      if (numArray == null)
        return JLLDHLJOAOI;
      Vector3 vector3;
      vector3.x = BitConverter.ToSingle(numArray, 0);
      vector3.y = BitConverter.ToSingle(numArray, 4);
      vector3.z = BitConverter.ToSingle(numArray, 8);
      return vector3;
    }

    public static void SetQuaternion(string HDAJOEOLHGG, Quaternion PDGAOEAMDCL) => PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), IKPCKBIAJMO.EncryptQuaternionValue(HDAJOEOLHGG, PDGAOEAMDCL));

    public static Quaternion GetQuaternion(string HDAJOEOLHGG) => IKPCKBIAJMO.GetQuaternion(HDAJOEOLHGG, Quaternion.identity);

    public static Quaternion GetQuaternion(string HDAJOEOLHGG, Quaternion JLLDHLJOAOI)
    {
      string encryptedPrefsString = IKPCKBIAJMO.GetEncryptedPrefsString(HDAJOEOLHGG, IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG));
      return !(encryptedPrefsString == "{not_found}") ? IKPCKBIAJMO.DecryptQuaternionValue(HDAJOEOLHGG, encryptedPrefsString, JLLDHLJOAOI) : JLLDHLJOAOI;
    }

    private static string EncryptQuaternionValue(string HDAJOEOLHGG, Quaternion PDGAOEAMDCL)
    {
      byte[] LHEJEHJHFMM = new byte[16];
      Buffer.BlockCopy((Array) BitConverter.GetBytes(PDGAOEAMDCL.x), 0, (Array) LHEJEHJHFMM, 0, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(PDGAOEAMDCL.y), 0, (Array) LHEJEHJHFMM, 4, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(PDGAOEAMDCL.z), 0, (Array) LHEJEHJHFMM, 8, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(PDGAOEAMDCL.w), 0, (Array) LHEJEHJHFMM, 12, 4);
      return IKPCKBIAJMO.EncryptData(HDAJOEOLHGG, LHEJEHJHFMM, IKPCKBIAJMO.AIHKHLNLINM.Quaternion);
    }

    private static Quaternion DecryptQuaternionValue(
      string HDAJOEOLHGG,
      string JHJLBKDKCIB,
      Quaternion JLLDHLJOAOI)
    {
      if (JHJLBKDKCIB.IndexOf(':') > -1)
      {
        string str = IKPCKBIAJMO.DeprecatedDecryptValue(JHJLBKDKCIB);
        if (str == "")
          return JLLDHLJOAOI;
        string[] strArray = str.Split("|"[0]);
        float result1;
        float.TryParse(strArray[0], out result1);
        float result2;
        float.TryParse(strArray[1], out result2);
        float result3;
        float.TryParse(strArray[2], out result3);
        float result4;
        float.TryParse(strArray[3], out result4);
        Quaternion PDGAOEAMDCL = new Quaternion(result1, result2, result3, result4);
        IKPCKBIAJMO.SetQuaternion(HDAJOEOLHGG, PDGAOEAMDCL);
        return PDGAOEAMDCL;
      }
      byte[] numArray = IKPCKBIAJMO.DecryptData(HDAJOEOLHGG, JHJLBKDKCIB);
      if (numArray == null)
        return JLLDHLJOAOI;
      Quaternion quaternion;
      quaternion.x = BitConverter.ToSingle(numArray, 0);
      quaternion.y = BitConverter.ToSingle(numArray, 4);
      quaternion.z = BitConverter.ToSingle(numArray, 8);
      quaternion.w = BitConverter.ToSingle(numArray, 12);
      return quaternion;
    }

    public static void SetColor(string HDAJOEOLHGG, Color32 PDGAOEAMDCL)
    {
      uint PDGAOEAMDCL1 = (uint) ((int) PDGAOEAMDCL.a << 24 | (int) PDGAOEAMDCL.r << 16 | (int) PDGAOEAMDCL.g << 8) | (uint) PDGAOEAMDCL.b;
      PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), IKPCKBIAJMO.EncryptColorValue(HDAJOEOLHGG, PDGAOEAMDCL1));
    }

    public static Color32 GetColor(string HDAJOEOLHGG) => IKPCKBIAJMO.GetColor(HDAJOEOLHGG, new Color32((byte) 0, (byte) 0, (byte) 0, (byte) 1));

    public static Color32 GetColor(string HDAJOEOLHGG, Color32 JLLDHLJOAOI)
    {
      string encryptedPrefsString = IKPCKBIAJMO.GetEncryptedPrefsString(HDAJOEOLHGG, IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG));
      if (encryptedPrefsString == "{not_found}")
        return JLLDHLJOAOI;
      int num = (int) IKPCKBIAJMO.DecryptUIntValue(HDAJOEOLHGG, encryptedPrefsString, 16777216U);
      return new Color32((byte) ((uint) num >> 16), (byte) ((uint) num >> 8), (byte) num, (byte) ((uint) num >> 24));
    }

    private static string EncryptColorValue(string HDAJOEOLHGG, uint PDGAOEAMDCL)
    {
      byte[] bytes = BitConverter.GetBytes(PDGAOEAMDCL);
      return IKPCKBIAJMO.EncryptData(HDAJOEOLHGG, bytes, IKPCKBIAJMO.AIHKHLNLINM.Color);
    }

    public static void SetRect(string HDAJOEOLHGG, Rect PDGAOEAMDCL) => PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), IKPCKBIAJMO.EncryptRectValue(HDAJOEOLHGG, PDGAOEAMDCL));

    public static Rect GetRect(string HDAJOEOLHGG) => IKPCKBIAJMO.GetRect(HDAJOEOLHGG, new Rect(0.0f, 0.0f, 0.0f, 0.0f));

    public static Rect GetRect(string HDAJOEOLHGG, Rect JLLDHLJOAOI)
    {
      string encryptedPrefsString = IKPCKBIAJMO.GetEncryptedPrefsString(HDAJOEOLHGG, IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG));
      return !(encryptedPrefsString == "{not_found}") ? IKPCKBIAJMO.DecryptRectValue(HDAJOEOLHGG, encryptedPrefsString, JLLDHLJOAOI) : JLLDHLJOAOI;
    }

    private static string EncryptRectValue(string HDAJOEOLHGG, Rect PDGAOEAMDCL)
    {
      byte[] LHEJEHJHFMM = new byte[16];
      Buffer.BlockCopy((Array) BitConverter.GetBytes(PDGAOEAMDCL.x), 0, (Array) LHEJEHJHFMM, 0, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(PDGAOEAMDCL.y), 0, (Array) LHEJEHJHFMM, 4, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(PDGAOEAMDCL.width), 0, (Array) LHEJEHJHFMM, 8, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(PDGAOEAMDCL.height), 0, (Array) LHEJEHJHFMM, 12, 4);
      return IKPCKBIAJMO.EncryptData(HDAJOEOLHGG, LHEJEHJHFMM, IKPCKBIAJMO.AIHKHLNLINM.Rect);
    }

    private static Rect DecryptRectValue(
      string HDAJOEOLHGG,
      string JHJLBKDKCIB,
      Rect JLLDHLJOAOI)
    {
      if (JHJLBKDKCIB.IndexOf(':') > -1)
      {
        string str = IKPCKBIAJMO.DeprecatedDecryptValue(JHJLBKDKCIB);
        if (str == "")
          return JLLDHLJOAOI;
        string[] strArray = str.Split("|"[0]);
        float result1;
        float.TryParse(strArray[0], out result1);
        float result2;
        float.TryParse(strArray[1], out result2);
        float result3;
        float.TryParse(strArray[2], out result3);
        float result4;
        float.TryParse(strArray[3], out result4);
        Rect PDGAOEAMDCL = new Rect(result1, result2, result3, result4);
        IKPCKBIAJMO.SetRect(HDAJOEOLHGG, PDGAOEAMDCL);
        return PDGAOEAMDCL;
      }
      byte[] numArray = IKPCKBIAJMO.DecryptData(HDAJOEOLHGG, JHJLBKDKCIB);
      if (numArray == null)
        return JLLDHLJOAOI;
      return new Rect()
      {
        x = BitConverter.ToSingle(numArray, 0),
        y = BitConverter.ToSingle(numArray, 4),
        width = BitConverter.ToSingle(numArray, 8),
        height = BitConverter.ToSingle(numArray, 12)
      };
    }

    public static void SetRawValue(string HDAJOEOLHGG, string OJIGEMHFILJ) => PlayerPrefs.SetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG), OJIGEMHFILJ);

    public static string GetRawValue(string HDAJOEOLHGG) => PlayerPrefs.GetString(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG));

    internal static IKPCKBIAJMO.AIHKHLNLINM GetRawValueType(string PDGAOEAMDCL)
    {
      IKPCKBIAJMO.AIHKHLNLINM aihkhlnlinm1 = IKPCKBIAJMO.AIHKHLNLINM.Unknown;
      byte[] numArray;
      try
      {
        numArray = Convert.FromBase64String(PDGAOEAMDCL);
      }
      catch (Exception ex)
      {
        return aihkhlnlinm1;
      }
      if (numArray.Length < 7)
        return aihkhlnlinm1;
      int length = numArray.Length;
      IKPCKBIAJMO.AIHKHLNLINM aihkhlnlinm2 = (IKPCKBIAJMO.AIHKHLNLINM) numArray[length - 7];
      if (numArray[length - 6] > (byte) 10)
        aihkhlnlinm2 = IKPCKBIAJMO.AIHKHLNLINM.Unknown;
      return aihkhlnlinm2;
    }

    internal static string EncryptKey(string HDAJOEOLHGG)
    {
      HDAJOEOLHGG = ObscuredString.EncryptDecrypt(HDAJOEOLHGG, IKPCKBIAJMO.cryptoKey);
      HDAJOEOLHGG = Convert.ToBase64String(Encoding.UTF8.GetBytes(HDAJOEOLHGG));
      return HDAJOEOLHGG;
    }

    public static bool HasKey(string HDAJOEOLHGG) => PlayerPrefs.HasKey(HDAJOEOLHGG) || PlayerPrefs.HasKey(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG));

    public static void DeleteKey(string HDAJOEOLHGG)
    {
      PlayerPrefs.DeleteKey(IKPCKBIAJMO.EncryptKey(HDAJOEOLHGG));
      if (IKPCKBIAJMO.preservePlayerPrefs)
        return;
      PlayerPrefs.DeleteKey(HDAJOEOLHGG);
    }

    public static void DeleteAll() => PlayerPrefs.DeleteAll();

    public static void Save() => PlayerPrefs.Save();

    private static string GetEncryptedPrefsString(string HDAJOEOLHGG, string OLAFPOPAPIA)
    {
      string str = PlayerPrefs.GetString(OLAFPOPAPIA, "{not_found}");
      if (!(str == "{not_found}"))
        return str;
      PlayerPrefs.HasKey(HDAJOEOLHGG);
      return str;
    }

    private static string EncryptData(
      string HDAJOEOLHGG,
      byte[] LHEJEHJHFMM,
      IKPCKBIAJMO.AIHKHLNLINM EGOFKFODHFI)
    {
      int length1 = LHEJEHJHFMM.Length;
      byte[] numArray1 = IKPCKBIAJMO.EncryptDecryptBytes(LHEJEHJHFMM, length1, HDAJOEOLHGG + IKPCKBIAJMO.cryptoKey);
      uint hash = CEENGIAFKBO.CalculateHash(LHEJEHJHFMM, length1, 0U);
      byte[] numArray2 = new byte[4]
      {
        (byte) (hash & (uint) byte.MaxValue),
        (byte) (hash >> 8 & (uint) byte.MaxValue),
        (byte) (hash >> 16 & (uint) byte.MaxValue),
        (byte) (hash >> 24 & (uint) byte.MaxValue)
      };
      byte[] numArray3 = (byte[]) null;
      int length2;
      if (IKPCKBIAJMO.lockToDevice != IKPCKBIAJMO.ODIOCLPJGAG.None)
      {
        length2 = length1 + 11;
        uint nindomambad = IKPCKBIAJMO.NINDOMAMBAD;
        numArray3 = new byte[4]
        {
          (byte) (nindomambad & (uint) byte.MaxValue),
          (byte) (nindomambad >> 8 & (uint) byte.MaxValue),
          (byte) (nindomambad >> 16 & (uint) byte.MaxValue),
          (byte) (nindomambad >> 24 & (uint) byte.MaxValue)
        };
      }
      else
        length2 = length1 + 7;
      byte[] inArray = new byte[length2];
      byte[] numArray4 = inArray;
      int count = length1;
      Buffer.BlockCopy((Array) numArray1, 0, (Array) numArray4, 0, count);
      if (numArray3 != null)
        Buffer.BlockCopy((Array) numArray3, 0, (Array) inArray, length1, 4);
      inArray[length2 - 7] = (byte) EGOFKFODHFI;
      inArray[length2 - 6] = (byte) 2;
      inArray[length2 - 5] = (byte) IKPCKBIAJMO.lockToDevice;
      Buffer.BlockCopy((Array) numArray2, 0, (Array) inArray, length2 - 4, 4);
      return Convert.ToBase64String(inArray);
    }

    internal static byte[] DecryptData(string HDAJOEOLHGG, string JHJLBKDKCIB)
    {
      byte[] numArray1;
      try
      {
        numArray1 = Convert.FromBase64String(JHJLBKDKCIB);
      }
      catch (Exception ex)
      {
        IKPCKBIAJMO.SavesTampered();
        return (byte[]) null;
      }
      if (numArray1.Length == 0)
      {
        IKPCKBIAJMO.SavesTampered();
        return (byte[]) null;
      }
      int length1 = numArray1.Length;
      if (numArray1[length1 - 6] != (byte) 2)
      {
        IKPCKBIAJMO.SavesTampered();
        return (byte[]) null;
      }
      IKPCKBIAJMO.ODIOCLPJGAG odioclpjgag = (IKPCKBIAJMO.ODIOCLPJGAG) numArray1[length1 - 5];
      byte[] numArray2 = new byte[4];
      Buffer.BlockCopy((Array) numArray1, length1 - 4, (Array) numArray2, 0, 4);
      uint num1 = (uint) ((int) numArray2[0] | (int) numArray2[1] << 8 | (int) numArray2[2] << 16 | (int) numArray2[3] << 24);
      uint num2 = 0;
      int length2;
      if (odioclpjgag != IKPCKBIAJMO.ODIOCLPJGAG.None)
      {
        length2 = length1 - 11;
        if (IKPCKBIAJMO.lockToDevice != IKPCKBIAJMO.ODIOCLPJGAG.None)
        {
          byte[] numArray3 = new byte[4];
          Buffer.BlockCopy((Array) numArray1, length2, (Array) numArray3, 0, 4);
          num2 = (uint) ((int) numArray3[0] | (int) numArray3[1] << 8 | (int) numArray3[2] << 16 | (int) numArray3[3] << 24);
        }
      }
      else
        length2 = length1 - 7;
      byte[] DOJDGDHBMIP = new byte[length2];
      Buffer.BlockCopy((Array) numArray1, 0, (Array) DOJDGDHBMIP, 0, length2);
      byte[] DFFKEFLPKGO = IKPCKBIAJMO.EncryptDecryptBytes(DOJDGDHBMIP, length2, HDAJOEOLHGG + IKPCKBIAJMO.cryptoKey);
      if ((int) CEENGIAFKBO.CalculateHash(DFFKEFLPKGO, length2, 0U) != (int) num1)
      {
        IKPCKBIAJMO.SavesTampered();
        return (byte[]) null;
      }
      if (IKPCKBIAJMO.lockToDevice == IKPCKBIAJMO.ODIOCLPJGAG.Strict && num2 == 0U && (!IKPCKBIAJMO.emergencyMode && !IKPCKBIAJMO.readForeignSaves))
        return (byte[]) null;
      if (num2 != 0U && !IKPCKBIAJMO.emergencyMode)
      {
        uint nindomambad = IKPCKBIAJMO.NINDOMAMBAD;
        if ((int) num2 != (int) nindomambad)
        {
          IKPCKBIAJMO.PossibleForeignSavesDetected();
          if (!IKPCKBIAJMO.readForeignSaves)
            return (byte[]) null;
        }
      }
      return DFFKEFLPKGO;
    }

    private static uint CalculateChecksum(string PBAIIOCIFDP)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(PBAIIOCIFDP + IKPCKBIAJMO.cryptoKey);
      return CEENGIAFKBO.CalculateHash(bytes, bytes.Length, 0U);
    }

    private static void SavesTampered()
    {
      if (IKPCKBIAJMO.onAlterationDetected == null)
        return;
      IKPCKBIAJMO.onAlterationDetected();
      IKPCKBIAJMO.onAlterationDetected = (Action) null;
    }

    private static void PossibleForeignSavesDetected()
    {
      if (IKPCKBIAJMO.onPossibleForeignSavesDetected == null || IKPCKBIAJMO.foreignSavesReported)
        return;
      IKPCKBIAJMO.foreignSavesReported = true;
      IKPCKBIAJMO.onPossibleForeignSavesDetected();
    }

    private static string GetDeviceId() => "";

    private static byte[] EncryptDecryptBytes(
      byte[] DOJDGDHBMIP,
      int IFDIBKBFMLF,
      string HDAJOEOLHGG)
    {
      int length = HDAJOEOLHGG.Length;
      byte[] numArray = new byte[IFDIBKBFMLF];
      for (int index = 0; index < IFDIBKBFMLF; ++index)
        numArray[index] = (byte) ((uint) DOJDGDHBMIP[index] ^ (uint) HDAJOEOLHGG[index % length]);
      return numArray;
    }

    private static string DeprecatedDecryptValue(string PDGAOEAMDCL)
    {
      string[] strArray = PDGAOEAMDCL.Split(':');
      if (strArray.Length < 2)
      {
        IKPCKBIAJMO.SavesTampered();
        return "";
      }
      string str1 = strArray[0];
      string str2 = strArray[1];
      byte[] bytes;
      try
      {
        bytes = Convert.FromBase64String(str1);
      }
      catch
      {
        IKPCKBIAJMO.SavesTampered();
        return "";
      }
      string str3 = ObscuredString.EncryptDecrypt(Encoding.UTF8.GetString(bytes, 0, bytes.Length), IKPCKBIAJMO.cryptoKey);
      if (strArray.Length == 3)
      {
        if (str2 != IKPCKBIAJMO.DeprecatedCalculateChecksum(str1 + IKPCKBIAJMO.JEFOAOINNJD))
          IKPCKBIAJMO.SavesTampered();
      }
      else if (strArray.Length == 2)
      {
        if (str2 != IKPCKBIAJMO.DeprecatedCalculateChecksum(str1))
          IKPCKBIAJMO.SavesTampered();
      }
      else
        IKPCKBIAJMO.SavesTampered();
      if (IKPCKBIAJMO.lockToDevice != IKPCKBIAJMO.ODIOCLPJGAG.None && !IKPCKBIAJMO.emergencyMode)
      {
        if (strArray.Length >= 3)
        {
          if (strArray[2] != IKPCKBIAJMO.JEFOAOINNJD)
          {
            if (!IKPCKBIAJMO.readForeignSaves)
              str3 = "";
            IKPCKBIAJMO.PossibleForeignSavesDetected();
          }
        }
        else if (IKPCKBIAJMO.lockToDevice == IKPCKBIAJMO.ODIOCLPJGAG.Strict)
        {
          if (!IKPCKBIAJMO.readForeignSaves)
            str3 = "";
          IKPCKBIAJMO.PossibleForeignSavesDetected();
        }
        else if (str2 != IKPCKBIAJMO.DeprecatedCalculateChecksum(str1))
        {
          if (!IKPCKBIAJMO.readForeignSaves)
            str3 = "";
          IKPCKBIAJMO.PossibleForeignSavesDetected();
        }
      }
      return str3;
    }

    private static string DeprecatedCalculateChecksum(string PBAIIOCIFDP)
    {
      int num1 = 0;
      byte[] bytes = Encoding.UTF8.GetBytes(PBAIIOCIFDP + IKPCKBIAJMO.cryptoKey);
      int length = bytes.Length;
      int num2 = IKPCKBIAJMO.cryptoKey.Length ^ 64;
      for (int index = 0; index < length; ++index)
      {
        byte num3 = bytes[index];
        num1 += (int) num3 + (int) num3 * (index + num2) % 3;
      }
      return num1.ToString("X2");
    }

    private static string JEFOAOINNJD
    {
      get
      {
        if (string.IsNullOrEmpty(IKPCKBIAJMO.deprecatedDeviceId))
          IKPCKBIAJMO.deprecatedDeviceId = IKPCKBIAJMO.DeprecatedCalculateChecksum(IKPCKBIAJMO.BOFDLLABFNL);
        return IKPCKBIAJMO.deprecatedDeviceId;
      }
    }

    internal enum AIHKHLNLINM : byte
    {
      Unknown = 0,
      Int = 5,
      UInt = 10, // 0x0A
      String = 15, // 0x0F
      Float = 20, // 0x14
      Double = 25, // 0x19
      Decimal = 27, // 0x1B
      Long = 30, // 0x1E
      ULong = 32, // 0x20
      Bool = 35, // 0x23
      ByteArray = 40, // 0x28
      Vector2 = 45, // 0x2D
      Vector3 = 50, // 0x32
      Quaternion = 55, // 0x37
      Color = 60, // 0x3C
      Rect = 65, // 0x41
    }

    public enum ODIOCLPJGAG : byte
    {
      None,
      Soft,
      Strict,
    }
  }
}
