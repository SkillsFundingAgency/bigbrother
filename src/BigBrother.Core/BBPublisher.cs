namespace BigBrother.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;

    public static class BBPublisher
    {

        private static readonly Subject<BBEvent> Events = new Subject<BBEvent>();

        private static readonly Subject<BBExceptionEvent> Exceptions = new Subject<BBExceptionEvent>();

        private static readonly Subject<BBMetricEvent> Metrics = new Subject<BBMetricEvent>();

        public static Dictionary<Type, IDisposable> EtwSubscriptions { get; } = new Dictionary<Type, IDisposable>();

        public static IObservable<BBEvent> EventStream => Events.AsObservable();

        public static IObservable<BBEvent> ExceptionStream => Exceptions.AsObservable();

        public static IObservable<BBEvent> MetricStream => Metrics.AsObservable();

        public static void Publish(BBEvent bbEvent)
        {
            // GUARDS

            Events.OnNext(bbEvent);
        }

        public static void Publish(BBExceptionEvent exceptionEvent)
        {
            // GUARDS

            Exceptions.OnNext(exceptionEvent);
        }

        public static void Publish(BBMetricEvent metricEvent)
        {
            // GUARDS

            Metrics.OnNext(metricEvent);
        }
    }
}
