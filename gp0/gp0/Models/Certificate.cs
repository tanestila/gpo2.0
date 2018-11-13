using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gp0.Models
{
    public class Certificate
    {
        public int id { get; set; }
        public int userid { get; set; }
        public string thumbprint { get; set; }
        public string subject { get; set; }
        public bool valid { get; set; }
        public bool actual { get; set; }
        public DateTime datefrom { get; set; }
        public DateTime dateto { get; set; }
        public string serialnumber { get; set; }
    }
}
