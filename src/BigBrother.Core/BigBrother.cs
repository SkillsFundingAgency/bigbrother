namespace BigBrother.Core
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Reflection;
    using Microsoft.ApplicationInsights;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.CSharp.RuntimeBinder;

    public static class BigBrother
    {
        static BigBrother()
        {
            var insightsKey = ConfigurationManager.AppSettings["iKey"];

            TelemetryConfiguration.Active.InstrumentationKey = insightsKey;
            InsightsClient.InstrumentationKey = insightsKey;

            var dynamicTypes = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, BBConfiguration.TelemetryAssemblySearch)
                                        .SelectMany(p => Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, p)).GetTypes());

            var setupTypes = dynamicTypes.Where(t => !t.IsAbstract &&
                                                     t.GetInterfaces().Contains(typeof (ISetupBB)));

            foreach (var setup in setupTypes.Select(type => (ISetupBB)Activator.CreateInstance(type)))
            {
                setup.Setup();
            }

            _eventInsightsSubscription = Events.Subscribe(OnNextEvent);
            _exceptionInsightsSubscription = Exceptions.Subscribe(OnNextException);
        }

        private static void OnNextException(BBExceptionEvent bbException)
        {
            try
            {
                InsightsClient.TrackException((ExceptionTelemetry) bbException.ToTelemetry());
            }
            catch (Exception ex)
            {
                InsightsClient.TrackException(
                    (ExceptionTelemetry) new BBInternalEvent("Error senting the telemetry event through AppInsights", ex).ToTelemetry());
            }
        }

        private static void OnNextEvent(BBEvent bbEvent)
        {
            try
            {
                InsightsClient.TrackEvent((EventTelemetry) bbEvent.ToTelemetry());
            }
            catch (Exception ex)
            {
                InsightsClient.TrackException(
                    (ExceptionTelemetry) new BBInternalEvent("Error senting the telemetry event through AppInsights", ex).ToTelemetry());
            }
        }

        private static readonly Subject<BBEvent> Events = new Subject<BBEvent>();

        private static readonly Subject<BBExceptionEvent> Exceptions = new Subject<BBExceptionEvent>();

        private static readonly Subject<BBMetricEvent> Metrics = new Subject<BBMetricEvent>();

        private static IDisposable _eventInsightsSubscription;

        private static IDisposable _exceptionInsightsSubscription;

        private static readonly TelemetryClient InsightsClient = new TelemetryClient();

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

        public static void Flush()
        {
            InsightsClient.Flush();
        }
    }
}
