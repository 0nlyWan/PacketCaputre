using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketDotNet;
using PcapDotNet.Core;
using SharpPcap;

namespace MyPacketCapturer
{
    public partial class frmCapture : Form
    {
        // all are defined in SharpPcap devices.
        CaptureDeviceList devices; // all devices on machine.
        public static ICaptureDevice device; // selected device.
        public static PacketDevice myDevice;
        public static string stringPacket = "";  // packets captured. 
        private static int numPackets = 0;
        private frmSend fsend; //Form to send packets.
        private FrmSmurf Fsmurf;
        private arpCache farp;

        public frmCapture()
        {
            InitializeComponent();
            devices = CaptureDeviceList.Instance;

            if (devices.Count < 1)
            {
                MessageBox.Show("No Devices found on the machine!! ");
                Application.Exit();
            }

            // Add all the devices on the machine to the combo box.
            cmbDevices.Items.AddRange(devices.Select(d => d.Description).ToArray());

            // Select my Ethernet device
            //device = devices.FirstOrDefault(d => d.Description.Contains("Ethernet"));
            //cmbDevices.Text = device.Description;

            //registerHandler();

           
   
        }


        private static void device_OnPacketArrival(object sender, CaptureEventArgs packet)
        {
            // increment total packtets captured
            numPackets++;


            stringPacket += "Packet Number: " + numPackets;
            stringPacket += Environment.NewLine;
            // all the data in our packet.
            byte[] data = packet.Packet.Data;

            // num of bytes per line
            int byteCounter = 0;

            stringPacket += "Destination MAC Address: ";
            foreach (byte pack in data)
            {
                if (byteCounter <= 13)
                    stringPacket += pack.ToString("X2") + " "; // X2 maeans put it in hexidecimal

                byteCounter++;

                switch (byteCounter)
                {
                    case 6:
                        stringPacket += Environment.NewLine;
                        stringPacket += "Source MAC Address: ";
                        break;
                    case 12:
                        stringPacket += Environment.NewLine;
                        stringPacket += "EtherType: ";
                        break;
                    case 14:
                        if (data[12] == 8)
                        {
                            if (data[13] == 0)
                                stringPacket += "(IP)";
                            if (data[13] == 6)
                                stringPacket += "(ARP)";
                        }
                        break;
                }
            }


            stringPacket += Environment.NewLine;
            stringPacket += Environment.NewLine;
            // reading the same data again... just RAW
            byteCounter = 0;
            stringPacket += "Raw Data" + Environment.NewLine;
            foreach (byte b in data)
            {
                stringPacket += b.ToString("X2") + " ";
                byteCounter++;

                if (byteCounter == 16)
                {
                    byteCounter = 0;
                    stringPacket += Environment.NewLine;
                }
            } 
            stringPacket += Environment.NewLine;
            stringPacket += Environment.NewLine;
        }
        private void btnStartStop_Click(object sender, EventArgs e)
        {

            if (btnStartStop.Text.ToLower() == "start!")
            {
                device.StartCapture();
                timer1.Enabled = true;
                btnStartStop.Text = "STOP!";
                btnStartStop.ForeColor = Color.Red;
            }
            else
            {
               try { device.StopCapture();}catch(Exception exception) {
                   MessageBox.Show(exception.Message);}

                timer1.Enabled = false;
                btnStartStop.Text = "START!";
                btnStartStop.ForeColor = Color.Green;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtPackets.AppendText(stringPacket);
            stringPacket = "";
            txtNumPackets.Text = numPackets.ToString();
        }

        private void cmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PCAP.net uses a different object for it's devices for SharpPcap to represent the same interface.
            // I need to match up the devices where the name and description are the 
            device = devices[cmbDevices.SelectedIndex];
            myDevice =
                LivePacketDevice.AllLocalMachine
                    .FirstOrDefault(d => d.Name == device.Name && d.Description == device.Description);

            cmbDevices.Text = device.Description;
            txtGUID.Text = device.Name;
            registerHandler();
        }

        private void registerHandler()
        {
            //register our handler function to the 'packet arrival' event
            device.OnPacketArrival += device_OnPacketArrival;

            int timeOut = 1000; //milliseconds(1 second)
            device.Open(DeviceMode.Promiscuous, timeOut);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text Files| *.txt|All Files| *.*";
            saveFileDialog1.Title = "Save the captured Packets";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                File.WriteAllText(saveFileDialog1.FileName, txtPackets.Text);
                MessageBox.Show("Data has been saved!","Success");
            }
            else
            {
                MessageBox.Show("Select a file to save!");
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files| *.txt|All Files| *.*";
            openFileDialog1.Title = "Open captured packets";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
                txtPackets.Text = File.ReadAllText(openFileDialog1.FileName);
        }

        private void sendWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmSend.Instantiations >= 1)
                MessageBox.Show("Maximum Send Windows have been reached");
            else
            {
                fsend = new frmSend();
                fsend.Show();
            }
        }

        private void smurfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmSmurf.Instantiations > 0)
                MessageBox.Show("Only 1 smurf window right now allowed");
            else
            {
                Fsmurf = new FrmSmurf();
                Fsmurf.Show();
            }
        }

        private void arpCacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            farp = new arpCache();
            farp.Show();
        }

        private void txtPackets_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
