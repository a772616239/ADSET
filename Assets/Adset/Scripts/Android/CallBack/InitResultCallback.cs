using UnityEngine;

public class InitResultCallback : AndroidJavaProxy
{
    private IInitResultListener listener;
    public InitResultCallback(IInitResultListener callback) : base("com.kc.openset.listener.OSETInitListener")
    {
        this.listener = callback;
        Debug.Log("七月 InitResultCallback init");
    }

    public void onError(string message)
    {
        this.listener.OnError(message);
    }
    public void onSuccess()
    {
        this.listener.OnSuccess();
    }
}

