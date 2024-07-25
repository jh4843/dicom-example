using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DicomExample.Models
{
    internal class DicomStorageSetting
    {
        public string CallingAeTitle { get; set; }
        public string CalledAeTitle { get; set; }
        public string RemoteHost { get; set; }
        public int RemotePort { get; set; }

        public DicomStorageSetting()
        {
            CallingAeTitle = "My AeTitle";
            CalledAeTitle = "SCP AEtitle";
            RemoteHost = "127.0.0.1";
            RemotePort = 104;
        }
    }
}
