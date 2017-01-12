using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timetable_Generation_Using_GA
{
    public partial class Form2 : Form
    {
        List<string> items = new List<string>();
        List<Queen> rand = new List<Queen>();
        List<Lecturer> lrand = new List<Lecturer>();
        public Form2()
        {
            InitializeComponent();
            listBox1.DataSource = items;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            Lecturer L1 = new Lecturer(textBox1.Text.ToString());
            string s = textBox1.Text + "   " + textBox2.Text + "   ";
            if (days.GetItemChecked(0))//Monday
            {
               List<int> slot=new List<int>();
               s += "Monday->";
                for (int i = 1; i <=4; i++)
                {
                    if (days.GetItemChecked(i))
                    {
                       
                        s += days.Items[i]+" ";
                        slot.Add(i - 1); }
                }
                L1.set_Lecturer_cons(slot, 0);
                for (int i = 0; i <=4; i++)
                {
                    days.SetItemChecked(i, false);
                }
            }

            if (day1.GetItemChecked(0))//Tuesday
            {
                List<int> slot = new List<int>();
                s += "Tuesday->";
                for (int i = 1; i <= 4; i++)
                {
                    if (day1.GetItemChecked(i))
                    {
                        s += day1.Items[i] + " ";
                        slot.Add(i - 1); }
                }
                L1.set_Lecturer_cons(slot, 1);
                for (int i = 0; i <= 4; i++)
                {
                    day1.SetItemChecked(i, false);
                }
            }

            if (day2.GetItemChecked(0))//Wednesday
            {
                List<int> slot = new List<int>();
                s += "Wednesday->";
                for (int i = 1; i <= 4; i++)
                {
                    if (day2.GetItemChecked(i))
                    {
                        s += day2.Items[i] + " ";
                        slot.Add(i - 1); }
                }
                L1.set_Lecturer_cons(slot, 2);
                for (int i = 0; i <= 4; i++)
                {
                    day2.SetItemChecked(i, false);
                }
            }

            if (day3.GetItemChecked(0))//Thday
            {
                List<int> slot = new List<int>();
                s += "Thrusday->";
                for (int i = 1; i <= 4; i++)
                {
                    if (day3.GetItemChecked(i))
                    {
                        s += day3.Items[i] + " ";
                        slot.Add(i - 1);
                    }
                }
                L1.set_Lecturer_cons(slot, 3);
                for (int i = 0; i <= 4; i++)
                {
                    day3.SetItemChecked(i, false);
                }
            }
            if (day4.GetItemChecked(0))//friday
            {
                List<int> slot = new List<int>();
                s += "Friday->";
                for (int i = 1; i <= 4; i++)
                {
                    if (day4.GetItemChecked(i))
                    {
                        s += day4.Items[i] + " ";
                        slot.Add(i - 1);
                    }
                }
                L1.set_Lecturer_cons(slot, 4);
                for (int i = 0; i <= 4; i++)
                {
                    day4.SetItemChecked(i, false);
                }
            }
            lrand.Add(L1);
            Queen q1 = new Queen(L1, textBox2.Text.ToString());
            rand.Add(q1);
            items.Add(s);
            listBox1.DataSource = null;
            listBox1.DataSource = items;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (rand.Count == 0)
            {
                MessageBox.Show("No Enteries..." , "ERROR!");
                return;
            }
            GeneticAlgorithm g1 = new GeneticAlgorithm(rand,lrand);
            g1.Show();
        }
    }
}
