using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoApp.Business.DALManipulation;
using ToDoApp.DAL;
using ToDoApp.DAL.Entity;

namespace ToDoApp.CustomAttribute
{
    public class PageAuthorizeAttribute : AuthorizeAttribute
    {
        public string UserRoles { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            UsersHelper Users = new UsersHelper(new Repository());

            var authCooke = httpContext.Request.Cookies["__AUTH"];

            if (authCooke != null)
            {
                User user = Users.GetUserByCookie(authCooke.Value);

                return user.IsActivated;
            }
            return false;
        }
    }

}