using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gp0.Models;

namespace gp0.ViewModels
{
    public class UserView
    {
        public string login { get; set; }
        public string email { get; set; }
        public string full_name { get; set; }
        public string position { get; set; }
        

        public UserView(User user)
        {
            this.login = user.login;
            this.email = user.email;
            this.full_name = user.full_name;
            this.position = user.position;
        }
    }
}
