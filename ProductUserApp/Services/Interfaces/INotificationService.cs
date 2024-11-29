namespace ProductUserApp.Services.Interfaces
{
    public interface INotificationService
    {
        event Action<string, string>? OnShow;

        void ShowSuccess(string message);
        void ShowError(string message);
        void ShowWarning(string message);
        void ShowInfo(string message);
    }
}
