using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    class EntityToFileMapping
    {
        public static Student CreateStudent(string line)
        {
            string[] fields = line.Split(','); // new char[] { ',' } 
            Student student= new Student()
            {
                id = Convert.ToInt32(fields[0]),
                name = fields[1],
                groupNumber = fields[2],
                email = fields[3],
                teacherName = fields[4],
                status = Convert.ToBoolean(fields[5])
            };
            return student;
        }



        public static Homework CreateHomework(string line)
        {
            string[] fields = line.Split(','); // new char[] { ',' } 
            Homework homework = new Homework()
            {

                id = Convert.ToInt32(fields[0]),
                receivedWeek = Convert.ToInt32(fields[1]),
                deadlineWeek = Convert.ToInt32(fields[2]),
                description = fields[2]
            };
            return homework;
        }

        public static Grade CreateGrade(string line)
        {
            string[] fields = line.Split(','); // new char[] { ',' } 
            Grade grade= new Grade()
            {
                gradeId = new GradeId(Convert.ToInt32(fields[0]),
                                    Convert.ToInt32(fields[1])),
                grade = Double.Parse(fields[2]),
                week = Int32.Parse(fields[3]),
                feedback = fields[4]
           
            };
            return grade;
        }

    }
}
