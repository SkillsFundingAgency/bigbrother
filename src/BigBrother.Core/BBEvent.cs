namespace BigBrother.Core
{
    public abstract class BBEvent
    {
        public abstract FlexEventType EventType { get; }

        public string Message { get; private set; }

        public BBEvent(string message)
        {
            Message = message;
        }
    }

    public enum FlexEventType
    {
        Information,
        Warning,
        Error,
        Metric
    }
}
