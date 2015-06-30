namespace Common.Telemetry.Events
{
	using System;
    using Microsoft.Diagnostics.Tracing;

    [EventSource(Name = "SFA-Common.Telemetry.Events")]
    public sealed partial class EtwInternalSource : EventSource
    {
        private static readonly Lazy<EtwInternalSource> lazy = new Lazy<EtwInternalSource>(() => new EtwInternalSource());

        public static EtwInternalSource Instance { get { return lazy.Value; } }

		[Event(
			1,
			Task = Tasks.BulkImport,
            Opcode = EventOpcode.Info,
            Level = EventLevel.LogAlways,
            Channel = EventChannel.None,
            Message = "BulkImport")]
		public void BulkImport(Guid batchId, Guid recordId, Boolean failed, String message)
		{
			if (IsEnabled())
				WriteEvent(1, batchId, recordId, failed, message);
		}

    }

    /// <summary>
    /// <see cref="EventTask"/> Codes for the <see cref="EtwInternalSource"/> event source.
    /// </summary>
    public static partial class Tasks
    {
        /// <summary>
        /// BulkImport event.
        /// </summary>
        public const EventTask BulkImport = (EventTask) (1 << 0);

	}
}
