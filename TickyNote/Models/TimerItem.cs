namespace TickyNote.Models
{
    public class TimerItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public DateTime Target { get; set; }

        //Toast Notification behavior
        public bool NotifyOnCompletion { get; set; } = true;
        public bool CompletedNotified { get; set; } = false;
        public TimerItem(string title, DateTime target)
        {
            Title = title;
            Target = target;
        }
    }
}
