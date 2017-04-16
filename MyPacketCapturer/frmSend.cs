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

namespace MyPacketCapturer
{
    public partial class frmSend : Form
    {
        public static int Instantiations = 0;
        public frmSend()
        {
            InitializeComponent();
            Instantiations++;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files| *.txt|All Files| *.*";
            openFileDialog1.Title = "Open captured packets";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
                txtPacket.Text = File.ReadAllText(openFileDialog1.FileName);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text Files| *.txt|All Files| *.*";
            saveFileDialog1.Title = "Save the captured Packets";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                File.WriteAllText(saveFileDialog1.FileName, txtPacket.Text);
                MessageBox.Show("Data has been saved!", "Success");
            }
            else
            {
                MessageBox.Show("Select a file to save!");
            }
        }

        private void frmSend_FormClosed(object sender, FormClosedEventArgs e)
        {
            Instantiations--;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string stringBytes = "";

            foreach(string s in txtPacket.Lines)
            {
                // Removing a comment('#some comment') from an NPG file
                string[] noComments = s.Split('#');
                string s1 = noComments[0];
                stringBytes += s1 + Environment.NewLine;
            }
            // extract the hex values into a string array.
            string[] sBytes = stringBytes.Split(new[] {"\n", "\r\n", " ","\t"}, StringSplitOptions.RemoveEmptyEntries);

            // change the string into bytes
            byte[] packet = new byte[sBytes.Length];

            int i = 0;
            foreach (string s in sBytes)
            {
                packet[i] = Convert.ToByte(s, 16);
                i++;
            }

            try
            {
                //for(int j = 0; j < 100000000; j++)
                    frmCapture.device.SendPacket(packet); // device.sendPacket is apart of sharpPcap reference
                   
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.Message);
            }
        }
    }
}
