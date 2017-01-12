using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable_Generation_Using_GA
{
    class day_slots
    {

        public int day;
        public List<int> slots;

        public day_slots(int d, List<int> s)
        {
            day = d;
            slots = s;
        }
    }
}
