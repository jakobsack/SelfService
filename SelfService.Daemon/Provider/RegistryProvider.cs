using Microsoft.Win32;
using SelfService.Daemon.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfService.Daemon.Provider
{
    internal static class RegistryProvider
    {
        internal static Result<List<RegistryItem>> GetRegistry()
        {
            Result<List<RegistryItem>> result = new Result<List<RegistryItem>>();
            try
            {
                List<RegistryItem> registryItems = new List<RegistryItem>();
                string[] paths = File.ReadAllLines("registry.txt");
                foreach (string path in paths.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    registryItems.Add(GetRegistry(path));
                }

                result.Data = registryItems;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Error = e.ToString();
            }

            return result;
        }

        private static RegistryItem GetRegistry(string path)
        {
            RegistryItem registryItem = new RegistryItem
            {
                KeyName = path
            };

            using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(path, false))
            {
                foreach (string valueName in registryKey.GetValueNames())
                {
                    RegistryValueKind kind = registryKey.GetValueKind(valueName);
                    object value = registryKey.GetValue(valueName);
                    string stringValue = "";
                    switch (kind)
                    {
                        case RegistryValueKind.MultiString:
                            string[] values = (string[])value;
                            stringValue = string.Join("\n", values);
                            break;

                        case RegistryValueKind.Binary:
                            byte[] bytes = (byte[])value;
                            for (int i = 0; i < bytes.Length; i++)
                            {
                                // Display each byte as two hexadecimal digits.
                                stringValue += string.Format("{0:X2}", bytes[i]);
                            }
                            break;

                        default:
                            stringValue = value.ToString();
                            break;
                    }

                    RegistryValue registryValue = new RegistryValue
                    {
                        Name = valueName,
                        Value = stringValue,
                        Type = kind.ToString()
                    };

                    registryItem.RegistryValues.Add(registryValue);
                }

                string[] subKeys = registryKey.GetSubKeyNames();
                foreach (string keyName in registryKey.GetSubKeyNames())
                {
                    string keyPath = path + @"\" + keyName;
                    registryItem.RegistryItems.Add(GetRegistry(keyPath));
                }
            }

            return registryItem;
        }
    }
}
