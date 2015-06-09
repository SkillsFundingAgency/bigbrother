namespace Common.Telemetry.Events
{
    using System;
    using BigBrother.Core;

    public partial class BulkImportEvent : BBEvent
    {
        public Guid BatchId { get; }

        public Guid RecordId { get; }

        public bool Failed { get; }

        public BulkImportEvent(string message, Guid batchId, Guid recordId, bool failed)
            : base(message)
        {
            BatchId = batchId;
            RecordId = recordId;
            Failed = failed;
        }

        public override FlexEventType EventType => FlexEventType.Information;
    }
}
