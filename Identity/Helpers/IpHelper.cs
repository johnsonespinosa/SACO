using System.Net;

namespace Identity.Helpers;

public static class IpHelper
{
    public static string GetIpAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var ipAddress in host.AddressList)
        {
            if(ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                return ipAddress.ToString();
        }
        return string.Empty;
    }
}