using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.DAL.Entity
{
    public class ToDo: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public States State { get; set; }
        public DateTime Date { get; set; }
        public double GeoLong { get; set; }
        public double GeoLat { get; set; }
        public bool IsPublic { get; set; }
        public virtual User User { get; set; }

        [NotMapped]
        public int SubTodoesCount { get; set; }
        public virtual ICollection<SubToDo> SubToDoes { get; set; }  
        
    }
}
