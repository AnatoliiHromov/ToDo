using System.Collections.Generic;
using System.Linq;
using ToDoApp.DAL;
using ToDoApp.DAL.Entity;
using ToDoApp.Business.DALManipulation;

namespace ToDoApp.Business
{
    public class Guest
    {
        Repository repo = new Repository();
        public int ToDoesCount()
        {
            ToDoesHelper todoes = new ToDoesHelper(repo);
            return todoes.ToDoesCount();
        }
        public int UsersCount()
        {
            UsersHelper users = new UsersHelper(repo);
            return users.GetUsersCount();
        }
    }
}
