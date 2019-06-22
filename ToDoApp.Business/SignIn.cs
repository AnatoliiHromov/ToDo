using System.Linq;
using ToDoApp.DAL.Entity;

namespace ToDoApp.Business
{
    public class SignIn : Guest
    {
        public int ToDoesNumber(User user)
        {
            var ToDoes = user.ToDoes.ToList();
            return ToDoes != null ? ToDoes.Count() : 0;
        }

    }
}
