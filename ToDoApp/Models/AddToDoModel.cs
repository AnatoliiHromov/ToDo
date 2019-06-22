using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ToDoApp.DAL.Entity;
using System.Web.Mvc;

namespace ToDoApp.Models
{
    public class AddToDoModel
    {
        public string address { get; set; }
        [Required(ErrorMessage="\t*")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "\tIncorrect length*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "\t*")]
        public string Description { get; set; }        
        public States State { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd H:mm:ss}", ApplyFormatInEditMode=true)]
        [DataType(DataType.DateTime)]   
        public DateTime Date { get; set; }

        public int ID { get; set; }
        public string Long { get; set; }
        public string Lat { get; set; }
        public bool IsPublic { get; set; }



    }
}