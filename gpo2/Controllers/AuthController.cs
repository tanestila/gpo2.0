using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gpo2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace gpo2.Controllers
{

    // Можешь создать новый контролер 
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserContext userContext;
        public AuthController(UserContext context)
        {
            userContext = context;
        }
        [HttpPost("[action]")]
        [Route("Login")]
        public JsonResult Login([FromBody] User user) //test
        {
            if (userContext.Users.FirstOrDefault(check_user => check_user.login == user.login && check_user.password == user.password) != null)
                return Json("Success");
            return Json("False");
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
        [Route("Regist")]
        public JsonResult Registration([FromBody] User request)
        {
            try
            {
                if (userContext.Users.FirstOrDefault(s => s.login==request.login)!=null)
                return Json("False"); 
                userContext.Users.AddAsync(request);
                userContext.SaveChangesAsync();
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