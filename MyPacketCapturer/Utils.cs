using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyPacketCapturer
{
    static class Utils
    {
        // ip range finder.
        public static void rangeFinder(InterfaceData ipData)
        {

            const int totalBits = 8;
            var stuff = ipData.IpAddressInformation.IPv4Mask.GetAddressBytes(); // get the bytes of the mask in array Form.
            var data = stuff.Select(d => Convert.ToString(d, 2)); // convert masks to binary...
            var moreShit = string.Join(" ", data); // join them by a space. probably don't need to join... just count 1s and 0s
            // that will identify host and subnets.
        }

        public static string IdentifyClass(IPAddress ipAddress)
        {
            var seperateOctets = ipAddress.ToString().Split('.');
            int firstOctect = int.Parse(seperateOctets[0]);

            if (firstOctect <= 126)
                return "Class A";
            if (firstOctect <= 191)
                return "Class B";

            return "Class C";
        }


        public static InterfaceData ReturnInterfaceData()
        {
            // Grab only the Network Interfaces that are either wireless OR ethernet and have DHCP enabled.
            var network = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(intf => intf.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                               intf.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                .Where(interf => interf.GetIPProperties().GetIPv4Properties().IsDhcpEnabled);

            // Loop over all the interfaces to find the one that i'm currently connected to.
            foreach (NetworkInterface enterFace in network)
            {
                var data = enterFace.GetIPProperties();
                if (string.IsNullOrEmpty(data.DnsSuffix)) // noticed that the network you're connected to has a DNSSuffix all other interfaces have nothing.
                    continue;

                //store some data from that interface into an object to be used later.
                InterfaceData interfaceData = new InterfaceData()
                {
                    DnsSuffix = data.DnsSuffix,
                    DnsAddress = data.DnsAddresses,
                    IpAddressInformation =
                        data.UnicastAddresses.FirstOrDefault(d => d.Address.AddressFamily == AddressFamily.InterNetwork),
                    MacAddress = enterFace.GetPhysicalAddress()
                };
                return interfaceData;
            }

            // probably should check if i return null. shouldn't be possible to get null if i'm on the internet though. 
            return null;
        }

        public static Dictionary<IPAddress,PhysicalAddress> PopulateArpDict()
        {
            Dictionary<IPAddress,PhysicalAddress> ipToMac = new Dictionary<IPAddress, PhysicalAddress>();
            Regex macAddressPattern = new Regex("([A-Fa-f0-9]{2}[-]){5}([A-Fa-f0-9]{2})");
            // Start a new process that'll run the arp -a command.
            // the addresses in my arp table will be used to ping the victim.

            Process cmd = new Process
            {
                StartInfo =
                {
                    FileName = "cmd",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false,
                    Arguments = "/C arp -a" // /C tells the process to exit right after the command.
                }
            };
            cmd.Start();
            var era = cmd
                .StandardOutput
                .ReadToEnd()
                .TrimEnd()
                .Split(new string[] { "\r\n" }, StringSplitOptions.None);


            // strip off all the meta data from each interface arp table. 
            var regexSplit = era.Where(z => macAddressPattern.IsMatch(z)).ToArray();

            foreach (var info in regexSplit)
            {
                // strip off all the white spaces.
                var dataEntry = info.Trim().Split().Where(d => !string.IsNullOrEmpty(d)).ToArray();
                string ip = dataEntry[0];
                string mac = dataEntry[1].ToUpper(); // uppercase the mac because the parse function only accepts uppercase.
                IPAddress ipAddress = IPAddress.Parse(ip);
                PhysicalAddress physicalAddress = PhysicalAddress.Parse(mac);

                if (ipToMac.ContainsKey(ipAddress))
                    continue;
                ipToMac.Add(ipAddress, physicalAddress);
            }

            return ipToMac;
        }
    }
}
