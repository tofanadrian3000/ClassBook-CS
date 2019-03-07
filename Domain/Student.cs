using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    class Student : HasID<int>
    {

        public int id { get; set; }
        public string name { get; set; }
        public string groupNumber { get; set; }
        public string email { get; set; }
        public string teacherName { get; set; }
        public bool status { get; set; }

        public Student(int id, string name, string group, string email, string teacher, bool status)
        {
            this.id = id;
            this.name = name;
            this.groupNumber = group;
            this.email = email;
            this.teacherName = teacher;
            this.status = status;
        }

        public Student() { }

        public int GetID()
        {
            return this.id;
        }

        public void SetID(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            return this.id == ((Student)obj).id;
        }

        public override string ToString()
        {
            return id.ToString() + "," +
                   name + "," +
                   groupNumber + "," +
                   email + "," +
                   teacherName + "," +
                   status.ToString();
        }

       object HasID<int>.GetID()
        {
            return id;
        }

    }
}
