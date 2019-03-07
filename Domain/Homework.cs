using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    class Homework : HasID<int>
    {
        public int number;
        public int deadlineWeek;
        public int receivedWeek;
        public string description;

        public Homework(int number, int received, int deadline, string feedback)
        {
            this.number = number;
            this.deadlineWeek = deadline;
            this.receivedWeek = received;
            this.description = feedback;
        }
        public Homework() { }

        public int id { get { return number; } set { this.number = value; } }

        public int GetID()
        {
            return this.number;
        }

        public void SetID(int id)
        {
            this.number = id;
        }

        public override string ToString()
        {
            return number.ToString() + "," +
                   receivedWeek.ToString() + "," +
                   deadlineWeek.ToString() + "," +
                   description;
        }

        object HasID<int>.GetID()
        {
            return id;
        }

        public override bool Equals(object obj)
        {
            Homework h = (Homework)obj;
            return h.number == this.number;
        }
    }
}
