using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TickyNote.Services;
using TickyNote.ViewModels;

namespace TickyNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(1) };
        private readonly MainViewModel _vm;
        private readonly NotificationService _notificationService = new();

        public MainWindow()
        {
            InitializeComponent();

            _vm = new MainViewModel();
            DataContext = _vm;

            _timer.Tick += (_, _) =>
            {
                // Check for notifications BEFORE TickAll removes completed timers
                foreach (var t in _vm.Timers)
                {
                    if (t.IsComplete && t.Model.NotifyOnCompletion && !t.Model.CompletedNotified)
                    {
                        t.Model.CompletedNotified = true;
                        _notificationService.ShowTimerDone(t.Title, "Time's up!", t.Model.Id, _vm.SelectedNotificationTone);
                    }
                }

                _vm.TickAll();
            };
            _timer.Start();
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }
    }
}