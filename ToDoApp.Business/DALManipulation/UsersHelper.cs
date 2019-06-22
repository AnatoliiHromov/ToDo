using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DAL;
using ToDoApp.DAL.Entity;

namespace ToDoApp.Business.DALManipulation
{
    public class UsersHelper
    {
        public UsersHelper(IRepository repository)
        {
            this.context = repository;
        }

        private IRepository context;

        public User AddUser(User user)
        {
            var d =user;
            user.IsActivated = false;
            context.Add(d);
            context.SaveChanges();
            return d;
        }

        public void DeleteUser(int id)
        {
            var d = GetUser(id);
            context.Delete(d);
            context.SaveChanges();
        }

        public User EditUser(User user)
        {
            var u = GetUser(user.ID);
            u.Name = user.Name;
            u.Surname = user.Surname;
            u.SecondName=user.SecondName;
            u.Phone=user.Phone;
            u.Comment=user.Comment;
            u.uPhoto=user.uPhoto;
            u.Email=user.Email;
            u.Password=user.Password;
            context.SaveChanges();
            return u;
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users.Select(x => { x.ToDoesCount = context.ToDoes.Count(s => s.User.ID == x.ID); return x; });
        }
        public int GetUsersCount()
        {
            return GetUsers().Count();
        }
        public User GetUser(int id)
        {
            return GetUsers().First(x => x.ID == id);
        }

        public User GetUser(string email)
        {
            if (UserExist(email))
                return GetUsers().First(x => x.Email == email);
            else return null;
        }

        public User GetUserByCookie(string cookies)
        {
            return GetUsers().First(x => x.Cookies == cookies);
        }

        public bool CheckPassword(string Email, string Password)
        {
            User u = GetUser(Email);
            if (u.Password == Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UserExist(string Email)
        {
            if(GetUsers().Any(x=>x.Email==Email))
                return true;
            else 
                return false; 
        }

        public User Login(string email, string password)
        {
            return GetUsers().FirstOrDefault(p => string.Compare(p.Email, email, true) == 0 && p.Password == password);
        }


    }
}