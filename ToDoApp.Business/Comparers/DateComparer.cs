using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Business.Comparers
{
    public class DateComparer:IComparer<DateTime>
    {
        public int Compare(DateTime x,DateTime y)
        {
            DateTime a = DateTime.UtcNow;
            if (x == a)
            {
                return -1;
            }
            else if (x > a)
            {
                if (x < y)
                    return -1;
                else if (x == y)
                    return 0;
                else
                {
                    if (a > y)
                        return -1;
                    else return 1;
                }
            }
            else if (x < a)
            {
                if (x > y)
                    return -1;
                else if (x == y)
                    return 0;
                else
                    return 1;
            }
            else
                return 0;
        }
    }
}
