using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable_Generation_Using_GA
{
    class Lecturer
    {

        public string l_name;
        public List<day_slots> days_and_slots;

        public Lecturer(string ln)
        {
            l_name = ln;
            days_and_slots = new List<day_slots>();
        }

        public void set_Lecturer_cons(List<int> ps, int pd)
        {

            day_slots t1 = new day_slots(pd, ps);
            days_and_slots.Add(t1);
        }

    }
}
