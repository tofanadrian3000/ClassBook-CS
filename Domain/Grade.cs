using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    class Grade : HasID<GradeId>
    {
        public GradeId gradeId;
        public double grade;
        public int week;
        public string feedback;

        public Grade(int studentId, int homeworkId, double grade, int week, string feedback)
        {
            this.gradeId = new GradeId(studentId, homeworkId);
            this.grade = grade;
            this.week = week;
            this.feedback = feedback;
        }

        public Grade()
        {
        }

        public GradeId GetID()
        {
            return gradeId;
        }

        public void SetID(GradeId id)
        {
            gradeId = id;
        }

        public int studentId
        {
            get { return gradeId.studentId; }
            set { gradeId.studentId = value; }
        }

        public int homeworkId
        {
            get { return gradeId.homeworkId; }
            set { gradeId.homeworkId = value; }
        }

        public GradeId id { get { return gradeId; } set { gradeId = value; } }

        public override string ToString()
        {
            return this.studentId + "," +
                   this.homeworkId + "," +
                   this.grade + "," +
                   this.week + "," +
                   this.feedback;
        }

        object HasID<GradeId>.GetID()
        {
            return gradeId;
        }

        public override bool Equals(object obj)
        {
            Grade g = (Grade)obj;
            return g.gradeId==this.gradeId;
        }
    }
}
