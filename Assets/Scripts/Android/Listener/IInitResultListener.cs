public interface IInitResultListener
{
    void OnError(string message);

    void OnSuccess();
}
