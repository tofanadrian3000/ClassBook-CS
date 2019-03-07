using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    class StudentGradeDTO
    {
        public String studentName;
        public IList<Grade> grades;

        public override string ToString()
        {
            string s = studentName + ":\n";
            ((List<Grade>)grades).ForEach(x => s += "homework: " + x.homeworkId.ToString() + "  grade: " + x.grade.ToString() + "\n");
            return s;
        }
    }
}
