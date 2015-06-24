namespace BigBrother.Console
{
    using System;
    using System.IO;
    using System.Linq;
    using Common.Telemetry.Events;
    using Core;

    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\David\Source\Repos\bigbrother\src\Common.Telemetry.Events\bin\Debug\Common.Telemetry.Events.dll";

            var dPath = Path.GetDirectoryName(path);
            if (dPath == null) throw new InvalidOperationException("$TargetPath can't be null");

            var appDomain = AppDomain.CreateDomain(Guid.NewGuid().ToString(), null, new AppDomainSetup
            {
                ApplicationBase = dPath,
                PrivateBinPath = dPath,
                ShadowCopyFiles = "true",
                LoaderOptimization = LoaderOptimization.MultiDomain
            });

            appDomain.Load(GetAssembly(Path.Combine(dPath, "Microsoft.Diagnostics.Tracing.EventSource.dll")));
            //appDomain.Load(GetAssembly(Path.Combine(dPath, "Microsoft.Threading.Tasks.dll")));
            //appDomain.Load(GetAssembly(Path.Combine(dPath, "Microsoft.Threading.Tasks.Extensions.dll")));
            //appDomain.Load(GetAssembly(Path.Combine(dPath, "Microsoft.Threading.Tasks.Extensions.Desktop.dll")));
            appDomain.Load(GetAssembly(Path.Combine(dPath, "System.Reactive.Interfaces.dll")));
            appDomain.Load(GetAssembly(Path.Combine(dPath, "System.Reactive.Core.dll")));
            appDomain.Load(GetAssembly(Path.Combine(dPath, "System.Reactive.Linq.dll")));
            appDomain.Load(GetAssembly(Path.Combine(dPath, "Microsoft.ApplicationInsights.dll")));

            var assembly = appDomain.Load(GetAssembly(path));
            var events = assembly.GetTypes().Where(t => typeof(BBEvent).IsAssignableFrom(t) && !t.IsAbstract);


            BigBrother.Publish(new BulkImportEvent("some message", Guid.NewGuid(), Guid.NewGuid(), false));
            BigBrother.Flush();

            Console.ReadKey(false);
        }
        private static byte[] GetAssembly(string assemblyPath)
        {
            byte[] data;

            using (var fs = File.OpenRead(assemblyPath))
            {
                data = new byte[fs.Length];
                fs.Read(data, 0, Convert.ToInt32(fs.Length));
            }

            if (data == null || data.Length == 0)
            {
                throw new ApplicationException("Failed to load " + assemblyPath);
            }

            return data;
        }
    }
}
