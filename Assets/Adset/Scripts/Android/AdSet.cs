using UnityEngine;

public class AdSet : MonoBehaviour
{

    AndroidJavaClass androidJavaClass = null;
    public AndroidJavaObject currentActivity = null;

    const string userId = "123";

#if UNITY_ANDROID
    const string AppKey = "519FBC20B23B2EBB";
    const string POS_ID_Splash = "7D5239D8D88EBF9B6D317912EDAC6439";
    const string POS_ID_Insert = "1D273967F51868AF2C4E080D496D06D0";
    const string POS_ID_Insert_Horizontal = "592AFB97E4FD2C63EC23AA781FF6E8B0";
    const string POS_ID_RewardVideo = "0AF4C9250F84820BABCB5DC77192981A";
 #endif
 
 #if UNITY_IOS
  const string AppKey = "31DC084BB6B04838";
  const string POS_ID_Splash = "18666EAA65EC1969E90E982DCA2CB2DD";
  const string POS_ID_Insert = "351C1A89F8AE79DF62C1B1165A5EAFCC";
  const string POS_ID_Insert_Horizontal = "A3E2589819F06436D644A0970E45193E";
  const string POS_ID_RewardVideo = "E80DABEF5FD288492D4A9D05BF84E417";
  
  #endif


    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_IOS
			// 获取当前组件附着的 游戏物体 GameObject
			GameObject gameObject = this.gameObject;
			// 获取当前组件附着的 游戏物体 GameObject 名称
			string name = gameObject.name;
			// 设置iOS回调方法所在类的名称
			BridgeCore.SetBlockClassName(name);
        #endif
        #if UNITY_ANDROID
        	androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        	currentActivity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
        #endif
    }

    public void Init()
    {
        BridgeCore.InitSdk(userId, AppKey, new InitResultListener());
    }

    public class InitResultListener : IInitResultListener
    {
        public void OnError(string message)
        {
            Debug.Log("广告初始化失败->" + message);
        }

        public void OnSuccess()
        {
            Debug.Log("广告初始化成功");
        }
    }




    public void PreLoadReward()
    {
        BridgeCore.PreLoadReward(userId, POS_ID_RewardVideo);
    }

    public void PreLoadInsert()
    {
        BridgeCore.PreLoadInsert(userId, POS_ID_Insert);
    }

    public void PreLoadHorizontalInsert()
    {
        BridgeCore.PreLoadHorizontalInsert(userId, POS_ID_Insert_Horizontal);
    }





    public void ShowReward()
    {
        BridgeCore.ShowReward(userId, new RewardListener());
    }

    public class RewardListener : IRewardListener
    {
        public void OnClick()
        {
            ShowToast("激励广告点击->OnClick");
        }

        public void OnClose(string key)
        {
            ShowToast("激励广告关闭->OnClose");
        }

        public void OnError(string errorCode, string errorMessage)
        {
            ShowToast($"激励广告加载错误->OnError-> errorCode:{errorCode},errorMessage:{errorMessage}");
        }

        public void OnReward(string key, int arg)
        {
            ShowToast("激励广告获取奖励成功->OnReward");
        }

        public void OnServiceResponse(int code)
        {
            // ShowToast("激励广告服务器调用成功->OnServiceResponse");
        }

        public void OnShow(string key)
        {
            ShowToast("激励广告获曝光->OnShow");
        }

        public void OnVideoEnd(string key)
        {
            ShowToast("激励广告视频播放结束->OnVideoEnd");
        }

        public void OnVideoStart()
        {
            ShowToast("激励广告视频开始播放->OnVideoStart");
        }
    }




    public void ShowSplash()
    {
        BridgeCore.ShowSplash(POS_ID_Splash, new SplashListener());
    }

    public class SplashListener : ISplashListener
    {
        public void OnClick()
        {
            ShowToast("开屏广告点击->OnClick");
        }

        public void OnClose()
        {
            ShowToast("开屏广告关闭->OnClose");
        }

        public void OnError(string errorCode, string errorMessage)
        {
            ShowToast($"开屏广告加载错误->OnError-> errorCode:{errorCode},errorMessage:{errorMessage}");
        }

        public void OnShow()
        {
            ShowToast("开屏广告播放->OnShow");
        }
    }




    public void ShowInsert()
    {
        BridgeCore.ShowInsert(new InsertListener());
    }

    public class InsertListener : IInsertListener
    {
        public void OnClick()
        {
            ShowToast("插屏广告点击->OnClick");
        }

        public void OnClose()
        {
            ShowToast("插屏广告关闭->OnClose");
        }

        public void OnError(string errorCode, string errorMessage)
        {
            ShowToast($"插屏广告加载错误->OnError-> errorCode:{errorCode},errorMessage:{errorMessage}");
        }

        public void OnShow()
        {
            ShowToast("插屏广告播放->OnShow");
        }
    }



    public void ShowHorizontalInsert()
    {
        BridgeCore.ShowHorizontalInsert(new HorizontalInsertListener());
    }

    public class HorizontalInsertListener : IInsertListener
    {
        public void OnClick()
        {
            ShowToast("横屏插屏广告点击->OnClick");
        }

        public void OnClose()
        {
            ShowToast("横屏插屏广告关闭->OnClose");
        }

        public void OnError(string errorCode, string errorMessage)
        {
            ShowToast($"横屏插屏广告加载错误->OnError-> errorCode:{errorCode},errorMessage:{errorMessage}");
        }

        public void OnShow()
        {
            ShowToast("横屏插屏广告播放->OnShow");
        }
    }

    private static void ShowToast(string message)
    {
        BridgeCore.ShowToast(message);
    }


	#if UNITY_IOS
		// 初始化
		public void OSETIOSInitOnSuccess()
		{
		    ShowToast("iOS初始化成功");
		}
		public void OSETIOSInitOnError()
		{
		    ShowToast("iOS初始化失败");
		}
		// 开屏
		public void OSETIOSSplashOnError(string error)
		{
		    ShowToast($"iOS开屏加载失败->{error}");
		}
		public void OSETIOSSplashOnShow()
		{
		    ShowToast("iOS开屏展示");
		}
		public void OSETIOSSplashOnClick()
		{
		    ShowToast("iOS开屏点击");
		}
		public void OSETIOSSplashOnClose()
		{
		    ShowToast("iOS开屏关闭");
		}
		
		// 插屏
		public void OSETIOSInsertOnError(string error)
		{
		    ShowToast($"iOS插屏加载失败->{error}");
		}
		public void OSETIOSInsertOnShow()
		{
		    ShowToast("iOS插屏展示");
		}
		public void OSETIOSInsertOnClick()
		{
		    ShowToast("iOS插屏点击");
		}
		public void OSETIOSInsertOnClose()
		{
		    ShowToast("iOS插屏关闭");
		}
		
		// 横屏插屏
		public void OSETIOSHorizontalInsertOnError(string error)
		{
		    ShowToast($"iOS横屏插屏加载失败->{error}");
		}
		public void OSETIOSHorizontalInsertOnShow()
		{
		    ShowToast("iOS横屏插屏展示");
		}
		public void OSETIOSHorizontalInsertOnClick()
		{
		    ShowToast("iOS横屏插屏点击");
		}
		public void OSETIOSHorizontalInsertOnClose()
		{
		    ShowToast("iOS横屏插屏关闭");
		}
		
		// 激励
		public void OSETIOSRewardOnError(string error)
		{
		    ShowToast($"iOS激励加载失败->{error}");
		}
		public void OSETIOSRewardOnSuccess()
		{
		    ShowToast("iOS激励加载成功");
		}
		public void OSETIOSRewardOnClick()
		{
		    ShowToast("iOS激励点击");
		}
		public void OSETIOSRewardOnClose()
		{
		    ShowToast("iOS激励关闭");
		}
		
		public void OSETIOSRewardOnReward(string checkString)
		{
		    ShowToast($"iOS激励达到奖励->{checkString}");
		}
		public void OSETIOSRewardPlayStart()
		{
		    ShowToast("iOS激励开始播放");
		}
		public void OSETIOSRewardPlayEnd()
		{
		    ShowToast("iOS激励播放结束");
		}
		public void OSETIOSRewardPlayError()
		{
		    ShowToast("iOS激励播放失败");
		}
		
		
	#endif
}
