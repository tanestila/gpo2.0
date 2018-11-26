using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gp0.ViewModels
{
    public class DocumentView
    {
        public int id { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
        public string text { get; set; }
        public string date { get; set; }
    }
}
