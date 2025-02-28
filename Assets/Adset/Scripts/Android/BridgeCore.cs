using UnityEngine;
using System.Runtime.InteropServices;
public class BridgeCore
{
    private static AndroidJavaClass osetSDKProtected = new AndroidJavaClass("com.jiagu.sdk.OSETSDKProtected");
    private static AndroidJavaClass unityBridge = new AndroidJavaClass("com.kc.openset.bridge.UnityBridge");
    private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

	#if UNITY_IOS
		[DllImport("__Internal")]
		private static extern void SetIOSBlockClassName(string className);
		[DllImport("__Internal")]
		private static extern void InitIOSSdk(string appKey,string userId);
		[DllImport("__Internal")]
		private static extern void ShowIOSReward(string posId,string userId);
		[DllImport("__Internal")]
		private static extern void ShowIOSSplash(string posId);
		[DllImport("__Internal")]
		private static extern void ShowIOSInsert(string posId);
		[DllImport("__Internal")]
		private static extern void ShowIOSHorizontalInsert(string posId);
		[DllImport("__Internal")]
		private static extern void PreLoadIOSReward(string posId,string userId);
		[DllImport("__Internal")]
		private static extern void PreLoadIOSInsert(string posId,string userId);
		[DllImport("__Internal")]
		private static extern void PreLoadIOSHorizontalInsert(string posId,string userId);
		
		public static void SetBlockClassName(string className)
		{
			SetIOSBlockClassName(className);
		}
	#endif



    public static void InitSdk(string userId,string appKey,IInitResultListener listener)
    {
		#if UNITY_ANDROID
			osetSDKProtected.CallStatic("install", GetApplication());
			unityBridge.CallStatic("initSdk", GetApplication(), userId, appKey, new InitResultCallback(listener));
#endif

#if UNITY_IOS
			InitIOSSdk(appKey,userId);
#endif
    }

	public static void InitSdk(string userId, string appKey, IInitResultListener listener, ICustomController customController)
	{
#if UNITY_ANDROID
        osetSDKProtected.CallStatic("install", GetApplication());
        unityBridge.CallStatic("initSdk", GetApplication(), userId, appKey, new InitResultCallback(listener), new CustomControllerCallback(customController));
#endif

#if UNITY_IOS
			InitIOSSdk(appKey,userId);
#endif
    }


	public static void PreLoadReward(string userId, string posId)
    {
		#if UNITY_ANDROID
			unityBridge.CallStatic("preLoadReward", GetCurrentActivity(), userId, posId);
		#endif
		#if UNITY_IOS
			PreLoadIOSReward(posId,userId);
		#endif
    }

    public static void ShowReward(string userId, IRewardListener listener)
    {
		#if UNITY_ANDROID
			unityBridge.CallStatic("showReward", GetCurrentActivity(), userId, new RewardCallback(listener));
		#endif
		#if UNITY_IOS
			ShowIOSReward("预留参数",userId);
		#endif
    }


    public static void PreLoadInsert(string userId,string posId)
    {
		#if UNITY_ANDROID
			unityBridge.CallStatic("preLoadInsert", GetCurrentActivity(), userId, posId);
		#endif
		#if UNITY_IOS
			PreLoadIOSInsert(posId,userId);
		#endif
    }

    public static void ShowInsert(IInsertListener listener)
    {
		#if UNITY_IOS
			ShowIOSInsert("预留参数");
		#endif
		#if UNITY_ANDROID
			unityBridge.CallStatic("showInsert", GetCurrentActivity(), new InsertCallBack(listener));
		#endif
    }



    public static void PreLoadHorizontalInsert(string userId, string posId)
    {
		#if UNITY_ANDROID
			unityBridge.CallStatic("preLoadHorizontal", GetCurrentActivity(), userId, posId);
		#endif
		#if UNITY_IOS
			PreLoadIOSHorizontalInsert(posId,userId);
		#endif
    }

    public static void ShowHorizontalInsert(IInsertListener listener)
    {
		#if UNITY_ANDROID
			unityBridge.CallStatic("showInsertHorizontal", GetCurrentActivity(),new InsertCallBack(listener));
		#endif
		#if UNITY_IOS
			ShowIOSHorizontalInsert("预留参数");
		#endif
    }

    
    public static void ShowSplash(string posId, ISplashListener listener)
    {
		#if UNITY_ANDROID
			unityBridge.CallStatic("showSplash", GetCurrentActivity(), posId, new SplashCallBack(listener));
		#endif
		#if UNITY_IOS
			ShowIOSSplash(posId);
		#endif
    }


    public static void ShowToast(string message)
    {
		#if UNITY_ANDROID
			unityBridge.CallStatic("showToast", message);
		#endif
		#if UNITY_IOS
			Debug.Log("showToast" + message);
		#endif
    }

    private static AndroidJavaObject GetCurrentActivity()
    {
        return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    }

    private static AndroidJavaObject GetApplication()
    {
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        return currentActivity.Call<AndroidJavaObject>("getApplicationContext");
    }
}
