using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    class GradeId
    {
        public int studentId;
        public int homeworkId;

        public GradeId(int studentId, int homeworkId)
        {
            this.studentId = studentId;
            this.homeworkId = homeworkId;
        }

        public override bool Equals(object obj)
        {
            GradeId g = (GradeId)obj;
            return g.studentId == this.studentId && g.homeworkId == this.homeworkId;
        }
    }
}
