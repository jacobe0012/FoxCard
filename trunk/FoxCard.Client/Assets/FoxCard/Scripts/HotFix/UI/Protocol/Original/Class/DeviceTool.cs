using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;

public class DeviceTool
{
    /// <summary>
    /// 获取局域网Ip
    /// </summary>
    /// <param name="addressType">IPv4或IPv6</param>
    /// <returns></returns>
    public static string GetLocalIp()
    {
        foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (ni.OperationalStatus == OperationalStatus.Up)
            {
                var ip = ni.GetIPProperties().UnicastAddresses
                    .FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork); // 选择第一个有效的 IPv4 地址

                if (ip != null)
                {
                    return ip.Address.ToString();
                }
            }
        }

        return "localhost";
    }

    /// <summary>
    /// 获取外网Ip
    /// </summary>
    /// <returns></returns>
    public static string GetExtranetIp()
    {
        string IP = string.Empty;
        try
        {
            //从网址中获取本机ip数据
            System.Net.WebClient client = new System.Net.WebClient();
            client.Encoding = System.Text.Encoding.Default;
            IP = client.DownloadString("http://checkip.amazonaws.com/");
            client.Dispose();
            IP = Regex.Replace(IP, @"[\r\n]", "");
        }
        catch (Exception)
        {
        }

        return IP;
    }

    /// <summary>
    /// 获取MAC地址
    /// </summary>
    /// <returns></returns>
    public static List<string> GetMACList(OperationalStatus operationalStatus = OperationalStatus.Up,
        bool isAppendName = true)
    {
        var list = new List<string>();
        //这里使用 NetworkInterface 获取网络设备信息，能够直接获取网络设备类型，描述，名称等信息
        NetworkInterface[] allNetWork = NetworkInterface.GetAllNetworkInterfaces();
        if (allNetWork.Length > 0)
        {
            foreach (var item in allNetWork)
            {
                if (item.OperationalStatus == operationalStatus)
                {
                    //对MAC地址加上网卡名称，方便进行对应和选择
                    string strInfo = isAppendName
                        ? item.GetPhysicalAddress().ToString() + $"({item.Name})"
                        : item.GetPhysicalAddress().ToString();
                    list.Add(strInfo);
                }
            }
        }
        else
        {
            Console.WriteLine("找不到可用的网卡！");
        }

        return list;
    }
}