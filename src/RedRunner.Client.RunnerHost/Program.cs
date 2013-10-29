using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RedRunner.Client.RunnerHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceRunner service = new ServiceRunner();

            if (args.Length > 0)
            {
                
                
                string serviceToRun = args[0];
                string configFile = string.Empty;

                if (args.Length > 1)
                {
                    configFile = args[1];
                }

                Console.WriteLine(string.Format("Starting windows service: \r\n  [Service: {0}]\r\n  [Config: {1}]", serviceToRun, configFile));
                service.Run(serviceToRun, configFile);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Service full type name is required.");
            }

        }
    }
}
