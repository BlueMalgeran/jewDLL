using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Management;
using System.Text;


namespace jewDLL
{
    public class jewTools
    {
        // Start Contains Function
        public static bool Contains<T>(T content, T term)
        {
            bool result = content.ToString().Contains(term.ToString());
            return result;
        }
        // End Contains Function

        // Start Digits Function
        public static int Digits<T>(T args)
        {
            int result = args.ToString().Length;
            return result;
        }
        // End Contains Function

        // Start GetIP Function
        public static string GetIP()
        {
            string result = new WebClient().DownloadString("http://icanhazip.com");
            return result;
        }
        // End Contains Function

        // Start SplitToDigits Function
        public static int[] SplitToDigits(int num)
        {
            List<int> listOfInts = new List<int>();

            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num /= 10;
            }
            listOfInts.Reverse();
            return listOfInts.ToArray();
        }
        // End SplitToDigits Function

        // Start Swap function
        public static void Swap<T>(ref T x, ref T y)
        {
            T t = y;
            y = x;
            x = t;
        }
        // End Swap function

        // Start GetHWID Function
        public static string GetHWID()
        {
            bool flag = string.IsNullOrEmpty(fingerPrint);
            if (flag)
            {
                fingerPrint = GetHash(string.Concat(new string[]
                {
                        "CPU >> ",
                        cpuId(),
                        "\nBIOS >> ",
                        biosId(),
                        "\nBASE >> ",
                        baseId(),
                        videoId(),
                        "\nMAC >> ",
                        macId()
                }));
            }
            return fingerPrint;
        }

        private static string GetHash(string s)
        {
            MD5 md = new MD5CryptoServiceProvider();
            ASCIIEncoding asciiencoding = new ASCIIEncoding();
            byte[] bytes = asciiencoding.GetBytes(s);
            return GetHexString(md.ComputeHash(bytes));
        }

        private static string GetHexString(byte[] bt)
        {
            string text = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int num = (int)b;
                int num2 = num & 15;
                int num3 = num >> 4 & 15;
                bool flag = num3 > 9;
                if (flag)
                {
                    text += ((char)(num3 - 10 + 65)).ToString();
                }
                else
                {
                    text += num3.ToString();
                }
                bool flag2 = num2 > 9;
                if (flag2)
                {
                    text += ((char)(num2 - 10 + 65)).ToString();
                }
                else
                {
                    text += num2.ToString();
                }
                bool flag3 = i + 1 != bt.Length && (i + 1) % 2 == 0;
                if (flag3)
                {
                    text += "-";
                }
            }
            return text;
        }

        private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string text = "";
            ManagementClass managementClass = new ManagementClass(wmiClass);
            ManagementObjectCollection instances = managementClass.GetInstances();
            foreach (ManagementBaseObject managementBaseObject in instances)
            {
                ManagementObject managementObject = (ManagementObject)managementBaseObject;
                bool flag = managementObject[wmiMustBeTrue].ToString() == "True";
                if (flag)
                {
                    bool flag2 = text == "";
                    if (flag2)
                    {
                        try
                        {
                            text = managementObject[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return text;
        }

        private static string identifier(string wmiClass, string wmiProperty)
        {
            string text = "";
            ManagementClass managementClass = new ManagementClass(wmiClass);
            ManagementObjectCollection instances = managementClass.GetInstances();
            foreach (ManagementBaseObject managementBaseObject in instances)
            {
                ManagementObject managementObject = (ManagementObject)managementBaseObject;
                bool flag = text == "";
                if (flag)
                {
                    try
                    {
                        text = managementObject[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return text;
        }

        private static string cpuId()
        {
            string text = identifier("Win32_Processor", "UniqueId");
            bool flag = text == "";
            if (flag)
            {
                text = identifier("Win32_Processor", "ProcessorId");
                bool flag2 = text == "";
                if (flag2)
                {
                    text = identifier("Win32_Processor", "Name");
                    bool flag3 = text == "";
                    if (flag3)
                    {
                        text = identifier("Win32_Processor", "Manufacturer");
                    }
                    text += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return text;
        }

        private static string biosId()
        {
            return string.Concat(new string[]
            {
                    identifier("Win32_BIOS", "Manufacturer"),
                    identifier("Win32_BIOS", "SMBIOSBIOSVersion"),
                    identifier("Win32_BIOS", "IdentificationCode"),
                    identifier("Win32_BIOS", "SerialNumber"),
                    identifier("Win32_BIOS", "ReleaseDate"),
                    identifier("Win32_BIOS", "Version")
            });
        }

        private static string diskId()
        {
            return identifier("Win32_DiskDrive", "Model") + identifier("Win32_DiskDrive", "Manufacturer") + identifier("Win32_DiskDrive", "Signature") + identifier("Win32_DiskDrive", "TotalHeads");
        }

        private static string baseId()
        {
            return identifier("Win32_BaseBoard", "Model") + identifier("Win32_BaseBoard", "Manufacturer") + identifier("Win32_BaseBoard", "Name") + identifier("Win32_BaseBoard", "SerialNumber");
        }

        private static string videoId()
        {
            return identifier("Win32_VideoController", "DriverVersion") + identifier("Win32_VideoController", "Name");
        }

        private static string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }

        private static string fingerPrint = string.Empty;
        // End GetHWID Function
    }
}
