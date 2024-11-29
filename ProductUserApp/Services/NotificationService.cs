using Blazored.Toast.Services;
using ProductUserApp.Services.Interfaces;

namespace ProductUserApp.Services
{
    public class NotificationService : INotificationService
    {
        public event Action<string, string>? OnShow;

        public void ShowSuccess(string message) => OnShow?.Invoke("success", message);

        public void ShowError(string message) => OnShow?.Invoke("error", message);

        public void ShowWarning(string message) => OnShow?.Invoke("warning", message);

        public void ShowInfo(string message) => OnShow?.Invoke("info", message);
    }

}
