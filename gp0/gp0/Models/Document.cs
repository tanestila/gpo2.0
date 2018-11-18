using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gp0.Models
{
    public class Document
    {
        public int id { get; set; }
        public int idSender { get; set; }
        public int idReceiver { get; set; }
        public string text { get; set; }
        public string date { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
    }
}
