using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gp0.Models;

namespace gp0.ViewModels
{
    public class LoginView
    {
        public string email { get; set; }
        public string password { get; set; }
        public string error { get; set; }
        public LoginView(User user, string error)
        {
            this.email = user.email;
            this.password = user.password;
            this.error = error;
        }
        public LoginView(string error)
        {
            this.error = error;
        }
        public LoginView()
        {
        }
    }
}
