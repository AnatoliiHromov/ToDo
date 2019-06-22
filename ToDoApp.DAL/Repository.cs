using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DAL.Entity;

namespace ToDoApp.DAL
{
    public class Repository : IRepository
    {
        ToDoContext context = new ToDoContext();

        public IEnumerable<User> Users
        {
            get
            {
                return context.Users.ToList();
            }
        }

        public IEnumerable<ToDo> ToDoes
        {
            get
            {
                return context.ToDoes.Include("User").ToList();
            }
        }

        public IEnumerable<Friend> Friends
        {
            get 
            { 
                return context.Friends.Include("User").ToList(); 
            }
        }

        public IEnumerable<SubToDo> SubToDoes
        {
            get
            {
                return context.SubToDoes.Include("ToDo").ToList();
            }
        }

        public void Add<T>(T entity) where T : ToDoApp.DAL.Entity.Entity
        {
            this.context.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : ToDoApp.DAL.Entity.Entity
        {
            this.context.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
