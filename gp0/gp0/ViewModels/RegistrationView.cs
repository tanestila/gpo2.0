using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gp0.Models;

namespace gp0.ViewModels
{
    public class RegistrationView
    {
        public string email { get; set; }
        public string password { get; set; }
        public string error { get; set; }
        public string login { get; set; }
        public string full_name { get; set; }
        public string position { get; set; }
        public string sign { get; set; }
        public RegistrationView(User user,string error)
        {
            this.login = user.login;
            this.email = user.email;
            this.password = user.password;
            this.error = error;
        }
        public RegistrationView(string error)
        {
            this.error = error;
        }
        public RegistrationView()
        {
        }
    }
}
