using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebProjectMVC.Services
{
    public enum AlertType { Success, Warning, Error, Info }

    public class Alert
    {
        public AlertType Type { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Title { get; set; }

        // Bootstrap 5 class name
        public string CssClass => Type switch
        {
            AlertType.Success => "alert-success",
            AlertType.Warning => "alert-warning",
            AlertType.Error => "alert-danger",
            _ => "alert-info"
        };
    }

    public interface IAlertService
    {
        void Success(string message, string? title = null);
        void Warning(string message, string? title = null);
        void Error(string message, string? title = null);
        void Info(string message, string? title = null);
        List<Alert> GetAlerts();
    }

    public class AlertService : IAlertService
    {
        private const string Key = "Alerts";
        private readonly ITempDataDictionaryFactory _factory;
        private readonly IHttpContextAccessor _accessor;

        public AlertService(ITempDataDictionaryFactory factory, IHttpContextAccessor accessor)
        {
            _factory = factory;
            _accessor = accessor;
        }

        private ITempDataDictionary TempData =>
            _factory.GetTempData(_accessor.HttpContext!);

        public void Success(string message, string? title = null) => Add(AlertType.Success, message, title);
        public void Warning(string message, string? title = null) => Add(AlertType.Warning, message, title);
        public void Error(string message, string? title = null) => Add(AlertType.Error, message, title);
        public void Info(string message, string? title = null) => Add(AlertType.Info, message, title);

        private void Add(AlertType type, string message, string? title)
        {
            var alerts = GetAlerts();
            alerts.Add(new Alert { Type = type, Message = message, Title = title });
            TempData[Key] = JsonSerializer.Serialize(alerts);
        }

        public List<Alert> GetAlerts()
        {
            if (TempData.TryGetValue(Key, out var value) && value is string json)
                return JsonSerializer.Deserialize<List<Alert>>(json) ?? new();
            return new();
        }
    }
}