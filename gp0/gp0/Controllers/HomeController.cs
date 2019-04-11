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
                where docs.idReceiver == user.id && docs.isDeleted != true
                join users in _userContext.Users on docs.idSender equals users.id 
                select new DocumentView(){ id = docs.id, sender = users.email, text = GetShortText(docs.text), date = docs.date};
            return View(documents);
        }
        public async Task<IActionResult> InDocument(int? id)
        {
            if (id != null)
            {
                var emailIdentity = User.Identity.Name;
                Document doc = await _userContext.Documents.FirstOrDefaultAsync(p => p.id == id && !p.isDeleted);
                var user = _userContext.Users.FirstOrDefault(identityUser => identityUser.email == emailIdentity);
                if (user!=null)
                if (user.id != doc.idReceiver)
                    return RedirectToAction("Error", "Home");
                DocumentView view = new DocumentView()
                {
                    date=doc.date,
                    id=doc.id,
                    sender = _userContext.Users.FirstOrDefault(p=>p.id==doc.idSender)?.email,
                    text=doc.text
                };
                if (view.sender != null)
                    return View(view);
            }
            return NotFound();
        }
        public async Task<IActionResult> UserInfo(string email)
        {
            if (email != null)
            {
                var user = await _userContext.Users.FirstOrDefaultAsync(checkUser => checkUser.email == email);
                if (user == null)
                    return NotFound();
                return View(new UserView(user));
            }

            return NotFound();
        }
        [HttpPost]
        public Task<JsonResult> CertificateInfo(CertificateRequest request)
        {
            try
            {
                X509Certificate2 cert = new X509Certificate2(Convert.FromBase64String(CertificateRequest.CheckCert(request.text)));
                return Task.FromResult(Json(new CertificateInfoModel()
                {
                    algorithm = cert.SignatureAlgorithm.FriendlyName,
                    dateto = cert.NotAfter.ToLongDateString(),
                    issuer = cert.Issuer,
                    subject = cert.Subject,
                    valid = cert.Verify(),
                    message = "Сертификат проверен"
                }));
            }
            catch (Exception)
            {
                return Task.FromResult(Json(new CertificateInfoModel()
                {
                    message="Ошибка при разборе сертификата"
                }));
            }
        }
        public async Task<IActionResult> OutDocument(int? id)
        {
            if (id != null)
            {
                var emailIdentity = User.Identity.Name;
                Document doc = await _userContext.Documents.FirstOrDefaultAsync(p => p.id == id);
                var user = await _userContext.Users.FirstOrDefaultAsync(identityUser => identityUser.email == emailIdentity);
                if (user.id != doc.idSender)
                    return RedirectToAction("Error", "Home");
                DocumentView view = new DocumentView()
                {
                    date = doc.date,
                    id = doc.id,
                    receiver = _userContext.Users.FirstOrDefault(p => p.id == doc.idReceiver)?.email,
                    text = doc.text
                };
                if (view.receiver != null)
                    return View(view);
            }
            return NotFound();
        }
        private static string GetShortText(string text)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(text);
            XmlElement xRoot = xml.DocumentElement;
            string data = xRoot.FirstChild.InnerText;
            if (data.Length > 15)
            {
                return data.Substring(0,15);
            }
            return data;
        }
        public IActionResult OutDocuments()
        {
            var emailIdentity = User.Identity.Name;
            var user = _userContext.Users.FirstOrDefault(identityUser => identityUser.email == emailIdentity);
            IEnumerable<DocumentView> documents = from docs in _userContext.Documents
                where docs.idSender == user.id && docs.isDeleted != true
                join users in _userContext.Users on docs.idReceiver equals users.id
                select new DocumentView() { id = docs.id, receiver = users.email, text = GetShortText(docs.text), date = docs.date };
            return View(documents);
        }

        public async Task<IActionResult> Profile()
        {
            var email= User.Identity.Name;
            if (email != null)
            {
                var user = await _userContext.Users.FirstOrDefaultAsync(checkUser => checkUser.email == email);
                if (user == null)
                    return NotFound();
                return View(new UserView(user));
            }

            return NotFound();
        }
        public IActionResult SendDoc()
        {
            IEnumerable<UserView> view = from users in _userContext.Users
                select new UserView(users);
            return View(view);
        }
        public IActionResult AddressBook()
        {
            IEnumerable<UserView> view = from users in _userContext.Users
                                         select new UserView(users);
            return View(view);
        }
        public async Task<JsonResult> DeleteInDocument(int? id)
        {
            Document document = await _userContext.Documents.FirstOrDefaultAsync(p => p.id == id);
            document.isDeleted = true;
            _userContext.Documents.Update(document);
            await _userContext.SaveChangesAsync();
            return base.Json(new CertificateRequest()
            {
                correct = true,
                text = "Документ успешно удален"
            });
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
            return base.Json(new CertificateRequest()
            {
                correct = true,
                text = "Документ успешно отправлен"
            });
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
