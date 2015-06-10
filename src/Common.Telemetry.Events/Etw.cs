namespace Common.Telemetry.Events
{
	using System;
    using Microsoft.Diagnostics.Tracing;

    public sealed partial class EtwInternalSource : EventSource
    {
		public void BulkImport(Guid batchId, Guid recordId, Boolean failed, String message)
		{
			if (IsEnabled())
				WriteEvent(1, batchId, recordId, failed, message);
		}

    }
}

