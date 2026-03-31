using System.IO;
using System.Text.Json;
using TickyNote.Models;

namespace TickyNote.Services
{
    public class SettingsStore
    {
        private readonly string _path;

        public SettingsStore()
        {
            _path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TickyNote",
                "settings.json");
        }

        public AppSettings Load()
        {
            if (!File.Exists(_path)) return new();
            try
            {
                return JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(_path)) ?? new();
            }
            catch
            {
                return new();
            }
        }

        public void Save(AppSettings settings)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
            File.WriteAllText(_path, JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true }));
        }
    }

    public class AppSettings
    {
        // Default constants - single source of truth
        public static readonly TimeSpan DefaultCountdownDuration = TimeSpan.FromMinutes(5);
        public const NotificationTone DefaultNotificationTone = NotificationTone.Reminder;
        public const string DefaultThemeColor = "#FDE68A";

        public static readonly string[] AvailableColors =
        [
            DefaultThemeColor, // Yellow (default)
            "#BBF7D0", // Green
            "#FED7AA", // Orange
            "#FECACA", // Red/Pink
            "#BFDBFE", // Blue
            "#E9D5FF", // Purple
            "#F5F5F4", // Light Gray
        ];

        public static readonly DefaultCountdownOption[] AvailableDefaultCountdowns =
        [
            new("30 seconds", seconds: 30),
            new("5 minutes", minutes: 5),
            new("10 minutes", minutes: 10),
            new("30 minutes", minutes: 30),
            new("1 hour", hours: 1),
            new("1 day", days: 1),
        ];

        public long DefaultCountdownTicks { get; set; } = DefaultCountdownDuration.Ticks;
        public NotificationTone NotificationTone { get; set; } = DefaultNotificationTone;
        public string ThemeColor { get; set; } = DefaultThemeColor;
    }
}
