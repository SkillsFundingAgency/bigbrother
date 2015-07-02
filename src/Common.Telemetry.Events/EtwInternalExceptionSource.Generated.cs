namespace Common.Telemetry.Events
{
	using System;
    using Microsoft.Diagnostics.Tracing;

    [EventSource(Name = "Common.Telemetry.Events-Exceptions")]
    public sealed partial class EtwInternalExceptionSource : EventSource
    {
        private static readonly Lazy<EtwInternalExceptionSource> lazy = new Lazy<EtwInternalExceptionSource>(() => new EtwInternalExceptionSource());

        public static EtwInternalExceptionSource Instance { get { return lazy.Value; } }

		[Event(
			1,
			Task = Tasks.BBException,
            Opcode = EventOpcode.Info,
            Level = EventLevel.Critical,
            Channel = EventChannel.Operational,
            Message = "Exception: {0}")]
		public void BBException(String message, string exMessage, string exStackTrace)
		{
			if (IsEnabled())
				WriteEvent(1, message, exMessage, exStackTrace);
		}

    }

    /// <summary>
    /// <see cref="EventTask"/> Codes for the <see cref="EtwInternalExceptionSource"/> event source.
    /// </summary>
    public static partial class Tasks
    {
        /// <summary>
        /// BBException event.
        /// </summary>
        public const EventTask BBException = (EventTask) (1 << 0);

	}
}
