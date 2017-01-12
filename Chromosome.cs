using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable_Generation_Using_GA
{
    class Chromosome
    {
        public Queen[,] Alloc;
        int fitness;
        Queen[] input;


        public int Fitness
        {
            get { return fitness; }
            set { fitness = value; }
        }


        public Chromosome(Queen[] inp)
        {
            input = inp;
            Alloc = new Queen[5, 4];
            fitness = 0;
        }

        public void setRandomAlloc()
        {
            //set random allocations here
            int r = -1, c = -1, count = 0;


            Random R = new Random();

            while (true)
            {
                r = R.Next(0, 5);
                c = R.Next(0, 4);
                if (Alloc[r, c] == null)
                {
                    Alloc[r, c] = input[count];
                    count++;
                }

                if (count == input.Length)
                    break;

            }




        }

        
        

        }


    }

