using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

internal class NetworkInfo
{
	public static string ShowInterfaceInfo()
	{
		StringBuilder stringBuilder = new StringBuilder();
		IPGlobalProperties iPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
		NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
		stringBuilder.AppendFormat("{0}.{1}network info:\n", iPGlobalProperties.HostName, iPGlobalProperties.DomainName);
		if (allNetworkInterfaces == null || allNetworkInterfaces.Length < 1)
		{
			stringBuilder.AppendFormat("no interface。\n");
			return stringBuilder.ToString();
		}
		stringBuilder.AppendFormat("{0} interfaces\n", allNetworkInterfaces.Length);
		NetworkInterface[] array = allNetworkInterfaces;
		foreach (NetworkInterface networkInterface in array)
		{
			PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
			stringBuilder.AppendFormat("MAC address: {0}, name: {1}, type: {2}, OperationalStatus: {3}\n", physicalAddress, networkInterface.Name, networkInterface.NetworkInterfaceType, networkInterface.OperationalStatus);
		}
		return stringBuilder.ToString();
	}

	public static string HostName(bool withDomainName)
	{
		try
		{
			IPGlobalProperties iPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
			return withDomainName ? $"{iPGlobalProperties.HostName}.{iPGlobalProperties.DomainName}" : iPGlobalProperties.HostName;
		}
		catch
		{
			return string.Empty;
		}
	}

	public static bool IsConnectionExist(string hostname)
	{
		try
		{
			Dns.GetHostEntry(hostname);
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public static string GetMacAddr()
	{
		try
		{
			IPGlobalProperties.GetIPGlobalProperties();
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			if (allNetworkInterfaces == null || allNetworkInterfaces.Length < 1)
			{
				return "";
			}
			NetworkInterface[] array = allNetworkInterfaces;
			for (int i = 0; i < array.Length; i++)
			{
				PhysicalAddress physicalAddress = array[i].GetPhysicalAddress();
				if (physicalAddress.ToString() != null)
				{
					return physicalAddress.ToString();
				}
			}
		}
		catch (Exception)
		{
		}
		return "";
	}
}
