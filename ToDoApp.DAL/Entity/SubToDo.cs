using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.DAL.Entity
{
    public class SubToDo: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public States State { get; set; }

        public virtual ToDo ToDo { get; set; }

    }
}
