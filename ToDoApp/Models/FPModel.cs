using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoApp.Models
{
    public class FPModel
    {        
        [Required(ErrorMessage = "*")]
        public string Email { get; set; }
    }
}