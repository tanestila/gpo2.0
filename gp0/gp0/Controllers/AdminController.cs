using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using gp0.Models;
using gp0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using CertificateRequest = gp0.Models.CertificateRequest;

namespace gp0.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserContext _userContext;
        public AdminController(UserContext context)
        {
            _userContext = context;
        }
        public IActionResult CreateUser()
        {

            return View();
        }

        public IActionResult GetUsers()
        {
            IEnumerable<UserView> view = from users in _userContext.Users
                                         select new UserView(users);
            return View(view);

        }

    }
}
