using SelfService.Daemon.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SelfService.Daemon.Provider
{
    internal static class ServiceProvider
    {
        private static readonly Dictionary<long, string> processIds = new Dictionary<long, string>();

        internal static Result<List<ServiceItem>> GetServices()
        {
            Result<List<ServiceItem>> result = new Result<List<ServiceItem>>();
            try
            {
                string[] services = File.ReadAllLines("services.txt").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                processIds.Clear();

                result.Data = new List<ServiceItem>();
                foreach (string service in services)
                {
                    result.Data.Add(GetService(service));
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Error = e.ToString();
            }

            return result;
        }

        private static ServiceItem GetService(string service)
        {
            ServiceItem serviceItem = new ServiceItem
            {
                Name = service,
            };

            try
            {
                PerformanceCounter performanceCounter = new PerformanceCounter("Process", "Private Bytes", GetServiceInstance(service), true);
                CounterSample currentSample = performanceCounter.NextSample();
                double memory = CounterSample.Calculate(currentSample);
                serviceItem.Memory = (long)memory;
            }
            catch
            {
                // NOOP
            }

            try
            {
                ServiceController windowsService = new ServiceController(service);
                serviceItem.Status = windowsService.Status.ToString();
            }
            catch
            {
                // NOOP
            }

            return serviceItem;
        }

        private static string GetServiceInstance(string serviceName)
        {
            uint processId = GetProcessIdByServiceName(serviceName);
            if (processId == 0)
            {
                return null;
            }


            if (!processIds.ContainsKey(processId))
            {
                string processName = null;
                try
                {
                    Process process = Process.GetProcessById((int)processId);
                    processName = process.ProcessName;
                }
                catch
                {
                    return null;
                }

                PerformanceCounterCategory category = new PerformanceCounterCategory("Process");
                foreach (string instance in category.GetInstanceNames().Where(x => x.StartsWith(processName)))
                {
                    try
                    {
                        using (PerformanceCounter cnt = new PerformanceCounter("Process", "ID Process", instance, true))
                        {
                            long pid = cnt.RawValue;
                            processIds[pid] = instance;
                        }
                    }
                    catch
                    {
                        //NOOP
                    }
                }
            }

            if (processIds.TryGetValue(processId, out string instanceName))
            {
                return instanceName;
            }
            else
            {
                return null;
            }
        }

        private static uint GetProcessIdByServiceName(string serviceName)
        {
            uint processId = 0;
            string query = "SELECT PROCESSID FROM WIN32_SERVICE WHERE NAME = \"" + serviceName + "\"";
            using (System.Management.ManagementObjectSearcher managementObjectSeracher = new System.Management.ManagementObjectSearcher(query))
            {
                foreach (System.Management.ManagementObject managementObject in managementObjectSeracher.Get())
                {
                    processId = (uint)managementObject["PROCESSID"];
                    if (processId > 0)
                    {
                        return processId;
                    }
                }
            }

            return processId;
        }
    }
}
