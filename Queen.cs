using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable_Generation_Using_GA
{
    class Queen
    {
        public Lecturer lecturer;
        public string course_name, room_name;

        public Queen(Lecturer ln, string cn)
        {
            lecturer = ln;
            course_name = cn;
            room_name = "GF-17";
        }


    }
}
