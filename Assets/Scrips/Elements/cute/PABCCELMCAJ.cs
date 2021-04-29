// Decompiled with JetBrains decompiler
// Type: Cute.PABCCELMCAJ
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Security;
using UnityEngine;

namespace Cute
{
  public class PABCCELMCAJ
  {
    public static string FBHNEJFGEFD => Application.absoluteURL;

    public static ThreadPriority EGFILKAEJEC
    {
      get => Application.backgroundLoadingPriority;
      set => Application.backgroundLoadingPriority = value;
    }

    public static string JLNLPMFHPGB => Application.identifier;

    public static string MIJJLHJCNAD => Application.cloudProjectId;

    public static string JBKFMMNMKPM => Application.companyName;

    public static string GCCGCCCHPHL => Application.dataPath;

    public static bool AHLICKMKLNB => Application.genuine;

    public static bool DPHJFMCEGAI => Application.genuineCheckAvailable;

    public static ApplicationInstallMode CMFEJACNJOM => Application.installMode;

    public static NetworkReachability JMDNPACJFHI => Application.internetReachability;

    public static bool EIKOONAIMAA => Application.isConsolePlatform;

    public static bool PKNHDLMMDPN => Application.isEditor;

    [Obsolete("This property is deprecated, please use LoadLevelAsync to detect if a specific scene is currently loading.")]
    public static bool NFLJLAGONGK => Application.isLoadingLevel;

    public static bool OFDHFHKPDBO => Application.isMobilePlatform;

    [Obsolete("use Application.isEditor instead")]
    public static bool HLKMNMILKFD => Application.isPlayer;

    public static bool BNDCGKDLADN => Application.isPlaying;

    public static bool MLNONLPDBJF => Application.isShowingSplashScreen;

    [Obsolete("Use SceneManager.sceneCountInBuildSettings")]
    public static int ONJPIFLAGIO => Application.levelCount;

    [Obsolete("Use SceneManager to determine what scenes have been loaded")]
    public static int INILAOPCBIN => Application.loadedLevel;

    [Obsolete("Use SceneManager to determine what scenes have been loaded")]
    public static string JBNCCKMKNKF => Application.loadedLevelName;

    public static string LHMAODLAGNH
    {
      [SecurityCritical] get => Application.persistentDataPath;
    }

    public static RuntimePlatform JGLFFCABMAC => Application.platform;

    public static string LLKNJAKKFKA => Application.productName;

    public static bool PNMNPGJIAID
    {
      get => Application.runInBackground;
      set => Application.runInBackground = value;
    }

    public static ApplicationSandboxType HDMIOFDHMJN => Application.sandboxType;

    public static StackTraceLogType KAPDCJFEAAB
    {
      get => Application.stackTraceLogType;
      set => Application.stackTraceLogType = value;
    }

    public static int CICGCPNKIOD => Application.streamedBytes;

    public static string DCHALKJIFMI => Application.streamingAssetsPath;

    public static SystemLanguage OANEKKMOMLJ => Application.systemLanguage;

    public static int IKMGFNGHDPJ
    {
      get => Application.targetFrameRate;
      set => Application.targetFrameRate = value;
    }

    public static string NCAOHADCBKC => Application.temporaryCachePath;

    public static string LGLLDNGLAPF => Application.unityVersion;

    public static string AFGBOOMJMOC => Application.version;

    public static event Application.LogCallback IHGLEGFMLLJ
    {
      add => Application.logMessageReceived += value;
      remove => Application.logMessageReceived -= value;
    }

    public static event Application.LogCallback HKBBHEICKNN
    {
      add => Application.logMessageReceivedThreaded += value;
      remove => Application.logMessageReceivedThreaded -= value;
    }

    public static event Application.LowMemoryCallback FEGBLHLGGEM
    {
      add => Application.lowMemory += value;
      remove => Application.lowMemory -= value;
    }

    public static void CancelQuit() => Application.CancelQuit();

    public static bool CanStreamedLevelBeLoaded(int BJBBCGBCDGE) => Application.CanStreamedLevelBeLoaded(BJBBCGBCDGE);

    public static bool CanStreamedLevelBeLoaded(string BFHLBJDLHLE) => Application.CanStreamedLevelBeLoaded(BFHLBJDLHLE);

    public static void CaptureScreenshot(string NACGNDIHOBK) => ScreenCapture.CaptureScreenshot(NACGNDIHOBK);

