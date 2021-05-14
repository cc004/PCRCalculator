using System;
using System.Security;
using UnityEngine;

namespace Cute
{
	public class Application01
	{
		public static string FBHNEJFGEFD => UnityEngine.Application.absoluteURL;

		public static ThreadPriority EGFILKAEJEC
		{
			get
			{
				return UnityEngine.Application.backgroundLoadingPriority;
			}
			set
			{
				UnityEngine.Application.backgroundLoadingPriority = value;
			}
		}

		public static string JLNLPMFHPGB => UnityEngine.Application.identifier;

		public static string MIJJLHJCNAD => UnityEngine.Application.cloudProjectId;

		public static string JBKFMMNMKPM => UnityEngine.Application.companyName;

		public static string GCCGCCCHPHL => UnityEngine.Application.dataPath;

		public static bool AHLICKMKLNB => UnityEngine.Application.genuine;

		public static bool DPHJFMCEGAI => UnityEngine.Application.genuineCheckAvailable;

		public static ApplicationInstallMode CMFEJACNJOM => UnityEngine.Application.installMode;

		public static NetworkReachability JMDNPACJFHI => UnityEngine.Application.internetReachability;

		public static bool EIKOONAIMAA => UnityEngine.Application.isConsolePlatform;

		public static bool PKNHDLMMDPN => UnityEngine.Application.isEditor;

		[Obsolete("This property is deprecated, please use LoadLevelAsync to detect if a specific scene is currently loading.")]
		public static bool NFLJLAGONGK => UnityEngine.Application.isLoadingLevel;

		public static bool OFDHFHKPDBO => UnityEngine.Application.isMobilePlatform;

		[Obsolete("use Application.isEditor instead")]
		public static bool HLKMNMILKFD => UnityEngine.Application.isPlayer;

		public static bool BNDCGKDLADN => UnityEngine.Application.isPlaying;

		public static bool MLNONLPDBJF => UnityEngine.Application.isShowingSplashScreen;

		[Obsolete("Use SceneManager.sceneCountInBuildSettings")]
		public static int ONJPIFLAGIO => UnityEngine.Application.levelCount;

		[Obsolete("Use SceneManager to determine what scenes have been loaded")]
		public static int INILAOPCBIN => UnityEngine.Application.loadedLevel;

		[Obsolete("Use SceneManager to determine what scenes have been loaded")]
		public static string JBNCCKMKNKF => UnityEngine.Application.loadedLevelName;

		public static string persistentDataPath01
		{
			[SecurityCritical]
			get
			{
				return UnityEngine.Application.persistentDataPath;
			}
		}

		public static RuntimePlatform JGLFFCABMAC => UnityEngine.Application.platform;

		public static string LLKNJAKKFKA => UnityEngine.Application.productName;

		public static bool PNMNPGJIAID
		{
			get
			{
				return UnityEngine.Application.runInBackground;
			}
			set
			{
				UnityEngine.Application.runInBackground = value;
			}
		}

		public static ApplicationSandboxType HDMIOFDHMJN => UnityEngine.Application.sandboxType;

		public static StackTraceLogType KAPDCJFEAAB
		{
			get
			{
				return UnityEngine.Application.stackTraceLogType;
			}
			set
			{
				UnityEngine.Application.stackTraceLogType = value;
			}
		}

		public static int CICGCPNKIOD => UnityEngine.Application.streamedBytes;

		public static string DCHALKJIFMI => UnityEngine.Application.streamingAssetsPath;

		public static SystemLanguage OANEKKMOMLJ => UnityEngine.Application.systemLanguage;

		public static int targetFrameRate01
		{
			get
			{
				return UnityEngine.Application.targetFrameRate;
			}
			set
			{
				UnityEngine.Application.targetFrameRate = value;
			}
		}

		public static string NCAOHADCBKC => UnityEngine.Application.temporaryCachePath;

		public static string LGLLDNGLAPF => UnityEngine.Application.unityVersion;

		public static string AFGBOOMJMOC => UnityEngine.Application.version;

		public static event UnityEngine.Application.LogCallback IHGLEGFMLLJ
		{
			add
			{
				UnityEngine.Application.logMessageReceived += value;
			}
			remove
			{
				UnityEngine.Application.logMessageReceived -= value;
			}
		}

		public static event UnityEngine.Application.LogCallback HKBBHEICKNN
		{
			add
			{
				UnityEngine.Application.logMessageReceivedThreaded += value;
			}
			remove
			{
				UnityEngine.Application.logMessageReceivedThreaded -= value;
			}
		}

		public static event UnityEngine.Application.LowMemoryCallback FEGBLHLGGEM
		{
			add
			{
				UnityEngine.Application.lowMemory += value;
			}
			remove
			{
				UnityEngine.Application.lowMemory -= value;
			}
		}

		public static void CancelQuit()
		{
			UnityEngine.Application.CancelQuit();
		}

		public static bool CanStreamedLevelBeLoaded(int BJBBCGBCDGE)
		{
			return UnityEngine.Application.CanStreamedLevelBeLoaded(BJBBCGBCDGE);
		}

		public static bool CanStreamedLevelBeLoaded(string BFHLBJDLHLE)
		{
			return UnityEngine.Application.CanStreamedLevelBeLoaded(BFHLBJDLHLE);
		}

