using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDoApp.Models
{
    public class EditUserModel
    {
        public string uPhoto { get; set; }
        [Required(ErrorMessage = "\t*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "\t*")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "\t*")]
        public string SecondName { get; set; }

        public string Comment { get; set; }

        [Required(ErrorMessage = "\t*")]
        [RegularExpression(@"[+]([0-9]){3,12}|([0-9]){3,12}", ErrorMessage = "\t*Incorrect Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "\t*")]
        [Remote("CheckPassword", "Account", ErrorMessage = "\t*Wrong Password")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "\t*")]
        public string Password { get; set; }
        [Required(ErrorMessage = "\t*")]
        [System.ComponentModel.DataAnnotations.CompareAttribute("Password", ErrorMessage = "\t*Passwords are not match")]
        public string DPassword { get; set; }
        

        [Required(ErrorMessage = "\t*")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            ErrorMessage = "\tIncorrect Email")]
        [Remote("CheckEmail", "Registration", ErrorMessage = "\tEmail is already exist")]
        public string NewEmail { get; set; }
    }
}