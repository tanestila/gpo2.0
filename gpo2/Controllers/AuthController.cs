using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;
using gpo2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gpo2.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserContext _userContext;
        public AuthController(UserContext context)
        {
            _userContext = context;
        }

        [HttpPost("[action]")]
        [Route("Login")]
        public JsonResult Login([FromBody] User user) 
        {
            if (_userContext.Users.FirstOrDefault(checkUser => checkUser.login == user.login && checkUser.password == user.password) != null)
                return Json("Success");
            return Json("False");
        }
        public JsonResult LoginCertificate([FromBody] CertificateMessage request) 
        {
            try
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
                return Json("Success");
                certinfo.
            }
            catch (Exception e)
            {
                return Json("False");
            }
        }
        // GET: Auth/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Auth/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        [Route("Registration")]
        public JsonResult Registration([FromBody] User request)
        {
            try
            {
                if (_userContext.Users.FirstOrDefault(s => s.login==request.login)!=null)
                return Json("False"); 
                _userContext.Users.AddAsync(request);
                _userContext.SaveChangesAsync();
                return Json("Success");
            }
            catch
            {
                return Json("Error"); ;
            }
        }

        //// GET: Auth/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: Auth/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Login));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Auth/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Auth/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return View();
            }
        }
    }
}