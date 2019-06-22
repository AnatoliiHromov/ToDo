using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDoApp.Controllers
{
    public class ErrorController : Controller
    {

        public string UserNotExist(string Token)
        {
            return "Some trouble\n" + "User " + Token + " not exist.";
        }

    }
}
