using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using gp0.Models;

namespace gp0.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserContext _userContext;
        public HomeController(UserContext context)
        {
            _userContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult Registration()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult RegistrationPost(User user)
        {
            try
            {
                if (_userContext.Users.FirstOrDefault(s => s.login == user.login) != null)
                    return Content("False");
                _userContext.Users.AddAsync(user);
                _userContext.SaveChangesAsync();
                return Content("Success");
            }
            catch
            {
                return Content("Error"); ;
            }
        }

        [HttpPost]
        public IActionResult LoginPost(User user)
        {
            if (_userContext.Users.FirstOrDefault(checkUser => checkUser.email == user.email && checkUser.password == user.password) != null)
                return Content($"Success email:{user.email} Password: {user.password}");
            return Content($"False email:{user.email} Password: {user.password}");
        }

        [HttpPost]
        public JsonResult LoginCertificate(CertificateMessage request)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(request.Text);
            XmlElement xRoot = xml.DocumentElement;
            XmlNode data = xRoot.LastChild;
            var certdata = data.LastChild;
            var certxml = certdata.LastChild;
            var cert = certxml.LastChild;
            string certstr = cert.InnerText;
            certstr = certstr.Trim();
            X509Certificate2 certinfo = new X509Certificate2(Convert.FromBase64String(certstr));
            var getcert = new Certificate()
            {
                actual = true,
                datefrom = certinfo.NotBefore,
                dateto = certinfo.NotAfter,
                serialnumber = certinfo.SerialNumber,
                subject = certinfo.SerialNumber,
                thumbprint = certinfo.Thumbprint,
                userid = 1,
                valid = true
            };
            _userContext.Certificates.AddAsync(getcert);
            _userContext.SaveChangesAsync();
            return Json(new CertificateMessage()
            {
                Correct = true,
                Text = "красава"
            });
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
