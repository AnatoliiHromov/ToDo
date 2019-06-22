using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Business.Helpers
{
    public static class EMailSender
    {
        const string Host = "stimul360@gmail.com";
        const string Pass = "Sonyericsson0991317388";
        public static void SendRemaind(string Email, string ToDoName) {
            MailAddress from = new MailAddress("stimul360@gmail.com", "ToDoTemplate");
            // кому отправляем
            MailAddress to = new MailAddress(Email);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Remaind about expired todo";
            // текст письма - включаем в него ссылку
            m.Body = string.Format("Attention for doing "+ToDoName+" you have less than 1 hour.");
            m.IsBodyHtml = true;
            // адрес smtp-сервера, с которого мы и будем отправлять письмо
            SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential(Host, Pass);
            try
            {
                smtp.Send(m);
            }
            catch (Exception)
            {
            }
        }
    }
}
