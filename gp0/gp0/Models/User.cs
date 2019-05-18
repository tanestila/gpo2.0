﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gp0.Models
{
    public class User
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public bool isAdmin { get; set; }
        public string full_name { get; set; }
        public string position { get; set; }
    }
}
