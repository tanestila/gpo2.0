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
        [HttpPost]
        public JsonResult LoginCertificate(CertificateMessage request)
        {
            X509Certificate2 certinfo;
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(request.text);
                XmlElement xRoot = xml.DocumentElement;
                XmlNode data = xRoot.LastChild;
                var certdata = data.LastChild;
                var certxml = certdata.LastChild;
                var cert = certxml.LastChild;
                string certstr = cert.InnerText;
                certstr = certstr.Trim();
                certinfo = new X509Certificate2(Convert.FromBase64String(certstr));
            }
            catch (Exception)
            {
                return Json(new CertificateMessage()
                {
                    correct = false,
                    email = request.email,
                    text = "Ошибка в разборе подписи"
                });
            }
            try
            {
                var cert = _userContext.Certificates.FirstOrDefault(checkCert =>
                    checkCert.thumbprint == certinfo.Thumbprint);
                var user = _userContext.Users.Find(cert.userid);
                if (user!= null)
                    return Json(new CertificateMessage()
                    {
                        correct = true,
                        text = "Авторизация пройдена "+ user.login
                    });
                else
                    return Json(new CertificateMessage()
                    {
                        text = "Сертификат не зарегестрирован",
                        correct = true
                    });
            }
            catch (Exception)
            {
                return Json(new CertificateMessage()
                {
                    text = "Сертификат не зарегистрирован",
                    correct = false,
                });
            }
        }
        public JsonResult RegistrationCertificate(CertificateMessage request)
        {
            if (request.email == null)
                return Json(new CertificateMessage()
                {
                    text = "Введите email",
                    correct = false
                });
            X509Certificate2 certinfo;
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(request.text);
                XmlElement xRoot = xml.DocumentElement;
                XmlNode data = xRoot.LastChild;
                var certdata = data.LastChild;
                var certxml = certdata.LastChild;
                var cert = certxml.LastChild;
                string certstr = cert.InnerText;
                certstr = certstr.Trim();
                certinfo = new X509Certificate2(Convert.FromBase64String(certstr));
            }
            catch (Exception)
            {
                return Json(new CertificateMessage()
                {
                    correct = false,
                    email = request.email,
                    text = "Ошибка в разборе подписи"
                });
            }
            try
            {
                if (_userContext.Certificates.FirstOrDefault(checkCert =>
                        checkCert.thumbprint == certinfo.Thumbprint) != null)
                    return Json(new CertificateMessage()
                    {
                        correct = false,
                        email = request.email,
                        text = "Сертификат уже зарегистрирован"
                    });
                var user = _userContext.Users.FirstOrDefault(checkUser => checkUser.email == request.email);
                if (user==null)
                    return Json(new CertificateMessage()
                    {
                        correct = false,
                        email = request.email,
                        text = "Данный email не найден"
                    });
                var getcert = new Certificate()
                {
                    actual = true,
                    datefrom = certinfo.NotBefore,
                    dateto = certinfo.NotAfter,
                    serialnumber = certinfo.SerialNumber,
                    subject = certinfo.SerialNumber,
                    thumbprint = certinfo.Thumbprint,
                    userid = user.id,
                    valid = true
                };
                _userContext.Certificates.AddAsync(getcert);
                _userContext.SaveChangesAsync();
                return Json(new CertificateMessage()
                {
                    email = request.email,
                    correct = true,
                    text = "Сертификат успешно зарегистрирован"
                });
            }
            catch (Exception e)
            {
                return Json(new CertificateMessage()
                {
                    text = e.ToString(),
                    correct = false,
                    email = request.email
                });
            }
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
