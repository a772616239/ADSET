using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.Callbacks;
using System.IO;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
#endif

public class BuildCallback : IPreprocessBuildWithReport, IPostprocessBuildWithReport
{
    int IOrderedCallback.callbackOrder { get { return 0; } }

    System.DateTime startTime;

    //打包前事件
    void IPreprocessBuildWithReport.OnPreprocessBuild(BuildReport report)
    {
        startTime = System.DateTime.Now;
        Debug.Log("开始打包 : " + startTime);
    }

    //打包后事件
    void IPostprocessBuildWithReport.OnPostprocessBuild(BuildReport report)
    {
        System.TimeSpan buildTimeSpan = System.DateTime.Now - startTime;
        Debug.Log("打包成功，耗时 : " + buildTimeSpan);
    }

    //回调  打包后处理
    [PostProcessBuild(1)]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
    {
		if (target != BuildTarget.iOS)
		{
		    return;
		}
#if UNITY_IOS
		var projPath = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";
		var proj = new PBXProject();
		proj.ReadFromFile(projPath);
		
		var targetObjt = proj.GetUnityMainTargetGuid();
		proj.SetBuildProperty(targetObjt, "ENABLE_BITCODE", "NO");
		
		var targetGUID = proj.GetUnityFrameworkTargetGuid();
		proj.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-ObjC");
		proj.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-l\"c++\"");
		proj.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-l\"c++abi\"");
		proj.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-l\"sqlite3\"");
		proj.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-l\"z\"");
		
		proj.SetBuildProperty(targetGUID, "ENABLE_BITCODE", "NO");
		
		// framework to project
		proj.AddFrameworkToProject(targetGUID, "SafariServices.framework", false);
		proj.AddFrameworkToProject(targetGUID, "CFNetwork.framework", false);
		proj.AddFrameworkToProject(targetGUID, "AVFoundation.framework", false);
		proj.AddFrameworkToProject(targetGUID, "WebKit.framework", false);
		proj.AddFrameworkToProject(targetGUID, "StoreKit.framework", false);
		proj.AddFrameworkToProject(targetGUID, "Security.framework", false);
		proj.AddFrameworkToProject(targetGUID, "CoreTelephony.framework", false);
		proj.AddFrameworkToProject(targetGUID, "SystemConfiguration.framework", false);
		proj.AddFrameworkToProject(targetGUID, "QuartzCore.framework", false);
		proj.AddFrameworkToProject(targetGUID, "CoreLocation.framework", false);
		proj.AddFrameworkToProject(targetGUID, "AdSupport.framework", false);
		proj.AddFrameworkToProject(targetGUID, "Accelerate.framework", false);
		proj.AddFrameworkToProject(targetGUID, "ImageIO.framework", false);
		proj.AddFrameworkToProject(targetGUID, "CoreMotion.framework", false);
		proj.AddFrameworkToProject(targetGUID, "CoreMedia.framework", false);
		proj.AddFrameworkToProject(targetGUID, "MediaPlayer.framework", false);
		proj.AddFrameworkToProject(targetGUID, "MobileCoreServices.framework", false);
		proj.AddFrameworkToProject(targetGUID, "MessageUI.framework", false);
		proj.AddFrameworkToProject(targetGUID, "AudioToolbox.framework", false);
		proj.AddFrameworkToProject(targetGUID, "CoreGraphics.framework", false);
		proj.AddFrameworkToProject(targetGUID, "DeviceCheck.framework", false);
		proj.AddFrameworkToProject(targetGUID, "CoreData.framework", false);
		proj.AddFrameworkToProject(targetGUID, "AVKit.framework", false);
		proj.AddFrameworkToProject(targetGUID, "QuickLook.framework", false);
		proj.AddFrameworkToProject(targetGUID, "JavaScriptCore.framework", false);

		
		proj.AddFileToBuild(targetGUID, proj.AddFile("usr/lib/libxml2.tbd", "Frameworks/libxml2.tbd", PBXSourceTree.Sdk));
		proj.AddFileToBuild(targetGUID, proj.AddFile("usr/lib/libz.tbd", "Frameworks/libz.tbd", PBXSourceTree.Sdk));
		proj.AddFileToBuild(targetGUID, proj.AddFile("usr/lib/libsqlite3.tbd", "Frameworks/libsqlite3.tbd", PBXSourceTree.Sdk));
		proj.AddFileToBuild(targetGUID, proj.AddFile("usr/lib/libc++.tbd", "Frameworks/libc++.tbd", PBXSourceTree.Sdk));
		proj.AddFileToBuild(targetGUID, proj.AddFile("usr/lib/libresolv.9.tbd", "Frameworks/libresolv.9.tbd", PBXSourceTree.Sdk));
		proj.AddFileToBuild(targetGUID, proj.AddFile("usr/lib/libxml2.2.tbd", "Frameworks/libxml2.2.tbd", PBXSourceTree.Sdk));
		proj.AddFileToBuild(targetGUID, proj.AddFile("usr/lib/libiconv.tbd", "Frameworks/libiconv.tbd", PBXSourceTree.Sdk));
		proj.AddFileToBuild(targetGUID, proj.AddFile("usr/lib/libc++abi.tbd", "Frameworks/libc++abi.tbd", PBXSourceTree.Sdk));
		proj.AddFileToBuild(targetGUID, proj.AddFile("usr/lib/libbz2.1.0.tbd", "Frameworks/libbz2.1.0.tbd", PBXSourceTree.Sdk));
		proj.AddFileToBuild(targetGUID, proj.AddFile("usr/lib/libz.1.2.5.tbd", "Frameworks/libz.1.2.5.tbd", PBXSourceTree.Sdk));
		
		proj.AddFileToEmbedFrameworks(targetObjt, proj.AddFile("Frameworks/Plugins/iOS/KSAdSDK.framework", "Frameworks/Plugins/iOS/KSAdSDK.framework", PBXSourceTree.Sdk));
		proj.RemoveFileFromBuild(targetGUID,proj.AddFile("Frameworks/Plugins/iOS/KSAdSDK.framework", "Frameworks/Plugins/iOS/KSAdSDK.framework", PBXSourceTree.Sdk));
		proj.WriteToFile(projPath);
		
		
		
		var plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
		var plist = new PlistDocument();
		plist.ReadFromFile(plistPath);
		PlistElementDict rootDict = plist.root;
		plist.root.SetString("NSUserTrackingUsageDescription", "获取标记权限向您提供更优质、安全的个性化服务及内容，未经同意我们不会用于其他目的；开启后，您也可以前往系统“设置-隐私 ”中随时关闭。");
		// 弹窗小字文案建议：
		// 获取标记权限向您提供更优质、安全的个性化服务及内容，未经同意我们不会用于其他目的；开启后，您也可以前往系统“设置-隐私 ”中随时关闭。
		// 获取IDFA等广告标识符权限向您提供更优质、安全的个性化服务及内容；开启后，您也可以前往系统“设置-隐私 ”中随时关闭。
		// 设置支持HTTP
		PlistElementDict dict;
		dict = rootDict.CreateDict("NSAppTransportSecurity");
		dict.SetBoolean("NSAllowsArbitraryLoads", true);
		
		//plist.root.SetString("NSPhotoLibraryAddUsageDescription", "需要相册权限");
		//plist.root.SetString("NSPhotoLibraryUsageDescription", "需要相册权限");
		//plist.root.SetString("NSCalendarsUsageDescription", "需要日历权限");
		//plist.root.SetString("NSMicrophoneUsageDescription", "录制屏幕需要麦克风权限");
		//plist.root.SetString("NSCameraUsageDescription", "需要相机权限");
		plist.root.SetString("NSLocationWhenInUseUsageDescription", "需要定位权限,以便更好的为您服务");
		
		//PList文件添加白名单（可选 可增加收益）
		PlistElementArray queriesSchemes;
		queriesSchemes = rootDict.CreateArray("LSApplicationQueriesSchemes");
		queriesSchemes.AddString("tbopen");
		queriesSchemes.AddString("openapp.jdmobile");
		queriesSchemes.AddString("alipays");
		queriesSchemes.AddString("imeituan");
		queriesSchemes.AddString("pddopen");
		queriesSchemes.AddString("sinaweibo");
		queriesSchemes.AddString("snssdk1128");
		queriesSchemes.AddString("kwai");
		queriesSchemes.AddString("ksnebula");
		queriesSchemes.AddString("ctrip");
		queriesSchemes.AddString("vipshop");
		queriesSchemes.AddString("OneTravel");
		queriesSchemes.AddString("taobaoliveshare");
		queriesSchemes.AddString("taobaolite");
		queriesSchemes.AddString("iqiyi");
		queriesSchemes.AddString("eleme");
		queriesSchemes.AddString("openjdlite");
		queriesSchemes.AddString("xhsdiscover");
		queriesSchemes.AddString("tmall");
		queriesSchemes.AddString("dianping");
		queriesSchemes.AddString("youku");
		queriesSchemes.AddString("fleamarket");
		queriesSchemes.AddString("bilibili");
		queriesSchemes.AddString("freereader");
		queriesSchemes.AddString("tantanapp");
		queriesSchemes.AddString("suning");
		queriesSchemes.AddString("qunariphone");
		queriesSchemes.AddString("lianjia");
		queriesSchemes.AddString("zhihu");
		queriesSchemes.AddString("weixin");
		queriesSchemes.AddString("travelguide");
		queriesSchemes.AddString("wbmain");
		queriesSchemes.AddString("taobaotravel");
		queriesSchemes.AddString("cainiao");
		queriesSchemes.AddString("kaola");
		queriesSchemes.AddString("bitauto.yicheapp");
		queriesSchemes.AddString("lianjiabeike");
		queriesSchemes.AddString("taoumaimai");
		queriesSchemes.AddString("amapuri");
		queriesSchemes.AddString("openanjuke");
		queriesSchemes.AddString("bosszp");
		queriesSchemes.AddString("txvideo");
		queriesSchemes.AddString("mttbrowser");
		queriesSchemes.AddString("momochat");
		queriesSchemes.AddString("baiduboxlite");
		queriesSchemes.AddString("com.360buy.jdpingou");
		queriesSchemes.AddString("vmall");
		queriesSchemes.AddString("tuhu");
		queriesSchemes.AddString("comjia");
		queriesSchemes.AddString("yymobile");
		queriesSchemes.AddString("shuqireader");
		
		plist.WriteToFile(plistPath);
		
		UnityEngine.Debug.Log("Xcode 后续处理完成");
#endif
    }

}
