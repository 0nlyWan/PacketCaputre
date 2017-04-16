using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyPacketCapturer
{
    class TcpPacket
    {
        public string RouterMac { get; set; }
        public string SourceMac { get; set; }
        public IPAddress SourceAddr { get; set; }
        public IPAddress DestinationAddr { get; set; }

        //Ethernet Frame
        private readonly string _Ethernetprotocol = "0800";
        //IP Frame
        private readonly string _version = "45";
        private readonly string _typeOfService = "00";
        private readonly string _totalLength = "0034";
        private readonly string _identification = "4bef"; // might want to randomly generate this value.
        private readonly string _flagsAndOffset = "4000"; // 40 means don't Fragment.
        private readonly string _ttl = "80";
        private readonly string _iPprotocol = "06"; //tcp
        private readonly string _headerChecksum = "0000";
        // TCP Frame
        private readonly string _sourcePort = "cf22"; // I think this can be any value be up to like 65,xxx as long as it's not a major port.
        private readonly string _destinationPort = "21"; // ftp
        private readonly string _sequenceNumber = "41e95a73"; // might be a good idea to generate these too.
        private readonly string _acknowledgmentNumber = "00000000";
        private readonly string _headerLength = "80";
        private readonly string _flags = "02"; //SYN
        private readonly string _windowSize = "2000";
        private readonly string _checksum = "0a6b";
        private readonly string _urgentPointer = "0000";
        private readonly string _maxSize = "020405b4";
        private readonly string _Nop = "01";
        private readonly string _windowScale = "030308";
        private readonly string _sackPermitted = "0402";



        public TcpPacket(string routerMac, string sourceMac, IPAddress sourceAddr, IPAddress destionationAddr)
        {
            RouterMac = routerMac;
            SourceMac = sourceMac;
            SourceAddr = sourceAddr;
            DestinationAddr = destionationAddr;
        }


        public override string ToString()
        {
            StringBuilder ftpPacket = new StringBuilder();

            //Ethernet Frame building.
            ftpPacket.AppendLine(RouterMac);
            ftpPacket.AppendLine(SourceMac);
            ftpPacket.AppendLine(_Ethernetprotocol);

            //IP frame building
            ftpPacket.AppendLine(_version);
            ftpPacket.AppendLine(_typeOfService);
            ftpPacket.AppendLine(_totalLength);
            ftpPacket.AppendLine(_identification);
            ftpPacket.AppendLine(_flagsAndOffset);
            ftpPacket.AppendLine(_ttl);
            ftpPacket.AppendLine(_iPprotocol);
            ftpPacket.AppendLine(_headerChecksum);
            ftpPacket.AppendLine(
                string.Concat(SourceAddr.GetAddressBytes().Select(bytee => bytee.ToString("X2")))); // converting the source ip to Hex.
            ftpPacket.AppendLine(string.Concat(DestinationAddr.GetAddressBytes().Select(bytee => bytee.ToString("X2")))); // convert the destination ip to hex.

            //TCP frame building
            ftpPacket.AppendLine(_sourcePort);
            ftpPacket.AppendLine(_destinationPort);
            ftpPacket.AppendLine(_sequenceNumber);
            ftpPacket.AppendLine(_acknowledgmentNumber);
            ftpPacket.AppendLine(_headerLength);
            ftpPacket.AppendLine(_flags);
            ftpPacket.AppendLine(_windowSize);
            ftpPacket.AppendLine(_checksum);
            ftpPacket.AppendLine(_urgentPointer);
            ftpPacket.AppendLine(_maxSize);
            ftpPacket.AppendLine(_Nop);
            ftpPacket.AppendLine(_windowScale);
            ftpPacket.AppendLine(_Nop);
            ftpPacket.AppendLine(_Nop);
            ftpPacket.AppendLine(_sackPermitted);

            return ftpPacket.ToString();

        }
    }
}