    public static void CaptureScreenshot(string NACGNDIHOBK, int BLADPNKFCFO) => ScreenCapture.CaptureScreenshot(NACGNDIHOBK, BLADPNKFCFO);

    [Obsolete("Use Object.DontDestroyOnLoad instead")]
    public static void DontDestroyOnLoad(UnityEngine.Object JFBEPGMFCDL) => Application.DontDestroyOnLoad(JFBEPGMFCDL);

    public static void ExternalCall(string ADIAIMIJCNC, params object[] KMHKMIMACEM) => Application.ExternalCall(ADIAIMIJCNC, KMHKMIMACEM);

    public static void ExternalEval(string IAOCGGPDPGE) => Application.ExternalEval(IAOCGGPDPGE);

    [Obsolete("For internal use only")]
    public static void ForceCrash(int ILDOCAMALCK) => Application.ForceCrash(ILDOCAMALCK);

    public static float GetStreamProgressForLevel(int BJBBCGBCDGE) => Application.GetStreamProgressForLevel(BJBBCGBCDGE);

    public static float GetStreamProgressForLevel(string BFHLBJDLHLE) => Application.GetStreamProgressForLevel(BFHLBJDLHLE);

    public static bool HasProLicense() => Application.HasProLicense();

    public static bool HasUserAuthorization(UserAuthorization ILDOCAMALCK) => Application.HasUserAuthorization(ILDOCAMALCK);

    [Obsolete("Use SceneManager.LoadScene")]
    public static void LoadLevel(int ELNABNNHPPH) => Application.LoadLevel(ELNABNNHPPH);

    [Obsolete("Use SceneManager.LoadScene")]
    public static void LoadLevel(string CIAJGHECFHJ) => Application.LoadLevel(CIAJGHECFHJ);

    [Obsolete("Use SceneManager.LoadScene")]
    public static void LoadLevelAdditive(int ELNABNNHPPH) => Application.LoadLevelAdditive(ELNABNNHPPH);

    [Obsolete("Use SceneManager.LoadScene")]
    public static void LoadLevelAdditive(string CIAJGHECFHJ) => Application.LoadLevelAdditive(CIAJGHECFHJ);

    [Obsolete("Use SceneManager.LoadSceneAsync")]
    public static AsyncOperation LoadLevelAdditiveAsync(int ELNABNNHPPH) => Application.LoadLevelAdditiveAsync(ELNABNNHPPH);

    [Obsolete("Use SceneManager.LoadSceneAsync")]
    public static AsyncOperation LoadLevelAdditiveAsync(string BFHLBJDLHLE) => Application.LoadLevelAdditiveAsync(BFHLBJDLHLE);

    [Obsolete("Use SceneManager.LoadSceneAsync")]
    public static AsyncOperation LoadLevelAsync(int ELNABNNHPPH) => Application.LoadLevelAsync(ELNABNNHPPH);

    [Obsolete("Use SceneManager.LoadSceneAsync")]
    public static AsyncOperation LoadLevelAsync(string BFHLBJDLHLE) => Application.LoadLevelAsync(BFHLBJDLHLE);

    public static void OpenURL(string POBPFGHNOBF) => Application.OpenURL(POBPFGHNOBF);

    public static void Quit() => Application.Quit();

    [Obsolete("Application.RegisterLogCallback is deprecated. Use Application.logMessageReceived instead.")]
    public static void RegisterLogCallback(Application.LogCallback PGJINEBMBCB) => Application.RegisterLogCallback(PGJINEBMBCB);

    [Obsolete("Application.RegisterLogCallbackThreaded is deprecated. Use Application.logMessageReceivedThreaded instead.")]
    public static void RegisterLogCallbackThreaded(Application.LogCallback PGJINEBMBCB) => Application.RegisterLogCallbackThreaded(PGJINEBMBCB);

    public static bool RequestAdvertisingIdentifierAsync(
      Application.AdvertisingIdentifierCallback BMKEPKKGBKC) => Application.RequestAdvertisingIdentifierAsync(BMKEPKKGBKC);

    public static AsyncOperation RequestUserAuthorization(
      UserAuthorization ILDOCAMALCK) => Application.RequestUserAuthorization(ILDOCAMALCK);

    [Obsolete("Use SceneManager.UnloadScene")]
    public static bool UnloadLevel(int ELNABNNHPPH) => Application.UnloadLevel(ELNABNNHPPH);

    [Obsolete("Use SceneManager.UnloadScene")]
    public static bool UnloadLevel(string PPLCEEONDPE) => Application.UnloadLevel(PPLCEEONDPE);
  }
}
