using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.DAL.Entity
{
    public class Friend: Entity
    {
        public bool IsAccepted { get; set; }
        public int FromUser { get; set; }
        public virtual User User { get; set; }
    }
}
