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
        [HttpGet]
        public IActionResult CreateUser()
        {
            //if (_userContext.Users.FirstOrDefault(s => s.login == user.login) != null)
            //    return View(new RegistrationView(user, "Логин занят"));
            //if (_userContext.Users.FirstOrDefault(s => s.email == user.email) != null)
            //    return View(new RegistrationView(user, "Email занят"));
            //try
            //{
            //    await _userContext.Users.AddAsync(user);
            //    await _userContext.SaveChangesAsync();
            //    return View(new RegistrationView("Регистрация прошла успешно"));
            //}
            //catch (Exception)
            //{
            //    return View(new RegistrationView(user, "Произошла ошибка при регистрации"));
            //}
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateUser(RegistrationView user)
        {
            X509Certificate2 certinfo;
            try
            {
                certinfo = new X509Certificate2(Convert.FromBase64String(Models.CertificateRequest.CheckCert(user.sign)));
            }
            catch (Exception)
            {
                return base.Json(new RegistrationView()
                {
                    email = user.email,
                    error = "Ошибка в разборе подписи",
                    login=user.login,
                    full_name = user.full_name,
                    position = user.position
                });
            }
            try
            {
                if (_userContext.Certificates.FirstOrDefault(checkCert =>
                        checkCert.thumbprint == certinfo.Thumbprint) != null)
                    return base.Json(new RegistrationView()
                    {
                        email = user.email,
                        error = "Сертификат уже зарегистрирован",
                        login = user.login,
                        full_name = user.full_name,
                        position = user.position
                    });
                var checkedUser = await _userContext.Users.FirstOrDefaultAsync(checkUser => checkUser.email == user.email);
                if (checkedUser != null)
                    return base.Json(new RegistrationView()
                    {
                        login = user.login,
                        full_name = user.full_name,
                        position = user.position,
                        error = "Данный email занят"
                    });
                checkedUser = await _userContext.Users.FirstOrDefaultAsync(checkUser => checkUser.login == user.login);
                if (checkedUser != null)
                    return base.Json(new RegistrationView()
                    {
                        login = user.login,
                        full_name = user.full_name,
                        position = user.position,
                        error = "Данный login занят"
                    });
                await _userContext.Users.AddAsync(new User()
                {
                    email = user.email, full_name = user.full_name, isAdmin = false, login = user.login,
                    password = user.password, position = user.position
                });
                await _userContext.SaveChangesAsync();
                checkedUser = await _userContext.Users.FirstOrDefaultAsync(login => login.login == user.login);
                var getcert = new Certificate()
                {
                    Actual = true,
                    datefrom = certinfo.NotBefore,
                    dateto = certinfo.NotAfter,
                    serialnumber = certinfo.SerialNumber,
                    subject = certinfo.Subject,
                    thumbprint = certinfo.Thumbprint,
                    userid = checkedUser.id,
                    valid = true
                };
                await _userContext.Certificates.AddAsync(getcert);
                await _userContext.SaveChangesAsync();

                return base.Json(new RegistrationView()
                {
                    error = "Сертификат успешно зарегистрирован"
                });
            }
            catch (Exception e)
            {
                return base.Json(new RegistrationView()
                {
                    error = "Произошла ошибка при регистрации"
                });
            }
        }
        public IActionResult GetUsers()
        {
            IEnumerable<UserView> view = from users in _userContext.Users
                                         select new UserView(users);
            return View(view);

        }

    }
}
