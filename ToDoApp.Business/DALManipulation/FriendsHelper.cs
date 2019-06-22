using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DAL;
using ToDoApp.DAL.Entity;

namespace ToDoApp.Business.DALManipulation
{
    public class FriendsHelper
    {
        public FriendsHelper(IRepository repository)
        {
            this.context = repository;
            this.users = new UsersHelper(this.context);
        }
        private IRepository context;
        private UsersHelper users;
        public Friend AddFriend(int UID,int FUID)
        {
            var f = new Friend()
            {
                FromUser=FUID,
                IsAccepted=false             
            };
            f.User = users.GetUser(UID);
            context.Add(f);
            context.SaveChanges();
            return f;
        }
        public void DeleteFriend(int friendID) 
        {
            var f = GetFriend(friendID);
            context.Delete(f);
            context.SaveChanges();
        }
        public Friend GetFriend(int friendID)
        {
            return GetFriends().First(x => x.ID == friendID);
        }
        public IEnumerable<Friend> GetFriends(int userID)
        {
            return context.Friends.Where(x => x.User != null && x.User.ID == userID).ToList();
        }
        public IEnumerable<Friend> GetFriends()
        {
            return context.Friends;
        }
        public int FriendsCount(int UID)
        {
            return GetFriends(UID).Count();
        }
       
    }
}
