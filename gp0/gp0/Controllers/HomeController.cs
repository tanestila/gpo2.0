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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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
            IEnumerable<Document> documents = _userContext.Documents
                .Where(doc => doc.idReceiver == user.id);
            foreach (var doc in documents)
            {
                
            }
            return View(documents);
        }
        public IActionResult OutDocuments()
        {
            var emailIdentity = User.Identity.Name;
            var user = _userContext.Users.FirstOrDefault(identityUser => identityUser.email == emailIdentity);
            var inDocuments = _userContext.Documents
                .Where(doc => doc.idSender == user.id);
            foreach (var doc in inDocuments)
            {

            }
            return View(inDocuments);
        }
        public IActionResult SendDoc()
        {

            return View();
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
                    text = "Введите данные"
                });
            var receiver = await _userContext.Users.FirstOrDefaultAsync(checkUser => checkUser.email == doc.receiver);
            var emailIdentity = User.Identity.Name;
            var sender = await _userContext.Users.FirstOrDefaultAsync(identityUser => identityUser.email == emailIdentity);
            var document = new Document()
            {
                idSender = sender.id,
                idReceiver = receiver.id,
                text= doc.text
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