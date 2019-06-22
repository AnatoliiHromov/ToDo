using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using ToDoApp.Business.DALManipulation;
using ToDoApp.DAL;


namespace ToDoApp.Models
{
    public class RegistrationModel
    {
        public string uPhoto { get; set; }
        [Required(ErrorMessage = "\t*")]
        [StringLength(13,MinimumLength=2,ErrorMessage="\tIncorrect length*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "\t*")]
        [StringLength(13, MinimumLength = 2, ErrorMessage = "\tIncorrect length*")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "\t*")]
        [StringLength(13, MinimumLength = 2, ErrorMessage = "\tIncorrect length*")]
        public string SecondName { get; set; }
        
        public string Comment { get; set; }

        [Required(ErrorMessage = "\t*")]
        [RegularExpression(@"[+]([0-9]){3,12}|([0-9]){3,12}", ErrorMessage = "\tIncorrect Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "*")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",ErrorMessage = "\tIncorrect Email")]
        [Remote("CheckEmail", "Registration",ErrorMessage="\tEmail is already exist")]
        public string Email { get; set; }

        [Required(ErrorMessage = "\t*")]
        [StringLength(13, MinimumLength = 4, ErrorMessage = "\tIncorrect length*")]
        public string Password { get; set; }

        [Required(ErrorMessage = "\t*")]
        [System.ComponentModel.DataAnnotations.CompareAttribute("Password", ErrorMessage = "\t*Passwords are not match")]
        public string DPassword { get; set; }


    }
}