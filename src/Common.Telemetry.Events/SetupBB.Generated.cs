namespace Common.Telemetry.Events
{
	using System;
	using System.Reactive.Linq;
	using BigBrother.Core;

    public partial class SetupBB : ISetupBB
    {
        public void Setup()
        {
            BigBrother.EtwSubscriptions.Add(
                typeof (BulkImportEvent),
                BigBrother.EventStream.OfType<BulkImportEvent>().Subscribe(
                    e =>
                    {
                        EtwInternalSource.Instance.BulkImport(e.BatchId, e.RecordId, e.Failed, e.Message);
                    }));
        }
	}
}
