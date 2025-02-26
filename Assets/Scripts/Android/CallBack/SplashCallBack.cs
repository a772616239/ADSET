using System;
using UnityEngine;

public class SplashCallBack : AndroidJavaProxy
{
    private ISplashListener listener;
    public SplashCallBack(ISplashListener callback) : base("com.kc.openset.ad.listener.OSETSplashListener")
    {
        this.listener = callback;
    }

    public void onShow()
    {
        this.listener.OnShow();
    }

    public void onError(String s, String s1)
    {
        this.listener.OnError(s, s1);
    }

    public void onClick()
    {
        this.listener.OnClick();
    }

    public void onClose()
    {
        this.listener.OnClose();
    }

}
