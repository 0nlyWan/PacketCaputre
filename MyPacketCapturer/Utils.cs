using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PcapDotNet.Base;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Icmp;
using PcapDotNet.Packets.IpV4;
using AddressFamily = System.Net.Sockets.AddressFamily;

namespace MyPacketCapturer
{
    static class Utils
        // Static class of just utility functions.
    {
        private static Dictionary<IPAddress, PhysicalAddress> arpTable;
        #region Device Data
        public static string IdentifyClass(IPAddress ipAddress)
        {
            // determine what class your ip is based on your first octet.
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
                        data.UnicastAddresses.FirstOrDefault(d => d.Address.AddressFamily == AddressFamily.InterNetwork), //UnicastAddresses has a list of IPAddressInfo. I need just one.
                    MacAddress = enterFace.GetPhysicalAddress()
                };
                return interfaceData;
            }

            // probably should check if i return null. shouldn't be possible to get null if i'm on the internet though. 
            return null;
        }
        #endregion

        #region arp Table functions

        public static Dictionary<IPAddress, PhysicalAddress> GetArpTable()
        {
            if (arpTable == null)
            {
                PopulateArpDict();
                return arpTable;
            }
            else
                return arpTable;

        }
        private static void PopulateArpDict()
        {
            arpTable = new Dictionary<IPAddress, PhysicalAddress>();
            //This function essentially runs ARP -a in a command prompt and stores that data in a dictionary.
            
            //Dictionary<IPAddress,PhysicalAddress> ipToMac = new Dictionary<IPAddress, PhysicalAddress>();
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
            cmd.Start(); // run the command and store the output.
            var output = cmd
                .StandardOutput
                .ReadToEnd()
                .TrimEnd()
                .Split(new string[] { "\r\n" }, StringSplitOptions.None);


            // strip off all the meta data from each interface arp table. Only grab the lines that have a valid macAddress.
            var regexSplit = output.Where(z => macAddressPattern.IsMatch(z)).ToArray();

            foreach (var info in regexSplit)
            {
                // strip off all the white spaces.
                var dataEntry = info.Trim().Split().Where(d => !string.IsNullOrEmpty(d)).ToArray();
                string ip = dataEntry[0];
                string mac = dataEntry[1].ToUpper(); // uppercase the mac because the parse function only accepts uppercase.
                IPAddress ipAddress = IPAddress.Parse(ip);
                PhysicalAddress physicalAddress = PhysicalAddress.Parse(mac);

                if (arpTable.ContainsKey(ipAddress) || mac == "FF-FF-FF-FF-FF-FF") // not in the dictionary already and isn't the broadcast Address
                    continue;
                arpTable.Add(ipAddress, physicalAddress);
            }

            //return arpTable;
        }
        #endregion

        #region Packet Building

        //PCAP.NET FTW!!!!!!
        // https://github.com/PcapDotNet/Pcap.Net
        public static EthernetLayer BuildEthernetFrame(MacAddress victimAddress, MacAddress destination)
        {
            // Source address is the victim! We want to make it seem like they sent out the ping request when it was really me.
            return new EthernetLayer()
            {
                Source = victimAddress,
                Destination = destination,
                EtherType = EthernetType.None
            };
        }

        public static IpV4Layer BuildIpV4Frame(IpV4Address victimsIP, IpV4Address destinationIp, byte ttl)
        {
            return new IpV4Layer()
            {
                Source = victimsIP,
                CurrentDestination = destinationIp,
                Fragmentation = IpV4Fragmentation.None,
                HeaderChecksum = null, // automatically figured out
                Identification = 123, // made this up
                Options = IpV4Options.None, // figure it out yourself.
                Protocol = null, // will figure itself out.
                Ttl = ttl,
                TypeOfService = 0
            };
        }

        public static IcmpLayer BuildIcmpFrame()
        {
            return new IcmpEchoLayer()
            {
                Checksum = null, // will be figured out automatically..
                Identifier = 4352, // made these 2 values up..
                SequenceNumber = 0,
            };
        }

        public static Packet BuildIcmPacket(EthernetLayer ethernetFrame, IpV4Layer IpFrame , IcmpLayer IcmpFrame)
        {
            return new PacketBuilder(ethernetFrame,IpFrame,IcmpFrame).Build(DateTime.Now);
        }

        public static ArpLayer BuildArpFrame(PhysicalAddress SenderMac, IPAddress SenderIP, IPAddress TargetIpAddress)
        {
            return new ArpLayer()
            {
                Operation = ArpOperation.Request,
                ProtocolType = EthernetType.IpV4,
                SenderHardwareAddress = SenderMac.GetAddressBytes().AsReadOnly(),
                SenderProtocolAddress = SenderIP.GetAddressBytes().AsReadOnly(),
                TargetHardwareAddress = new byte[] {0, 0, 0, 0, 0, 0}.AsReadOnly(),
                TargetProtocolAddress = TargetIpAddress.GetAddressBytes().AsReadOnly(),
            };
        }

        public static Packet BuildArpPacket(EthernetLayer ethernetFrame, ArpLayer arpFrame)
        {
            return new PacketBuilder(ethernetFrame,arpFrame).Build(DateTime.Now);
        }
        #endregion
    }
}
