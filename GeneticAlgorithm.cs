using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Timetable_Generation_Using_GA
{
    public partial class GeneticAlgorithm : Form
    {
         Queen[] rand;
         Lecturer[] lrand;
         List<Chromosome> InitialPopulation = new List<Chromosome>();
         Thread Genetic_algo;
         int gens;
         Chromosome C1, C2;
         StreamWriter sw = new StreamWriter(@"../results.txt");
        public GeneticAlgorithm(object r,object l)
        {
            InitializeComponent();
            List<Queen> r1 = (List<Queen>)r;
            List<Lecturer> l1=(List<Lecturer>)l;
            rand = new Queen[r1.Count];
            lrand = new Lecturer[l1.Count];
            for (int i = 0; i < rand.Length; i++)
            {
                lrand[i] = l1[i];
                rand[i] = r1[i];
            }
            Chromosome c1 = new Chromosome(rand);
            c1.setRandomAlloc();
            InitialPopulation.Add(c1);
            setFitness(InitialPopulation[InitialPopulation.Count - 1]);

            while (true)
            {
                Chromosome c = new Chromosome(rand);
                c.setRandomAlloc();
                if (InitialPopulation.Count > 0)
                {
                    if (compare_chromosomes(c, InitialPopulation[InitialPopulation.Count - 1]) == false)
                    {

                        InitialPopulation.Add(c);
                        setFitness(InitialPopulation[InitialPopulation.Count - 1]);

                    }
                }
                if (InitialPopulation.Count == 5)
                    break;
            }
          
            Genetic_algo = new Thread(GA);
            Genetic_algo.Start();
        }

        public  void setFitness(object C1)
        {
         
            Chromosome C = (Chromosome)C1;
            int howfit = 0, count = 0,last_j=-1,consecs=0;
            bool ftime=true;
            for (int l = 0; l < lrand.Length; l++)
            {
                for (int i = 0; i < C.Alloc.GetLength(0); i++)
                {
                    for (int j = 0; j < C.Alloc.GetLength(1); j++)
                    {
                        if (C.Alloc[i, j] != null)
                        {
                            if (lrand[l].l_name == C.Alloc[i, j].lecturer.l_name)
                            {
                                if (satisfyconstraint(lrand[l], i, j) == false)
                                {
                                    howfit--;

                                }

                            }
                        }
                        else
                        {
                            if (j - last_j == 1)
                            { count++;
                            consecs = count;
                            }
                            else
                                count = 0;
                            last_j = j;
                        }
                    }
                    if (consecs >= 1 && ftime)
                        howfit-=consecs;
                    count = 0;
                }

                ftime = false;
            }

            C.Fitness = howfit;
        }


        public void GA()
        {
            Random R = new Random();
            int a = 0, b = 0,count=0;
            gens = 0;
            while (true)
            {
                a = R.Next(0, InitialPopulation.Count);
                b = R.Next(0, InitialPopulation.Count);
                crossover(a, b);
                if (C1 != null&&C2!=null)
                {
                    setFitness(C1);
                    if (C1.Fitness < InitialPopulation[0].Fitness)
                        Mutate(C1);
                    InitialPopulation.Add(C1);
                    setFitness(C2);
                    if (C2.Fitness < InitialPopulation[0].Fitness)
                        Mutate(C2);
                    InitialPopulation.Add(C2);
                    gens++;
                    count++;
                    find_best();
                    printAlloc(InitialPopulation[InitialPopulation.Count - 1]);
                    fitness.Text = InitialPopulation[InitialPopulation.Count - 1].Fitness.ToString();
                    generations.Text = gens.ToString();
                    total_pop.Text = InitialPopulation.Count.ToString();
                    Thread.Sleep(1000);
                }
                if (count == 5)
                {
                    Culling();
                    count = 0;
                }
            }
        }

        public void printAlloc(object c1)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            Chromosome c = (Chromosome)c1;
            string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
           
            for (int i = 0; i < c.Alloc.GetLength(0); i++)
            {
                string[] s = new string[5];
                s[0]=days[i];
                sw.Write(days[i]+" ");
                for (int j = 0; j < c.Alloc.GetLength(1); j++)
                {

                    if (c.Alloc[i, j] != null)
                    {
                        s[j + 1] = c.Alloc[i, j].lecturer.l_name + "->" + c.Alloc[i, j].course_name;

                        sw.Write(c.Alloc[i, j].lecturer.l_name + "->" + c.Alloc[i, j].course_name + " ");
                    }
                    else
                    { 
                        s[j + 1] = "No Class";
                        sw.Write("No Class");
                    }
                }
                dataGridView1.Rows.Add(s);
                sw.WriteLine();
            }

            sw.WriteLine("\n\n\n\n");
       }

        public void find_best()
        {
            InitialPopulation.Sort(delegate(Chromosome x, Chromosome y) { return x.Fitness.CompareTo(y.Fitness); });
        }

        public  void Mutate(object a1)
        {
            Chromosome a = (Chromosome)a1;
            Random R = new Random();
            int r = 0;
            int c = 0;
            Queen q1;
            while (true)
            {
                r = R.Next(0, 5);
                c = R.Next(0, 4);
                q1 = a.Alloc[r, c];
                if (q1 != null)
                    break;
            }
            a.Alloc[r, c] = null;
            while (true)
            {
                int r1 = R.Next(0, 5);
                int c1 = R.Next(0, 4);
                if (r1 != r || c1 != c)
                {
                    a.Alloc[r1, c1] = q1;
                    break;
                }
            }

        }

        public  void crossover(int a, int b)
        {
            Random R = new Random();
            C1 = new Chromosome(null);
            C2 = new Chromosome(null);
            int c = R.Next(0, 4);
            for (int i = 0; i < C1.Alloc.GetLength(0); i++)
            {

                for (int j = 0; j < c; j++)
                {
                    if (InitialPopulation[a].Alloc[i, j] != null)
                        C1.Alloc[i, j] = InitialPopulation[a].Alloc[i, j];

                }


                for (int j = c; j < 4; j++)
                {
                    if (InitialPopulation[a].Alloc[i, j] != null)
                        C2.Alloc[i, j] = InitialPopulation[a].Alloc[i, j];

                }


                for (int j = 0; j < c; j++)
                {
                    if (InitialPopulation[b].Alloc[i, j] != null)
                        C2.Alloc[i, j] = InitialPopulation[b].Alloc[i, j];

                }

                for (int j = c; j < 4; j++)
                {
                    if (InitialPopulation[b].Alloc[i, j] != null)
                        C1.Alloc[i, j] = InitialPopulation[b].Alloc[i, j];

                }

            }
     
        }

        public  void Culling()
        {
            int l_fit = InitialPopulation[0].Fitness;
            InitialPopulation.RemoveAt(0);
            while (l_fit == InitialPopulation[0].Fitness)
            {
                InitialPopulation.RemoveAt(0);
                if (InitialPopulation.Count == 2)
                    break;
            }
        }

        public  bool compare_chromosomes(object a1, object b1)
        {
            Chromosome a = (Chromosome)a1;
            Chromosome b = (Chromosome)b1;
            for (int i = 0; i < a.Alloc.GetLength(0); i++)
            {
                for (int j = 0; j < a.Alloc.GetLength(1); j++)
                {
                    if (a.Alloc[i, j] != null && b.Alloc[i, j] != null)
                    {
                        if (a.Alloc[i, j].lecturer.l_name != b.Alloc[i, j].lecturer.l_name)
                            return false;
                    }
                    if ((a.Alloc[i, j] != null && b.Alloc[i, j] == null) || (b.Alloc[i, j] != null && a.Alloc[i, j] == null))
                        return false;
                }

            }
            return true;
        }

        public  bool satisfyconstraint(object L1, int i, int j)
        {
           Lecturer L = (Lecturer)L1;
            for (int d = 0; d < L.days_and_slots.Count; d++)
            {
                if (L.days_and_slots[d].day == i)
                {

                    for (int k = 0; k < L.days_and_slots[d].slots.Count; k++)
                    {
                        if (L.days_and_slots[d].slots[k] == j)
                            return true;
                    }

                }
            }
            return false;
        }



        private void GeneticAlgorithm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Genetic_algo.Suspend();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Genetic_algo.Resume();
        }
    }
}
