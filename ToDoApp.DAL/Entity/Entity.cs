using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ToDoApp.DAL.Entity
{
    public class Entity
    {
        [Key]
        public int ID { get; set; }
    }
}
