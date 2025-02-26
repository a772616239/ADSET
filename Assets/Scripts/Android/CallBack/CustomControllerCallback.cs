using System;
using UnityEngine;

public class CustomControllerCallback : AndroidJavaProxy
{

    private ICustomController listener;
    public CustomControllerCallback(ICustomController callback) : base("com.kc.openset.config.controller.OSETUnityCustomController")
    {
        this.listener = callback;
        //Debug.Log("七月 CustomControllerCallback init");
    }



    /**
* 是否允许SDK主动使用地理位置信息
*
* @return true可以获取，false禁止获取。默认为true
*/
    public bool canReadLocation()
    {
        return listener.canReadLocation();
    }

    /**
     * 是否允许SDK主动使用手机硬件参数，如：imei, android_id, meid, imsi, iccid
     *
     * @return true可以使用，false禁止使用。默认为true
     */
    public bool canUsePhoneState()
    {
        return listener.canUsePhoneState();
    }

    /**
     * 是否允许SDK主动使用oaid
     *
     * @return true可以使用，false禁止使用。默认为true
     */
    public bool canUseOaid()
    {
        return listener.canUseOaid();
    }

    /**
     * 是否允许SDK主动使用mac_address
     *
     * @return true可以使用，false禁止使用。默认为true
     */
    public bool canUseMacAddress()
    {
        return listener.canUseMacAddress();
    }

    /**
     * 是否允许SDK主动使用ACCESS_NETWORK_STATE权限
     *
     * @return true可以使用，false禁止使用。默认为true
     */
    public bool canUseNetworkState()
    {
        return listener.canUseNetworkState();
    }

    /**
     * 是否允许SDK主动使用存储权限
     *
     * @return true可以使用，false禁止使用。默认为true
     */
    public bool canUseStoragePermission()
    {
        return listener.canUseStoragePermission();
    }


    /**
     * 是否允许SDK主动读取app安装列表
     *
     * @return true可以使用，false禁止使用。默认为true
     */
    public bool canReadInstalledPackages()
    {
        return listener.canReadInstalledPackages();
    }

    /**
     * 是否可启用个性化广告开关
     * @return true：开启，false：关闭
     */
    public bool canUsePersonalizedAd()
    {
        return listener.canUsePersonalizedAd();
    }

    /**
     * 是否开启传感器使用
     * @return true：开启，false：关闭
     */
    public bool canUseSensor()
    {
        return listener.canUseSensor();
    }
}