using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using gp0.Models;
using gp0.ViewModels;
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
            LoginView view = new LoginView();
            return View(view);
        }
        //public IActionResult Registration()
        //{
        //    var view = new RegistrationView();
        //    return View(view);
        //}
        [HttpPost]
        //public async Task<IActionResult> Registration(User user)
        //{
        //    if (_userContext.Users.FirstOrDefault(s => s.login == user.login) != null)
        //        return View(new RegistrationView(user, "Логин занят"));
        //    if (_userContext.Users.FirstOrDefault(s => s.email == user.email) != null)
        //        return View(new RegistrationView(user, "Email занят"));
        //    try
        //    {
        //        await _userContext.Users.AddAsync(user);
        //        await _userContext.SaveChangesAsync();
        //        return View(new RegistrationView("Регистрация прошла успешно"));
        //    }
        //    catch (Exception)
        //    {
        //        return View(new RegistrationView(user, "Произошла ошибка при регистрации"));
        //    }
        //}
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (user == null)
                return View(new LoginView("Введите данные"));
            var checkedUser = _userContext.Users.FirstOrDefault(checkUser => checkUser.email == user.email);
            if(checkedUser==null)
                return View(new LoginView(user, "Email или пароль не верны"));
            if (checkedUser.password!=user.password)
                return View(new LoginView("Email или пароль не верны"));
            await Authenticate(user.email);
            if (checkedUser.isAdmin)
                return RedirectToAction("CreateUser", "Admin");
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public async Task<JsonResult> LoginCertificate(Models.CertificateRequest request)
        {
            X509Certificate2 certInfo;
            try
            {
                certInfo = new X509Certificate2(Convert.FromBase64String(Models.CertificateRequest.CheckCert(request.text)));
            }
            catch (Exception)
            {
                return base.Json(new Models.CertificateRequest()
                {
                    text = "Выберите сертификат",
                    correct = false
                });
            }
            try
            {
                var cert = _userContext.Certificates.FirstOrDefault(checkCert =>
                    checkCert.thumbprint == certInfo.Thumbprint);
                if (cert == null)
                {
                    return base.Json(new Models.CertificateRequest()
                {
                    text = "сертификат не зарегистирован",
                    correct = false
                });
                }
                if (!cert.Actual || DateTime.Now >cert.dateto)
                    return base.Json(new Models.CertificateRequest()
                    {
                        text = "сертификат не действителен",
                        correct = false
                    });
                var user = _userContext.Users.Find(cert?.userid);
                await Authenticate(user.email);
                return base.Json(new Models.CertificateRequest()
                {
                    correct = true
                });
                    
            }
            catch (Exception)
            {
                return base.Json(new Models.CertificateRequest()
                {
                    text = "при проверке сертификата",
                    correct = false
                });
            }
        }
        //public JsonResult RegistrationCertificate(Models.CertificateRequest request)
        //{
        //    if (request.email == null)
        //        return base.Json(new Models.CertificateRequest()
        //        {
        //            text = "Введите email",
        //            correct = false
        //        });
        //    X509Certificate2 certinfo;
        //    try
        //    {
        //        certinfo = new X509Certificate2(Convert.FromBase64String(Models.CertificateRequest.CheckCert(request.text)));
        //    }
        //    catch (Exception)
        //    {
        //        return base.Json(new Models.CertificateRequest()
        //        {
        //            correct = false,
        //            email = request.email,
        //            text = "Ошибка в разборе подписи"
        //        });
        //    }
        //    try
        //    {
        //        if (_userContext.Certificates.FirstOrDefault(checkCert =>
        //                checkCert.thumbprint == certinfo.Thumbprint) != null)
        //            return base.Json(new Models.CertificateRequest()
        //            {
        //                correct = false,
        //                email = request.email,
        //                text = "Сертификат уже зарегистрирован"
        //            });
        //        var user = _userContext.Users.FirstOrDefault(checkUser => checkUser.email == request.email);
        //        if (user == null)
        //            return base.Json(new Models.CertificateRequest()
        //            {
        //                correct = false,
        //                email = request.email,
        //                text = "Данный email не найден"
        //            });
        //        var getcert = new Certificate()
        //        {
        //            Actual = true,
        //            datefrom = certinfo.NotBefore,
        //            dateto = certinfo.NotAfter,
        //            serialnumber = certinfo.SerialNumber,
        //            subject = certinfo.Subject,
        //            thumbprint = certinfo.Thumbprint,
        //            userid = user.id,
        //            valid = true
        //        };
        //        _userContext.Certificates.AddAsync(getcert);
        //        _userContext.SaveChangesAsync();

        //        return base.Json(new Models.CertificateRequest()
        //        {
        //            email = request.email,
        //            correct = true,
        //            text = "Сертификат успешно зарегистрирован"
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        return base.Json(new Models.CertificateRequest()
        //        {
        //            text = e.ToString(),
        //            correct = false,
        //            email = request.email
        //        });
        //    }
        //}
    }
}