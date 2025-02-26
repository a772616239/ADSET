using System;
using UnityEngine;

public class InsertCallBack : AndroidJavaProxy
{
    private IInsertListener listener;
    public InsertCallBack(IInsertListener callback) : base("com.kc.openset.ad.listener.OSETInterstitialListener")
    {
        this.listener = callback;
    }

    public void onShow()
    {
        this.listener.OnShow();
    }

    public void onClick()
    {
        this.listener.OnClick();
    }

    public void onClose()
    {
        this.listener.OnClose();
    }

    public void onError(String code, String e)
    {
        this.listener.OnError(code, e);
    }
}
