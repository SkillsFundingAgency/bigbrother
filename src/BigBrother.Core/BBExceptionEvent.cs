namespace BigBrother.Core
{
    using System;

    public class BBExceptionEvent : BBEvent
    {
        public Exception Exception { get; private set; }

        public BBExceptionEvent(string message, Exception exception)
            : base(message)
        {
            Exception = exception;
        }

        public override FlexEventType EventType
        {
            get { return FlexEventType.Error; }
        }
    }
}