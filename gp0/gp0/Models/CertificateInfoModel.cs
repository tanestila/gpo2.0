using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gp0.Models
{
    public class CertificateInfoModel
    {
            public string issuer { get; set; }
            public string subject { get; set; }
            public bool valid { get; set; }
            public string dateto { get; set; }
        public string algorithm { get; set; }
        public string message { get; set; }
    }
}
