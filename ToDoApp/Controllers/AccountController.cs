using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using ToDoApp.Filters;
using ToDoApp.Models;
using ToDoApp.DAL;
using ToDoApp.DAL.Entity;
using System.Threading.Tasks;
using ToDoApp.CustomAttribute;
using System.Net.Mail;
using System.IO;
using ToDoApp.Business.DALManipulation;
using ToDoApp.Business.Helpers;
using ToDoApp.Helpers;

namespace ToDoApp.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View(new AuthModel());
        }
        [HttpPost]
        public ActionResult Login(AuthModel logModel)
        {
            UsersHelper users = new UsersHelper(new Repository());
            if (ModelState.IsValid)
            {
                User user = users.GetUser(logModel.Email);
                if (user != null && SecurityHelper.Hash(logModel.Password) == user.Password)
                {
                    if (user.IsActivated)
                    {
                        AuthHelper.LogInUser(HttpContext, user.Cookies);
                        return RedirectToAction("Index", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Email not confirmed");
                    }
                }
                else
                    ModelState.AddModelError("Password", "Entered wrong data or user not exist");
            }
            return View(logModel);
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(FPModel logModel)
        {
            UsersHelper Users = new UsersHelper(new Repository());
            if (ModelState.IsValid)
            {
                User user = Users.GetUser(logModel.Email);
                if (user != null)
                {
                    string password = Membership.GeneratePassword(8, 0);
                    user.Password = SecurityHelper.Hash(password); 
                    Users.EditUser(user);
                    // наш email с заголовком письма
                    MailAddress from = new MailAddress("stimul360@gmail.com", "ToDoTemplate");
                    // кому отправляем
                    MailAddress to = new MailAddress(user.Email);
                    // создаем объект сообщения
                    MailMessage m = new MailMessage(from, to);
                    // тема письма
                    m.Subject = "Password Changing";
                    // текст письма - включаем в него ссылку
                    m.Body = string.Format("The new password:" + password);
                    m.IsBodyHtml = true;
                    // адрес smtp-сервера, с которого мы и будем отправлять письмо
                    SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                    // логин и пароль
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential("stimul360@gmail.com", "Sonyericsson0991317388");
                    try
                    {
                        smtp.Send(m);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Smtp not avaible");
                    }
                    ViewBag.Message = "New password sended at your email.";
                    return RedirectToAction("Login", "Account");
                }
                else
                    ModelState.AddModelError("", "Пользователь не существует");
            }
            return View(logModel);
        }
 
        [PageAuthorize(UserRoles = "User")]
        public ActionResult Index()
        {
            User user = Helpers.AuthHelper.GetUser(HttpContext);
            return View(user);
        }
        [PageAuthorize(UserRoles = "User")]
        public ActionResult LogOff()
        {
            Helpers.AuthHelper.LogOffUser(HttpContext);

            return RedirectToAction("Index", "Home");
        }
        [PageAuthorize(UserRoles = "User")]
        public ActionResult Edit()
        {
            User user = Helpers.AuthHelper.GetUser(HttpContext);
            EditUserModel EditModel = new EditUserModel()
            {
                Comment = user.Comment,
                Name = user.Name,
                SecondName = user.SecondName,
                Surname = user.Surname,
                Phone = user.Phone,
                uPhoto = user.uPhoto
            };
            ViewBag.Message = "Information was updated";
            return View(EditModel);
        }
        [PageAuthorize(UserRoles = "User")]
        [HttpPost]
        public ActionResult Edit(User user, HttpPostedFileBase file)
        {
            User u = Helpers.AuthHelper.GetUser(HttpContext);
            UsersHelper Helper = new UsersHelper(new Repository());
            if (file != null)
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(file.FileName);
                // сохраняем файл в папку Files в проекте
                file.SaveAs(Server.MapPath("~/Files/" + fileName));
                user.uPhoto = Server.MapPath("~/Files/" + fileName);
            }
            else
            {
                user.uPhoto = u.uPhoto;
            }
            user.IsActivated = u.IsActivated;
            user.Password = u.Password;
            user.Cookies = u.Cookies;
            user.Email = u.Email;
            user.ID = u.ID;
            Helper.EditUser(user);
            ViewBag.Message = "Information was updated";
            return RedirectToAction("Index", "Account");
        }
        public ActionResult GetImg(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                    return File(file.FullName, "text/plain", file.Name);
            }
            else
            {
                FileInfo file = new FileInfo(Path.GetFullPath(Server.MapPath("~/Files/" + "default-avatar.png")));
                if (file.Exists)
                    return File(file.FullName, "text/plain", file.Name);
            }
            return Content("");
        }
        public JsonResult CheckPassword(string OldPassword)
        {
            UsersHelper Helper = new UsersHelper(new Repository());
            User u = AuthHelper.GetUser(HttpContext);
            string Hash = SecurityHelper.Hash(OldPassword);
            return Json(Helper.CheckPassword(u.Email, Hash), JsonRequestBehavior.AllowGet);
        }
        [PageAuthorize(UserRoles = "User")]
        public ActionResult ChangePassword(string Password)
        {
            User u = AuthHelper.GetUser(HttpContext);
            UsersHelper Helper = new UsersHelper(new Repository());
            u.Password = SecurityHelper.Hash(Password);
            Helper.EditUser(u);
            ViewBag.Message = "Password was changed";
            return RedirectToAction("Index", "Account");
        }
        [PageAuthorize(UserRoles = "User")]
        public ActionResult ChangeEmail(EditUserModel EUModel)
        {
            User u = Helpers.AuthHelper.GetUser(HttpContext);
            UsersHelper Helper = new UsersHelper(new Repository());
            // наш email с заголовком письма
            MailAddress from = new MailAddress("stimul360@gmail.com", "ToDoTemplate");
            //кому отправляем
            MailAddress to = new MailAddress(EUModel.NewEmail);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Email change";
            // текст письма - включаем в него ссылку
            m.Body = string.Format("To complete the change of mail, please follow the link:" +
                            "<a href=\"{0}\" title=\"Confirm mail change\">{0}</a>",
                Url.Action("ChangeEmail", "Account", new { Token = u.ID, Email = EUModel.NewEmail }, Request.Url.Scheme));
            m.IsBodyHtml = true;
            // адрес smtp-сервера, с которого мы и будем отправлять письмо
            SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("stimul360@gmail.com", "Sonyericsson0991317388");
            smtp.Send(m);
            ViewBag.Message = "Email was changed";
            return View(EUModel);
        }
        [AllowAnonymous]
        public string Confirm(string Email)
        {
            return "Dear user " + Email + 
                    "\nWe sent further instructions on completing the change of the Email";
        }
        [AllowAnonymous]
        public ActionResult ChangeEmail(string Token, string Email)
        {
            UsersHelper Helper = new UsersHelper(new Repository());
            User user = Helper.GetUser(int.Parse(Token));
            if (user != null)
            {
                user.Email = Email;
                user.ID = int.Parse(Token);
                Helper.EditUser(user);
                return RedirectToAction("Login", "Account", new { ConfirmedEmail = user.Email });
            }
            else
            {
                return RedirectToAction("UserNotExist", "Error", new { Token = Token });
            }
        }
    }
}
