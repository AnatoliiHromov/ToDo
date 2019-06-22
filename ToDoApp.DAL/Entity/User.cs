using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.DAL.Entity
{
     public class User: Entity
    {            
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondName { get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }
        public string uPhoto { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool   IsActivated { get; set; }
        public string Cookies { get; set; }

        [NotMapped]
        public int ToDoesCount { get; set; }
        [NotMapped]
        public int FriendsCount { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<ToDo> ToDoes { get; set; }    
    }
}
