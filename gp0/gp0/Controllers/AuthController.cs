﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;
using gp0.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace gp0.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserContext _userContext;
        public AuthController(UserContext context)
        {
            _userContext = context;
        }
        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                    return Content("False");
                }

                _userContext.Users.AddAsync(user);
                _userContext.SaveChangesAsync();
                return Content("Success");
            }
            catch
            {
                return Content("Error"); ;
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> LoginPost(User user)
        {
            if (_userContext.Users.FirstOrDefault(checkUser =>
                    checkUser.email == user.email && checkUser.password == user.password) != null)
            {
                await Authenticate(user.email);
                return RedirectToAction("Home", "Home");
            }
            return RedirectToAction("Home", "Home"); ;
        }
        [HttpPost]
        public async Task<JsonResult> LoginCertificate(CertificateMessage request)
        {
            X509Certificate2 certinfo = null;
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
                    text = "Выберите сертификат",
                    correct = false
                });
            }
            try
            {
                var cert = _userContext.Certificates.FirstOrDefault(checkCert =>
                    checkCert.thumbprint == certinfo.Thumbprint);
                if (cert == null)
                    RedirectToAction("Login", "Auth");
                var user = _userContext.Users.Find(cert.userid);
                await Authenticate(user.email);
                return Json(new CertificateMessage()
                {
                    correct = true
                });
            }
            catch (Exception)
            {
                return Json(new CertificateMessage()
                {
                    text = "Сертификат не зарегистрирован",
                    correct = false
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
                if (user == null)
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
                    subject = certinfo.Subject,
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
    }
}