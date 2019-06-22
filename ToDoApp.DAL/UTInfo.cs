using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ToDoApp.Business
{
    public class UTInfo
    {
        public UTInfo(int UCount, int TCount)
        {
            this.UCount = UCount;
            this.TCount = TCount;
        }
        public int UCount { get; private set; }
        public int TCount { get; private set; }
    }       
}
