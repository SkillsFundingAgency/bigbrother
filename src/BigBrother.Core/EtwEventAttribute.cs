namespace BigBrother.Core
{
    using System;
    using Microsoft.Diagnostics.Tracing;

    [AttributeUsage(AttributeTargets.Class)]
    public class EtwEventAttribute : Attribute
    {
        public EventOpcode Opcode { get; set; }

        public EventLevel Level { get; set; }

        public EventChannel Channel { get; set; }

        public string Message { get; set; }
    }
}
