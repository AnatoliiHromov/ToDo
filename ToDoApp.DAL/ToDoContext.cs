using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DAL.Entity;

namespace ToDoApp.DAL
{
    public class ToDoContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ToDo> ToDoes { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<SubToDo> SubToDoes { get; set; }
    }
}
