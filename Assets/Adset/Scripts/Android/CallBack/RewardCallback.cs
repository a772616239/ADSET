using System;
using UnityEngine;

public class RewardCallback : AndroidJavaProxy
{
    private IRewardListener listener;
    public RewardCallback(IRewardListener callback) : base("com.kc.openset.ad.listener.OSETRewardListener")
    {
        this.listener = callback;
    }

    public void onShow(String key)
    {
        this.listener.OnShow(key);
    }

    public void onError(String code, String e)
    {
        this.listener.OnError(code, e);
    }

    public void onClick()
    {
        this.listener.OnClick();
    }

    public void onClose(String key)
    {
        this.listener.OnClose(key);
    }

    public void onVideoEnd(String key)
    {
        this.listener.OnVideoEnd(key);
    }

    public void onLoad()
    {
    
    }

    public void onVideoStart()
    {
        this.listener.OnVideoStart();
    }

    public void onReward(String key, int arg)
    {
        this.listener.OnReward(key, arg);
    }

    public void onServiceResponse(int code)
    {
        this.listener.OnServiceResponse(code);
    }
}
