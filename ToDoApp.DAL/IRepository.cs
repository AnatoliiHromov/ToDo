using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DAL.Entity;

namespace ToDoApp.DAL
{
    public interface IRepository
    {
        IEnumerable<User> Users { get; }
        IEnumerable<ToDo> ToDoes { get; }
        IEnumerable<Friend> Friends { get; }
        IEnumerable<SubToDo> SubToDoes { get; }
        void Add<T>(T entity) where T : ToDoApp.DAL.Entity.Entity;
        void Delete<T>(T entity) where T : ToDoApp.DAL.Entity.Entity;
        void SaveChanges();
    }
}
