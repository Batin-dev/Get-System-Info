using System;
using System.Management;

class Program
{
    static void Main()
    {
        ShowSystemInfo();
    }

    static void ShowSystemInfo()
    {
        string osName = GetWindowsVersion();
        string windowsVersion = GetWindowsVersion();
        string processorInfo = GetProcessorInfo();
        string gpuInfo = GetGPUInfo();
        string ramInfo = GetRAMInfo();
        string diskInfo = GetTotalDiskSize();

        Console.WriteLine($"Operating System: {osName}");
        Console.WriteLine($"Windows Version: {windowsVersion}");
        Console.WriteLine($"Processor: {processorInfo}");
        Console.WriteLine($"GPU: {gpuInfo}");
        Console.WriteLine($"RAM: {ramInfo}");
        Console.WriteLine($"Total Disk Size: {diskInfo}");

        Console.ReadLine();
    }

    static string GetWindowsVersion()
    {
        string windowsVersion = "Unknown";

        try
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectCollection information = searcher.Get();

            foreach (ManagementObject obj in information)
            {
                windowsVersion = obj["Caption"].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Windows Version Error: {ex.Message}");
        }

        return windowsVersion;
    }

    static string GetProcessorInfo()
    {
        string processorInfo = "Unknown";

        try
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            ManagementObjectCollection information = searcher.Get();

            foreach (ManagementObject obj in information)
            {
                processorInfo = obj["Name"].ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Processor Information Error: {ex.Message}");
        }

        return processorInfo;
    }

    static string GetGPUInfo()
    {
        string gpuInfo = "";

        try
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            ManagementObjectCollection information = searcher.Get();

            foreach (ManagementObject obj in information)
            {
                gpuInfo += $"{obj["Caption"].ToString()}, ";
            }

            gpuInfo = gpuInfo.TrimEnd(',', ' ');
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GPU Information Error: {ex.Message}");
        }

        return gpuInfo;
    }

    static string GetRAMInfo()
    {
        string ramInfo = "Unknown";

        try
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            ManagementObjectCollection information = searcher.Get();

            ulong totalRAMBytes = 0;

            foreach (ManagementObject obj in information)
            {
                totalRAMBytes += Convert.ToUInt64(obj["Capacity"]);
            }

            ulong totalRAMGB = totalRAMBytes / (1024 * 1024 * 1024);
            ramInfo = $"{totalRAMGB} GB";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"RAM Information Error: {ex.Message}");
        }

        return ramInfo;
    }

    static string GetTotalDiskSize()
    {
        string diskInfo = "Unknown";

        try
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DriveType=3");
            ManagementObjectCollection information = searcher.Get();

            ulong totalDiskSizeBytes = 0;

            foreach (ManagementObject obj in information)
            {
                totalDiskSizeBytes += Convert.ToUInt64(obj["Size"]);
            }

            ulong totalDiskSizeGB = totalDiskSizeBytes / (1024 * 1024 * 1024);
            diskInfo = $"{totalDiskSizeGB} GB";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Total Disk Size Information Error: {ex.Message}");
        }

        return diskInfo;
    }
}
