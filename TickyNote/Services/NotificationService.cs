using Microsoft.Toolkit.Uwp.Notifications;
using TickyNote.Models;

namespace TickyNote.Services
{
    public class NotificationService
    {
        public void ShowTimerDone(string title, string message, string timerId, NotificationTone tone = NotificationTone.Reminder)
        {
            var builder = new ToastContentBuilder()
                .AddArgument("action", "open")
                .AddArgument("timerId", timerId)
                .AddHeader("header",title, title)
                .AddText(message);

            if (tone != NotificationTone.Silent)
            {
                builder.AddAudio(GetAudioUri(tone));
            }
            else
            {
                builder.AddAudio(null, silent: true);
            }

            builder.Show();
        }

        private static Uri GetAudioUri(NotificationTone tone)
        {
            return tone switch
            {
                NotificationTone.Reminder => new Uri("ms-winsoundevent:Notification.Reminder"),
                NotificationTone.Alarm => new Uri("ms-winsoundevent:Notification.Looping.Alarm"),
                NotificationTone.Mail => new Uri("ms-winsoundevent:Notification.Mail"),
                NotificationTone.IM => new Uri("ms-winsoundevent:Notification.IM"),
                _ => new Uri("ms-winsoundevent:Notification.Reminder")
            };
        }
    }
}
