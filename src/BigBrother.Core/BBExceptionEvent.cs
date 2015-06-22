namespace BigBrother.Core
{
    using System;
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.DataContracts;

    public class BBExceptionEvent : BBEvent
    {
        public Exception Exception { get; }

        public BBExceptionEvent(string message, Exception exception)
            : base(message)
        {
            Exception = exception;
        }

        public override FlexEventType EventType => FlexEventType.Error;

        public override ITelemetry ToTelemetry()
        {
            var telemetry = new ExceptionTelemetry
            {
                Exception = Exception,
                HandledAt = ExceptionHandledAt.UserCode,
                SeverityLevel = SeverityLevel.Critical,
            };

            telemetry.Properties["Message"] = Message;

            return telemetry;
        }
    }
}