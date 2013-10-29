using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Reflection;
using System.Diagnostics;

namespace RedRunner.Client.RunnerHost
{
    public class ServiceRunner
    {

        public void Run(string serviceFullName, string configFile)
        {
            using (AppConfig.Change(configFile))
            {
                Trace.Listeners.Add(new ConsoleTraceListener());

                Type serviceType = Type.GetType(serviceFullName);
                ServiceBase service = (ServiceBase)Activator.CreateInstance(serviceType);

                MethodInfo startMethod = serviceType.GetMethod("OnStart", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                MethodInfo stopMethod = serviceType.GetMethod("OnStop", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                try
                {
                    startMethod.Invoke(service, new object[] { new string[] { } });
                    Console.WriteLine("Service started. Press any key to exit. . .");
                    Console.Read();
                    stopMethod.Invoke(service, null);
                }
                catch (Exception ex)
                {
                    WriteException(ex);
                    Trace.WriteLine("==================================================================");
                    Trace.WriteLine(ex.InnerException.StackTrace);
                }
            }
        }

        private void WriteException(Exception ex)
        {
            if (ex != null)
            {
                Console.WriteLine(ex.Message);
                WriteException(ex.InnerException);
            }
        }

    }
}
