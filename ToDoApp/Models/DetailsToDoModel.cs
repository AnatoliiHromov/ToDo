using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ToDoApp.DAL.Entity;
using System.Web.Mvc;

namespace ToDoApp.Models
{
    public class DetailsToDoModel
    {
        public IEnumerable<SubToDo> SubTodoes { get; set; }
        [Required(ErrorMessage="\t*")]
        public string Name { get; set; }
        [Required(ErrorMessage="\t*")]
        public string Description { get; set; }
        public int TID { get; set; }
        public States State { get; set; }

        public string Lat { get; set; }
        public string Long { get; set; }

        public virtual ToDo ToDo { get; set; }
    }
}