namespace BigBrother.Core
{
    using System;

    public class BBInternalEvent : BBEvent
    {
        public Exception Exception { get; private set; }

        public BBInternalEvent(string message, Exception exception)
            : base(message)
        {
            Exception = exception;
        }

        public override FlexEventType EventType => FlexEventType.Error;
    }
}