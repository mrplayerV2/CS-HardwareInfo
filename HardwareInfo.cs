using System.Management;

internal class HardwareInfo
{
	public static string GetProcessorID()
	{
		string result = "";
		foreach (ManagementObject item in new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor").Get())
		{
			result = (string)item["ProcessorId"];
		}
		return result;
	}

	public static string GetHDDSignature()
	{
		string result = "";
		foreach (ManagementObject item in new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk").Get())
		{
			if (item["VolumeSerialNumber"] != null)
			{
				return item["VolumeSerialNumber"].ToString();
			}
		}
		return result;
	}
}
