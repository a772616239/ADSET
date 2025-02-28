using System;

public interface IRewardListener
{
    void OnShow(String key);

    void OnError(String errorCode, String errorMessage);

    void OnClick();
         
    void OnClose(String key);
         
    void OnVideoEnd(String key);
         
    void OnVideoStart();
         
    void OnReward(String key, int arg);

    void OnServiceResponse(int code);
}
