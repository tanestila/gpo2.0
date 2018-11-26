﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using gp0.Models;
using gp0.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Pages;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace gp0.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserContext _userContext;
        public HomeController(UserContext context)
        {
            _userContext = context;
        }
        public IActionResult Home()
        {
            var emailIdentity = User.Identity.Name;
            var user = _userContext.Users.FirstOrDefault(identityUser => identityUser.email == emailIdentity);
            IEnumerable<DocumentView> documents = from docs in _userContext.Documents
                where docs.idReceiver == user.id
                join users in _userContext.Users on docs.idSender equals users.id 
                select new DocumentView(){ id = docs.id, sender = users.email, text = GetShortText(docs.text), date = docs.date};
            return View(documents);
        }
        public async Task<IActionResult> InDocument(int? id)
        {
            if (id != null)
            {
                Document doc = await _userContext.Documents.FirstOrDefaultAsync(p => p.id == id);
                DocumentView view = new DocumentView()
                {
                    date=doc.date,
                    id=doc.id,
                    receiver = _userContext.Users.FirstOrDefault(p=>p.id==doc.idReceiver).email,
                    text=doc.text
                };
                var emailIdentity = User.Identity.Name;
                var user = _userContext.Users.FirstOrDefault(identityUser => identityUser.email == emailIdentity);
                if (user.id != doc.idReceiver)
                    return RedirectToAction("Error", "Home");
                if (view.receiver != null)
                    return View(view);
            }
            return NotFound();
        }
        private string GetShortText(string text)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(text);
            XmlElement xRoot = xml.DocumentElement;
            string data = xRoot.FirstChild.InnerText;
            return data;
        }
        public IActionResult OutDocuments()
        {
            var emailIdentity = User.Identity.Name;
            var user = _userContext.Users.FirstOrDefault(identityUser => identityUser.email == emailIdentity);
            IEnumerable<DocumentView> documents = from docs in _userContext.Documents
                where docs.idSender == user.id
                join users in _userContext.Users on docs.idReceiver equals users.id
                select new DocumentView() { id = docs.id, receiver = users.email, text = GetShortText(docs.text), date = docs.date };
            return View(documents);
        }   
        public IActionResult SendDoc()
        {

            return View();
        }
        public IActionResult Error()
        {           
            return View(new ErrorViewModel{RequestId = "Ошибка доступа"});
        }
        [HttpPost]
        public async Task<JsonResult> SendDoc(DocumentRequest doc)
        {
            if (doc.text == null)
                return base.Json(new DocumentRequest()
                {
                    correct = false,
                    text = "Введите данные"
                });
            if (doc.receiver==null)
                return base.Json(new DocumentRequest()
                {
                    correct = false,
                    text = "Введите получателя"
                });
            var receiver = await _userContext.Users.FirstOrDefaultAsync(checkUser => checkUser.email == doc.receiver);
            if (receiver==null)
                return base.Json(new DocumentRequest()
                {
                    correct = false,
                    text = "Получатель не найден"
                });
            var emailIdentity = User.Identity.Name;
            var sender = await _userContext.Users.FirstOrDefaultAsync(identityUser => identityUser.email == emailIdentity);
            var document = new Document()
            {
                idSender = sender.id,
                idReceiver = receiver.id,
                text= doc.text,
                date = DateTime.Now.ToShortTimeString() + "  "+DateTime.Now.ToShortDateString()
            };
            await _userContext.Documents.AddAsync(document);
            await _userContext.SaveChangesAsync();
            return base.Json(new Models.CertificateRequest()
            {
                correct = true,
                text = "Документ успешно отправлен"
            });
        }
        
    }
}
