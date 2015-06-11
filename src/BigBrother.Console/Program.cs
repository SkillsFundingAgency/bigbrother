namespace BigBrother.Console
{
    using System;
    using Common.Telemetry.Events;
    using Core;

    class Program
    {
        static void Main(string[] args)
        {
            BBPublisher.Publish(new BulkImportEvent("some message", Guid.NewGuid(), Guid.NewGuid(), false));
        }
    }
}
