
using System;

public interface ISplashListener
{
    void OnShow();

    void OnError(String errorCode, String errorMessage);

    void OnClick();

    void OnClose();
}
