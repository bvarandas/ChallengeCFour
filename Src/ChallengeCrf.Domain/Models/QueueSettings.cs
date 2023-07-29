namespace ChallengeCrf.Domain.Models
{
    public abstract class QueueSettings
    {
        public string HostName { get; set; } = string.Empty;
        public string QueueName { get; set; } = string.Empty;
        public ushort Interval { get; set; }
    }

    public class QueueCommandSettings : QueueSettings { }
    public class QueueEventSettings : QueueSettings { }
}
