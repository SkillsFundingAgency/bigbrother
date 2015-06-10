namespace BigBrother.Console
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Core;

    class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, "Common.Telemetry.Events.dll") );

            var events = assembly.GetTypes().Where(t => typeof(BBEvent).IsAssignableFrom(t));

            var foo = events.First();

            var validProperties = foo.GetProperties().Where(p => (p.PropertyType.IsValueType ||
                                                                 p.PropertyType == typeof (Guid) ||
                                                                 p.PropertyType == typeof (string)) &&
                                                                 (p.PropertyType.Namespace != null && !p.PropertyType.Namespace.Contains("BigBrother.Core")));





            BBPublisher.Publish(new A(""));
            BBPublisher.Publish(new B(""));
            BBPublisher.Publish(new C("", new Exception()));
        }
    }

    public class A : BBEvent
    {
        public A(string message) : base(message)
        {
        }

        public override FlexEventType EventType
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class B : BBMetricEvent
    {
        public B(string message) : base(message)
        {
        }
    }

    public class C : BBExceptionEvent
    {
        public C(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
