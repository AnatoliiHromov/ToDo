using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using System.Net.Mail;
using System.Net;
using ToDoApp.Business.DALManipulation;
using ToDoApp.DAL;
using ToDoApp.DAL.Entity;
using ToDoApp.Business.Helpers;

namespace ToDoApp.Business.Jobs
{
    public class EmailSenderJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            ToDoesHelper Helper = new ToDoesHelper(new Repository());
            int c = 0;
            do
            {
                IEnumerable<ToDo> ToDoes = Helper.GetActualToDoes(100,c);
                if (ToDoes == null|| ToDoes.Count()==0)
                    return;
                foreach (ToDo todo in ToDoes)
                {
                    if (todo.Date.ToUniversalTime() >= DateTime.UtcNow && todo.Date.ToUniversalTime() < DateTime.UtcNow + TimeSpan.FromHours(1))
                    {
                        EMailSender.SendRemaind(todo.User.Email, todo.Name);
                    }
                    else return;                   
                }
                c += 10;
            }
            while(true);
        }
    }
}
