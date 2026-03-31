namespace TickyNote.Models
{
    /// <summary>
    /// Default Timer Options
    /// </summary>
    public class DefaultCountdownOption
    {
        public string DisplayName { get; set; }
        public TimeSpan Duration { get; set; }

        public DefaultCountdownOption(string displayName, int days = 0, int hours = 0, int minutes = 0, int seconds = 0)
        {
            DisplayName = displayName;
            Duration = new TimeSpan(days, hours, minutes, seconds);
        }

        public override bool Equals(object? obj)
        {
            return obj is DefaultCountdownOption other && Duration == other.Duration;
        }

        public override int GetHashCode()
        {
            return Duration.GetHashCode();
        }
    }
}
