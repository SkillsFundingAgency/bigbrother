namespace BigBrother.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Reflection;

    public static class BBPublisher
    {
        static BBPublisher()
        {
            var dynamicTypes = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, BBConfiguration.TelemetryAssemblySearch)
                                        .SelectMany(p => Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, p)).GetTypes());

            var setupTypes = dynamicTypes.Where(t => !t.IsAbstract &&
                                                     t.GetInterfaces().Contains(typeof (ISetupBB)));

            foreach (var setup in setupTypes.Select(type => (ISetupBB)Activator.CreateInstance(type)))
            {
                setup.Setup();
            }
        }

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
