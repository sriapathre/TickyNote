
using System.Windows;
using Microsoft.Toolkit.Uwp.Notifications;

namespace TickyNote
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                var args = ToastArguments.Parse(toastArgs.Argument);
                // Marshal back to UI thread
                Current.Dispatcher.Invoke(() =>
                {
                    // Example: bring app to front, or navigate to timer
                    // You can read args["timerId"] here
                    // MessageBox.Show($"Activated with: {args}");
                });
            };
        }

    }

}