		public static void CaptureScreenshot(string NACGNDIHOBK)
		{
			ScreenCapture.CaptureScreenshot(NACGNDIHOBK);
		}

		public static void CaptureScreenshot(string NACGNDIHOBK, int BLADPNKFCFO)
		{
			ScreenCapture.CaptureScreenshot(NACGNDIHOBK, BLADPNKFCFO);
		}

		[Obsolete("Use Object.DontDestroyOnLoad instead")]
		public static void DontDestroyOnLoad(UnityEngine.Object JFBEPGMFCDL)
		{
			UnityEngine.Application.DontDestroyOnLoad(JFBEPGMFCDL);
		}

		public static void ExternalCall(string ADIAIMIJCNC, params object[] KMHKMIMACEM)
		{
			UnityEngine.Application.ExternalCall(ADIAIMIJCNC, KMHKMIMACEM);
		}

		public static void ExternalEval(string IAOCGGPDPGE)
		{
			UnityEngine.Application.ExternalEval(IAOCGGPDPGE);
		}

		[Obsolete("For internal use only")]
		public static void ForceCrash(int ILDOCAMALCK)
		{
			UnityEngine.Application.ForceCrash(ILDOCAMALCK);
		}

		public static float GetStreamProgressForLevel(int BJBBCGBCDGE)
		{
			return UnityEngine.Application.GetStreamProgressForLevel(BJBBCGBCDGE);
		}

		public static float GetStreamProgressForLevel(string BFHLBJDLHLE)
		{
			return UnityEngine.Application.GetStreamProgressForLevel(BFHLBJDLHLE);
		}

		public static bool HasProLicense()
		{
			return UnityEngine.Application.HasProLicense();
		}

		public static bool HasUserAuthorization(UserAuthorization ILDOCAMALCK)
		{
			return UnityEngine.Application.HasUserAuthorization(ILDOCAMALCK);
		}

		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevel(int ELNABNNHPPH)
		{
			UnityEngine.Application.LoadLevel(ELNABNNHPPH);
		}

		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevel(string CIAJGHECFHJ)
		{
			UnityEngine.Application.LoadLevel(CIAJGHECFHJ);
		}

		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevelAdditive(int ELNABNNHPPH)
		{
			UnityEngine.Application.LoadLevelAdditive(ELNABNNHPPH);
		}

		[Obsolete("Use SceneManager.LoadScene")]
		public static void LoadLevelAdditive(string CIAJGHECFHJ)
		{
			UnityEngine.Application.LoadLevelAdditive(CIAJGHECFHJ);
		}

		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAdditiveAsync(int ELNABNNHPPH)
		{
			return UnityEngine.Application.LoadLevelAdditiveAsync(ELNABNNHPPH);
		}

		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAdditiveAsync(string BFHLBJDLHLE)
		{
			return UnityEngine.Application.LoadLevelAdditiveAsync(BFHLBJDLHLE);
		}

		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAsync(int ELNABNNHPPH)
		{
			return UnityEngine.Application.LoadLevelAsync(ELNABNNHPPH);
		}

		[Obsolete("Use SceneManager.LoadSceneAsync")]
		public static AsyncOperation LoadLevelAsync(string BFHLBJDLHLE)
		{
			return UnityEngine.Application.LoadLevelAsync(BFHLBJDLHLE);
		}

		public static void OpenURL(string POBPFGHNOBF)
		{
			UnityEngine.Application.OpenURL(POBPFGHNOBF);
		}

		public static void Quit()
		{
			UnityEngine.Application.Quit();
		}

		[Obsolete("Application.RegisterLogCallback is deprecated. Use Application.logMessageReceived instead.")]
		public static void RegisterLogCallback(UnityEngine.Application.LogCallback PGJINEBMBCB)
		{
			UnityEngine.Application.RegisterLogCallback(PGJINEBMBCB);
		}

		[Obsolete("Application.RegisterLogCallbackThreaded is deprecated. Use Application.logMessageReceivedThreaded instead.")]
		public static void RegisterLogCallbackThreaded(UnityEngine.Application.LogCallback PGJINEBMBCB)
		{
			UnityEngine.Application.RegisterLogCallbackThreaded(PGJINEBMBCB);
		}

		public static bool RequestAdvertisingIdentifierAsync(UnityEngine.Application.AdvertisingIdentifierCallback BMKEPKKGBKC)
		{
			return UnityEngine.Application.RequestAdvertisingIdentifierAsync(BMKEPKKGBKC);
		}

		public static AsyncOperation RequestUserAuthorization(UserAuthorization ILDOCAMALCK)
		{
			return UnityEngine.Application.RequestUserAuthorization(ILDOCAMALCK);
		}

		[Obsolete("Use SceneManager.UnloadScene")]
		public static bool UnloadLevel(int ELNABNNHPPH)
		{
			return UnityEngine.Application.UnloadLevel(ELNABNNHPPH);
		}

		[Obsolete("Use SceneManager.UnloadScene")]
		public static bool UnloadLevel(string PPLCEEONDPE)
		{
			return UnityEngine.Application.UnloadLevel(PPLCEEONDPE);
		}
	}
}
