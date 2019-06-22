using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ToDoApp.Business.DALManipulation;
using ToDoApp.DAL;
using ToDoApp.DAL.Entity;
using ToDoApp.Models;
using System.Net.Mail;
using System.Configuration;

namespace ToDoApp.Controllers
{
    public class RegistrationController : Controller
    {
        public ActionResult Registration()
        {
            return View();
        }

        //
        // GET: /Registration/
        [HttpPost]
        public ActionResult Registration(User user, HttpPostedFileBase file, RegistrationModel regmodel)
        {
            if (ModelState.IsValid)
            {
                UsersHelper Helper = new UsersHelper(new Repository());
                if (file != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(file.FileName);
                    // сохраняем файл в папку Files в проекте
                    file.SaveAs(Server.MapPath("~/Files/" + fileName));
                    user.uPhoto = Server.MapPath("~/Files/" + fileName);
                }
                user.Cookies = Guid.NewGuid().ToString(); // cookie для авторизации
                user.IsActivated = false; // аккаунт заблокирован
                user.Password = Helpers.SecurityHelper.Hash(user.Password);
                Helper.AddUser(user);
                user = Helper.GetUser(user.Email);
                // наш email с заголовком письма
                MailAddress from = new MailAddress(ConfigurationManager.AppSettings["smptplogin"], "ToDoTemplate");
                // кому отправляем
                MailAddress to = new MailAddress(user.Email);
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = "Email confirmation";
                // текст письма - включаем в него ссылку
                m.Body = string.Format("For complete the registration please follow this link" +
                                "<a href=\"{0}\" title=\"Confirm registration\">{0}</a>",
                    Url.Action("ConfirmEmail", "Registration", new { Token = user.ID, Email = user.Email }, Request.Url.Scheme));
                m.IsBodyHtml = true;
                // адрес smtp-сервера, с которого мы и будем отправлять письмо
                SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                // логин и пароль
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["smptplogin"], ConfigurationManager.AppSettings["smptpPassword"]);
                try
                {
                    smtp.Send(m);
                }
                catch (Exception )
                {
                    return View("regmodel");
                }
                return RedirectToAction("Confirm", "Registration", new { Email = user.Email });
            }
            else
           return View("regmodel");
        }

        [AllowAnonymous]
        public string Confirm(string Email)
        {
            return "Dear user " + Email +
                    "\nWe sent further instructions on completing the registration";
        }

        public JsonResult CheckEmail(string email)
        {
            UsersHelper Helper = new UsersHelper(new Repository());            
            return Json(!Helper.UserExist(email), JsonRequestBehavior.AllowGet);
        }
        
        [AllowAnonymous]
        public ActionResult ConfirmEmail(string Token, string Email)
        {
            UsersHelper Helper = new UsersHelper(new Repository());
            User user = Helper.GetUser(int.Parse(Token));
            if (user != null)
            {
                if (user.Email == Email)
                {
                    user.IsActivated = true;
                    user.ID = int.Parse(Token);
                    Helper.EditUser(user);
                    return RedirectToAction("Index", "Home", new { ConfirmedEmail = user.Email });
                }
                else
                {
                    return RedirectToAction("Confirm", "Account", new { Email = user.Email });
                }
            }
            else
            {
                return RedirectToAction("Confirm", "Account", new { Email = "" });
            }
        }
    }
}
