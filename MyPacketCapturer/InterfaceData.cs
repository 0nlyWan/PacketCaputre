using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MyPacketCapturer
{
    public class InterfaceData
    {
        /** Object to hold information about the devices on the machine.
         */
        public string DnsSuffix { get; set; }
        public IPAddressCollection DnsAddress { get; set; }
        public UnicastIPAddressInformation IpAddressInformation { get; set; }
        public PhysicalAddress MacAddress { get; set; }
    }
}
