using System;

public interface IInsertListener
{
    void OnShow();

    void OnClick();

    void OnClose();

    void OnError(String errorCode, String errorMessage);
}
