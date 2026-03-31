using CommunityToolkit.Mvvm.ComponentModel;
using TickyNote.Models;

namespace TickyNote.ViewModels
{
    public partial class TimerItemViewModel : ObservableObject
    {
        public TimerItem Model { get; }

        public TimerItemViewModel(TimerItem model) => Model = model;

        public string Title
        {
            get => Model.Title;
            set
            {
                if (Model.Title != value)
                {
                    Model.Title = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Target
        {
            get => Model.Target;
            set
            {
                if (Model.Target != value)
                {
                    Model.Target = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(RemainingTimeText));
                }
            }
        }

        public bool NotifyOnCompletion
        {
            get => Model.NotifyOnCompletion;
            set
            {
                if (Model.NotifyOnCompletion != value)
                {
                    Model.NotifyOnCompletion = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool CompletedNotified
        {
            get => Model.CompletedNotified;
            set
            {
                if (Model.CompletedNotified != value)
                {
                    Model.CompletedNotified = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan Remaining => Target - DateTime.Now;

        public bool IsComplete => Remaining <= TimeSpan.Zero;

        public string RemainingTimeText
        {
            get
            {
                var remaining = Remaining;
                if (remaining <= TimeSpan.Zero) return "00:00:00";
                if (remaining.TotalDays >= 1) return $"{(int)remaining.TotalDays}d {remaining:hh\\:mm\\:ss}";
                return remaining.ToString("hh\\:mm\\:ss");
            }
        }

        public void Tick()
        {
            OnPropertyChanged(nameof(Remaining));
            OnPropertyChanged(nameof(RemainingTimeText));
            OnPropertyChanged(nameof(IsComplete));
        }
    }
}
