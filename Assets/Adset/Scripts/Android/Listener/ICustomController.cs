

using System;
/**
 * 以下配置建议使用默认。否则可能会造成广告的价格变低。
 */
public abstract class ICustomController
{
    /**
 * 是否允许SDK主动使用地理位置信息
 *
 * @return true可以获取，false禁止获取。默认为true
 */
    virtual public bool canReadLocation()
    {
        return true;
    }

    /**
     * 是否允许SDK主动使用手机硬件参数，如：imei, android_id, meid, imsi, iccid
     *
     * @return true可以使用，false禁止使用。默认为true
     */
    virtual public bool canUsePhoneState()
    {
        return true;
    }

    /**
     * 是否允许SDK主动使用oaid
     *
     * @return true可以使用，false禁止使用。默认为true
     */
    virtual public bool canUseOaid()
    {
        return true;
    }

    /**
     * 是否允许SDK主动使用mac_address
     *
     * @return true可以使用，false禁止使用。默认为true
     */
    virtual public bool canUseMacAddress()
    {
        return true;
    }

    /**
     * 是否允许SDK主动使用ACCESS_NETWORK_STATE权限
     *
     * @return true可以使用，false禁止使用。默认为true
     */
    virtual public bool canUseNetworkState()
    {
        return true;
    }

    /**
     * 是否允许SDK主动使用存储权限
     *
     * @return true可以使用，false禁止使用。默认为true
     */
    virtual public bool canUseStoragePermission()
    {
        return true;
    }

    /**
     * 是否允许SDK主动读取app安装列表
     *
     * @return true可以使用，false禁止使用。默认为true
     */
    virtual public bool canReadInstalledPackages()
    {
        return true;
    }

    /**
     * 是否可启用个性化广告开关
     * @return true：开启，false：关闭
     */
    virtual public bool canUsePersonalizedAd()
    {
        return true;
    }

    /**
     * 是否开启传感器使用
     * @return true：开启，false：关闭
     */
    virtual public bool canUseSensor()
    {
        return true;
    }
}

