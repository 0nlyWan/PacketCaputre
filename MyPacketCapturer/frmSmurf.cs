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

namespace MyPacketCapturer
{
    public partial class FrmSmurf : Form
    {
        // Only want 1 instance of this form up.
        public static int Instantiations = 0;
        public static Dictionary<IPAddress,PhysicalAddress> ipToMac = new Dictionary<IPAddress, PhysicalAddress>();
        public FrmSmurf()
        {
            InitializeComponent();
            Instantiations++;
            var data = Utils.ReturnInterfaceData();
            ipToMac = Utils.PopulateArpDict();

            //filter the dictionary to only grab the ip/mac of the clients on the same subnet.
            ipToMac = ipToMac.Where(d => Utils.IdentifyClass(d.Key) == Utils.IdentifyClass(data.IpAddressInformation.Address))
                             .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
           
            
            // write out some data upon initiliazation. 
            txtDnsSuffix.Text = data.DnsSuffix;
            txtIp.Text = data.IpAddressInformation.Address.ToString();
            txtMask.Text = data.IpAddressInformation.IPv4Mask.ToString();
            txtClass.Text = Utils.IdentifyClass(data.IpAddressInformation.Address);
            cboAttackers.Items.AddRange(ipToMac.Keys.ToArray());
        }

       
    }
}
