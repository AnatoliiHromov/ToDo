using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Business.Comparers;
using ToDoApp.DAL;
using ToDoApp.DAL.Entity;

namespace ToDoApp.Business.DALManipulation
{
    public class ToDoesHelper
    {
        public ToDoesHelper(IRepository repository)
        {
            this.context = repository;
            this.users = new UsersHelper(this.context);
        }

        private IRepository context;
        private UsersHelper users;

        public ToDo AddToDo(ToDo t, int userID)
        {
            var d = t;           
            d.User = users.GetUser(userID);
            context.Add(d);
            context.SaveChanges();
            return d;
        }

        public void DeleteToDo(int todoID)
        {
            var d = GetToDo(todoID);
            context.Delete(d);
            context.SaveChanges();
        }

        public ToDo EditToDo(ToDo t, int UID, int TID)
        {
            var d = GetToDo(TID);
            d.Name = t.Name;
            d.Date = t.Date;
            d.GeoLong = t.GeoLong;
            d.GeoLat = t.GeoLat;
            d.Description = t.Description;
            d.State = t.State;
            d.User = users.GetUser(UID);
            context.SaveChanges();
            return d;
        }
        public int GetUID(int TID)
        {
            return GetToDo(TID).User.ID;
        }
        public IEnumerable<ToDo> GetToDoes()
        {
            return context.ToDoes;
        }
        public int ToDoesCount()
        {
            return context.ToDoes.Count();
        }
        public IEnumerable<ToDo> GetToDoes(string Token)
        {
            return context.ToDoes.Where(x => x.User != null && x.User.ID == int.Parse(Token)).ToList();
        }
        public IEnumerable<ToDo> GetActualToDoes(int Count,int from)
        {
            var count = context.ToDoes.Count();
            if ( count > from)
                return context.ToDoes
                    .Where(t => t.State == ToDoApp.DAL.Entity.States.NotDone
                        && t.Date.ToUniversalTime().Day >= DateTime.UtcNow.Day)
                    .OrderBy(t => t.Date.ToUniversalTime(),new DateComparer())
                    .Skip(from)
                    .Take(ToDoesCount() > Count ? Count : ToDoesCount())
                    .ToList();
            else return null;
        }

        public ToDo GetToDo(int todoID)
        {
            return GetToDoes().First(x => x.ID == todoID);
        }
    }
}
