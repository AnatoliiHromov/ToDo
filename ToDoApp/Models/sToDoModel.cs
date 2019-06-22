using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ToDoApp.DAL.Entity;
using System.Web.Mvc;

namespace ToDoApp.Models
{
    public class sToDoModel
    {
        [Required(ErrorMessage="\t*")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "\tIncorrect length*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "\t*")]
        public string Description { get; set; }        
        public States State { get; set; }

        public int TID { get; set; }
    }
}