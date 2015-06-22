namespace BigBrother.Core
{
    using System;
    using Microsoft.ApplicationInsights.DataContracts;

    public class BBInternalEvent : BBEvent
    {
        public Exception Exception { get; }

        public BBInternalEvent(string message, Exception exception)
            : base(message)
        {
            Exception = exception;
        }

        public override FlexEventType EventType => FlexEventType.Error;

        public ExceptionTelemetry ToTelemetry()
        {
            var telemetry = new ExceptionTelemetry
            {
                Exception = Exception,
                HandledAt = ExceptionHandledAt.Platform,
                SeverityLevel = SeverityLevel.Warning,
            };

            telemetry.Properties["Message"] = Message;

            return telemetry;
        }
    }
}