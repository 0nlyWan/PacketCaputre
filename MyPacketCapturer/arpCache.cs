using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LukeSkywalker.IPNetwork;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Ethernet;

namespace MyPacketCapturer
{
    public partial class arpCache : Form
    {
        private static InterfaceData data = Utils.ReturnInterfaceData();
        private Dictionary<IPAddress, PhysicalAddress> arpTable;
        private IPNetwork network;
        private static string classLevel = Utils.IdentifyClass(data.IpAddressInformation.Address);
        public arpCache()
        {
            InitializeComponent();
            network = IPNetwork.Parse(data.IpAddressInformation.Address, data.IpAddressInformation.IPv4Mask);
            arpTable = Utils.GetArpTable();

            //My data
            txtDnsSuffix.Text = data.DnsSuffix;
            txtIp.Text = data.IpAddressInformation.Address.ToString();
            txtMacAddress.Text = FrmSmurf.MacAddressFormat(data.MacAddress);
            txtClass.Text = classLevel;
            txtMask.Text = data.IpAddressInformation.IPv4Mask.ToString();

            //octets Data
            var addressBytes = network.Network.GetAddressBytes();
            txtFirstOctet.Text = addressBytes[0].ToString();
            txtSecondOctet.Text = addressBytes[1].ToString();
            txtThirdOctet.Text = addressBytes[2].ToString();
            txtFourthOctet.Text = addressBytes[3].ToString();
            DisableTxtWithNetwork();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (
                PacketCommunicator communicator = frmCapture.myDevice.Open(100, PacketDeviceOpenAttributes.Promiscuous,
                    1000))
            {
                var ethernetFrame = Utils.BuildEthernetFrame(new MacAddress(FrmSmurf.MacAddressFormat(data.MacAddress)),
                    new MacAddress("FF:FF:FF:FF:FF:FF"));

                Packet packet;
                var Ips = GenerateIps();
                foreach(string ip in Ips)
                {
                    var arpFrame = Utils.BuildArpFrame(data.MacAddress, data.IpAddressInformation.Address,
                        IPAddress.Parse(ip));
                    packet = Utils.BuildArpPacket(ethernetFrame, arpFrame);
                    communicator.SendPacket(packet);
                }
               
            }
        }

        private List<string> GenerateIps()
        {
            List<string> iPsToArp = new List<string>();
            var lastUsableAddress = network.LastUsable.GetAddressBytes();
            var firstUsableaddress = network.FirstUsable.GetAddressBytes();

            int firsOctet = firstUsableaddress[0]; // First octet will never change even if it's a Class A address..
            
            //each octet data in string. starting from 2nd Octet.
            string txtSecondO = txtSecondOctet.Text;
            string txtThirdO = txtThirdOctet.Text;
            string txtFourthO = txtFourthOctet.Text;


           
            // values where loop will end. unless a range is specified, we want all up to the last Usable address.
            int endSecondOctet = lastUsableAddress[1];
            int endThirdOctet = lastUsableAddress[2];
            int endFourthOctet = lastUsableAddress[3];

            // values where loop wil start. unless a start is specified, we will start from the first Usable.
            int startSecondOctet = firstUsableaddress[1];
            int startThirdOctet = firstUsableaddress[2];
            int startFourthOcthet = firstUsableaddress[3];

           

            //if range is specified...let's parse the data.
            if (txtSecondO.Contains("-"))
            {
                var splitData = txtSecondO.Split('-');
                startSecondOctet = int.Parse(splitData[0]);
                endSecondOctet = int.Parse(splitData[1]);
            }
            if (txtThirdO.Contains("-"))
            {
                var splitData = txtThirdO.Split('-');
                startThirdOctet = int.Parse(splitData[0]);
                endThirdOctet = int.Parse(splitData[1]);
            }
            if (txtFourthO.Contains("-"))
            {
                var splitData = txtFourthO.Split('-');
                startFourthOcthet = int.Parse(splitData[0]);
                endFourthOctet = int.Parse(splitData[1]);
            }

            // once data is parsed let's loop and generate IPs to arp
            // Second Octet...
            for (int secondOctet = startSecondOctet; secondOctet <= endSecondOctet; secondOctet++)
            {
                // Third Octet...
                for (int thirdOctet = startThirdOctet; thirdOctet <= endThirdOctet; thirdOctet++)
                {
                    // Fourth Octet....
                    for (int fourthOcthet = startFourthOcthet; fourthOcthet <= endFourthOctet; fourthOcthet++)
                    {
                        string ip = $"{firsOctet}.{secondOctet}.{thirdOctet}.{fourthOcthet}";
                        iPsToArp.Add(ip);
                    }
                }
            }
            
            return iPsToArp;
        }

        private void DisableTxtWithNetwork()
        {

            if (classLevel == "Class A")
                txtFirstOctet.Enabled = false;
            else if (classLevel == "Class B")
            {
                txtFirstOctet.Enabled = false;
                txtSecondOctet.Enabled = false;
            }
            else
            {
                txtFirstOctet.Enabled = false;
                txtSecondOctet.Enabled = false;
                txtThirdOctet.Enabled = false;
            }
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            await Task.Run(() => ListenForArps());
        }

        private void PacketHandler(Packet packet)
        {
            ArpDatagram arp = packet.Ethernet.Arp;

            if (arp.Operation == ArpOperation.Reply)
            {
                string ipForTxt = string.Join(".", arp.SenderProtocolAddress.Select(d => d.ToString()));
                string macForTxt = string.Join("-", arp.SenderHardwareAddress.Select(d => d.ToString("X2")));

                IPAddress parsedIP = IPAddress.Parse(ipForTxt);
                PhysicalAddress parsedMac = PhysicalAddress.Parse(macForTxt.ToUpper());

                if (arpTable.ContainsKey(parsedIP))
                    return;
                else
                {
                    // updating UI aysnc shameless stolen from from http://stackoverflow.com/questions/17631275/updating-ui-from-events-using-asyc-await
                    arpTable.Add(parsedIP, parsedMac);
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker) (() =>
                            {
                                updateUI(parsedIP);
                            }
                        ));
                    }
                    else
                        updateUI(parsedIP);
                }
            }

        }

        private void updateUI(IPAddress client)
        {
            txtOutput.AppendText($"{client} ---> {FrmSmurf.MacAddressFormat(arpTable[client])}\n");
        }
        private  void ListenForArps()
        {
            using (
                PacketCommunicator communicator = frmCapture.myDevice.Open(65536, PacketDeviceOpenAttributes.Promiscuous,
                    1000))
            {
                if(communicator.DataLink.Kind != DataLinkKind.Ethernet)
                    MessageBox.Show("Ethernet Only");
                using (BerkeleyPacketFilter filter = communicator.CreateFilter("arp"))
                {
                    communicator.SetFilter(filter);
                }

                communicator.ReceivePackets(0, PacketHandler);
            }

            
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
