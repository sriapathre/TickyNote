using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;
using TickyNote.Models;
using TickyNote.Services;

namespace TickyNote.ViewModels
{
    /// <summary>
    /// Represents all view options
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        private readonly TimerStore _timerStore = new();
        private readonly SettingsStore _settingsStore = new();

        public ObservableCollection<TimerItemViewModel> Timers { get; } = new();

        [ObservableProperty]
        private string _newTimerTitle = "New Countdown";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NewTimerDays))]
        [NotifyPropertyChangedFor(nameof(NewTimerHours))]
        [NotifyPropertyChangedFor(nameof(NewTimerMinutes))]
        [NotifyPropertyChangedFor(nameof(NewTimerSeconds))]
        private TimeSpan _newTimerDuration;

        // Computed properties for UI binding
        public int NewTimerDays
        {
            get => NewTimerDuration.Days;
            set => NewTimerDuration = new TimeSpan(value, NewTimerDuration.Hours, NewTimerDuration.Minutes, NewTimerDuration.Seconds);
        }

        public int NewTimerHours
        {
            get => NewTimerDuration.Hours;
            set => NewTimerDuration = new TimeSpan(NewTimerDuration.Days, value, NewTimerDuration.Minutes, NewTimerDuration.Seconds);
        }

        public int NewTimerMinutes
        {
            get => NewTimerDuration.Minutes;
            set => NewTimerDuration = new TimeSpan(NewTimerDuration.Days, NewTimerDuration.Hours, value, NewTimerDuration.Seconds);
        }

        public int NewTimerSeconds
        {
            get => NewTimerDuration.Seconds;
            set => NewTimerDuration = new TimeSpan(NewTimerDuration.Days, NewTimerDuration.Hours, NewTimerDuration.Minutes, value);
        }

        [ObservableProperty]
        private SolidColorBrush _themeColor = null!;

        [ObservableProperty]
        private bool _isInputPanelExpanded = true;

        [ObservableProperty]
        private NotificationTone _selectedNotificationTone;

        public bool HasTimers => Timers.Count > 0;

        public List<SolidColorBrush> AvailableColors { get; } =
            AppSettings.AvailableColors.Select(c => (SolidColorBrush)new BrushConverter().ConvertFromString(c)!).ToList();

        public List<NotificationTone> AvailableTones { get; } = Enum.GetValues<NotificationTone>().ToList();

        public List<DefaultCountdownOption> AvailableDefaultCountdowns { get; } = [.. AppSettings.AvailableDefaultCountdowns];

        [ObservableProperty]
        private DefaultCountdownOption? _selectedDefaultCountdown;

        [RelayCommand]
        private void SetThemeColor(SolidColorBrush color)
        {
            ThemeColor = color;
            SaveSettings();
        }

        [RelayCommand]
        private void SetNotificationTone(NotificationTone tone)
        {
            SelectedNotificationTone = tone;
            SaveSettings();
        }

        [RelayCommand]
        private void SetDefaultCountdown(DefaultCountdownOption option)
        {
            SelectedDefaultCountdown = option;
            NewTimerDuration = option.Duration;
            SaveSettings();
        }

        private void SaveSettings()
        {
            var color = ThemeColor.Color;
            _settingsStore.Save(new AppSettings
            {
                DefaultCountdownTicks = (SelectedDefaultCountdown?.Duration ?? AppSettings.DefaultCountdownDuration).Ticks,
                NotificationTone = SelectedNotificationTone,
                ThemeColor = $"#{color.R:X2}{color.G:X2}{color.B:X2}"
            });
        }

        [RelayCommand]
        private void ToggleInputPanel() => IsInputPanelExpanded = !IsInputPanelExpanded;

        public MainViewModel()
        {
            // Load persisted settings
            var settings = _settingsStore.Load();
            SelectedNotificationTone = settings.NotificationTone;
            ThemeColor = (SolidColorBrush)new BrushConverter().ConvertFromString(settings.ThemeColor)!;

            // Find matching option for radio button selection
            SelectedDefaultCountdown = AvailableDefaultCountdowns.FirstOrDefault(o =>
                o.Duration == NewTimerDuration);
            SetTimerDefaults();

            Timers.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(HasTimers));

                // Auto-expand input panel when all timers are removed
                if (Timers.Count == 0)
                {
                    IsInputPanelExpanded = true;
                }
                // Auto-collapse every time a timer is added
                else if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    IsInputPanelExpanded = false;
                }
            };

            foreach (var timer in _timerStore.Load())
            {
                Timers.Add(new TimerItemViewModel(timer));
            }            

            // Collapse input panel by default if timers exist
            IsInputPanelExpanded = Timers.Count == 0;
        }

        [RelayCommand]
        private void Save() => _timerStore.Save(Timers.Select(t => t.Model));

        [RelayCommand]
        private void Remove(TimerItemViewModel timer)
        {
            Timers.Remove(timer);
            Save();
        }

        [RelayCommand]
        private void AddDefaultCountDown()
        {
            var targetTime = DateTime.Now.Add(NewTimerDuration);

            Timers.Add(new TimerItemViewModel(new TimerItem(NewTimerTitle, targetTime)));
            Save();
            SetTimerDefaults();
        }

        /// <summary>
        /// Reset User selected defaults
        /// </summary>
        private void SetTimerDefaults()
        {
            var activeTimerCount = Timers.Count(t => !t.IsComplete);
            NewTimerTitle = $"New Countdown {activeTimerCount + 1}";
            NewTimerDuration = SelectedDefaultCountdown?.Duration ?? AppSettings.DefaultCountdownDuration;
        }

        public void TickAll()
        {
            // Collect completed timers to remove
            var completedTimers = Timers.Where(t => t.IsComplete).ToList();

            foreach (var timer in completedTimers)
            {
                Timers.Remove(timer);
            }

            if (completedTimers.Count > 0)
            {
                Save();
            }

            foreach (var timer in Timers)
            {
                timer.Tick();
            }
        }
    }
}
