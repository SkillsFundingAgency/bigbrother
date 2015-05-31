namespace BigBrother.Core
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;

    public static class BBPublisher
    {
        private static readonly Subject<BBEvent> Events = new Subject<BBEvent>();

        private static readonly Subject<BBExceptionEvent> Exceptions = new Subject<BBExceptionEvent>();

        private static readonly Subject<BBMetricEvent> Metrics = new Subject<BBMetricEvent>();

        public static IObservable<BBEvent> EventStream
        {
            get { return Events.AsObservable(); }
        }

        public static IObservable<BBEvent> ExceptionStream
        {
            get { return Exceptions.AsObservable(); }
        }

        public static IObservable<BBEvent> MetricStream
        {
            get { return Metrics.AsObservable(); }
        }

        public static void Push(BBEvent bbEvent)
        {
            // GUARDS

            Events.OnNext(bbEvent);
        }

        public static void Push(BBExceptionEvent exceptionEvent)
        {
            // GUARDS

            Exceptions.OnNext(exceptionEvent);
        }

        public static void Push(BBMetricEvent metricEvent)
        {
            // GUARDS

            Metrics.OnNext(metricEvent);
        }
    }
}
