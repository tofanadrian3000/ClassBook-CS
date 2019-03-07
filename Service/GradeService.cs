using ConsoleApp1.Domain;
using ConsoleApp1.Repository;
using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Service
{
    class GradeService
    {
        private double penaltyPointsForWeekLate = 2.5;
        private CrudRepository<GradeId, Grade> gradeRepository;
        private StudentService studentService;
        private HomeworkService homeworkService;

        public GradeService()
        {
            string fileName = "..\\..\\Data\\grades.txt";
            this.gradeRepository = new GradeRepository(new GradeValidator(),fileName);
            this.studentService = new StudentService();
            this.homeworkService = new HomeworkService();
        }


        public double PenaltyPointsForStudentOnHomeworkIntoAWeek(Student student, Homework homework, int week, int noExemptions)
        {
            if (week > homework.deadlineWeek)
            {
                int lateWeeks = week - homework.deadlineWeek;
                //int numberOfExemptions = serviceExemption.GetTheNumberOfExemptionOfStudentBetweenWeeks(idStudent, homework.deadlineWeek, week);

                int toPenaltyWeek = lateWeeks - noExemptions;
                if (toPenaltyWeek > 2)
                {
                    return 10;
                }
                else
                {
                    return toPenaltyWeek * penaltyPointsForWeekLate;
                }
            }
            return 0;
        }

        public double GetMaximumGradeForStudentOnHomeworkIntoWeek(Student student, Homework homework, int week, int noExemptions)
        {
            double grade = 10;
            return grade - PenaltyPointsForStudentOnHomeworkIntoAWeek(student, homework, week,noExemptions);
        }

        public Grade AddGrade(Grade grade, int noExemptions)
        {
            Student student = this.studentService.FindStudent(grade.studentId);
            Homework homework = this.homeworkService.FindHomework(grade.homeworkId);
            if (student != null && homework!= null)
            {
                double maximumGrade = GetMaximumGradeForStudentOnHomeworkIntoWeek(student, homework, grade.week,noExemptions);
                if (grade.grade > maximumGrade)
                {
                    throw new Exception("The maximum grade is: " + maximumGrade.ToString());
                }
                return this.gradeRepository.Save(grade);
            }
            else
            {
                return grade;
            }
        }

        public IList<Grade> GetGrades()
        {
            return this.gradeRepository.FindAll();
        }


        public IList<double> HomeworkGrades(int id)
        {

            IEnumerable<double> query = from grade in this.GetGrades()
                                        where grade.homeworkId == id
                                        select grade.grade;


            return query.ToList();
        }

        public List<StudentGradeDTO> CurrentWeekGrades()
        {
            IEnumerable<StudentGradeDTO> query = from student in studentService.GetAvailableStudents()
                                                 select new StudentGradeDTO()
                                                 {
                                                     studentName = student.name,
                                                     grades = (from grade in this.GetGrades()
                                                               where grade.studentId == student.id && grade.week == MyData.CurrentWeek
                                                               select grade).ToList()
                                                 };
            return query.ToList();
        }

        public IList<StudentGradeDTO> GroupGrades(string studentGroup)
        {
            IEnumerable<StudentGradeDTO> query = from student in studentService.GetAvailableStudents()
                                                 where student.groupNumber==studentGroup
                                                 select new StudentGradeDTO()
                                                 {
                                                     studentName = student.name,
                                                     grades = (from grade in this.GetGrades()
                                                               where grade.studentId == student.id
                                                               select grade).ToList()
                                                 };
            return query.ToList();
        }

        public IList<StudentGradeDTO> HomeworkGroupGrades(string studentGroup, int homeworkId)
        {
            IEnumerable<StudentGradeDTO> query = from student in studentService.GetAvailableStudents()
                                                 where studentGroup == student.groupNumber
                                                 select new StudentGradeDTO()
                                                 {
                                                     studentName = student.name,
                                                     grades = (from grade in this.GetGrades()
                                                               where grade.studentId == student.id && grade.homeworkId==homeworkId
                                                               select grade).ToList()
                                                 };
            return query.ToList();
        }

        public StudentGradeDTO StudentGrades(int id)
        {
            Student student = studentService.FindStudent(id);
            if (student == null)
            {
                throw new Exception("Student not found!");
            }
            IEnumerable<Grade> query = from grade in this.GetGrades()
                                       where grade.studentId == id
                                       select grade;
            StudentGradeDTO studentGrade = new StudentGradeDTO
            {
                studentName = student.name,
                grades = query.ToList(),
            };

            return studentGrade;
        }

        public double GetMedium(IList<Grade> grades)
        {
            int gainedWeight = 0;
            int totalWeight = 0;
            ((List<Homework>)homeworkService.GetHomeworks()).ForEach(x => totalWeight += x.deadlineWeek - x.receivedWeek);

            double finalGrade = 0;
            foreach (Grade grade in grades)
            {
                Homework homework = homeworkService.FindHomework(grade.homeworkId);
                int weight = homework.deadlineWeek - homework.receivedWeek;
                finalGrade += grade.grade * weight;
                gainedWeight += weight;
            }
            return (finalGrade + (totalWeight - gainedWeight)) / totalWeight;
        }

        public Double GetAverageGradeForStudent(int id)
        {
            StudentGradeDTO studentGrade = StudentGrades(id);
            return GetMedium(studentGrade.grades);
        }


        public Homework HardestHomework()
        {
            var helper = (from grade in this.GetGrades()
                          group grade by grade.homeworkId into g
                          select new { HomeworkId = g.Key, Average = g.ToList().Average(x => x.grade) }
                         );
            return (

                    from h in helper
                    where h.Average == helper.Min(x => x.Average)
                    select homeworkService.FindHomework(h.HomeworkId)
                    ).FirstOrDefault();
        }

        public List<Student> PassedStudents()
        {
            var helper = (from grade in this.GetGrades()
                          group grade by grade.studentId into g
                          select new { StudentId = g.Key, Average = GetMedium(g.ToList()) }
                         );
            return (
                   from h in helper
                   where h.Average >= 5
                   select studentService.FindStudent(h.StudentId)
                ).ToList();
        }

        public List<StudentGradeDTO> FinalGrades()
        {
            var helper = (from grade in this.GetGrades()
                          group grade by grade.studentId into g
                          select new { StudentId = g.Key, Average = GetMedium(g.ToList()) }
                         );
            return (
                   from h in helper
                   where h.Average >= 5
                   select new StudentGradeDTO
                   {
                       studentName = studentService.FindStudent(h.StudentId).name,
                       grades = new List<Grade>(new Grade[] { new Grade(1,1,h.Average,1,"asd")})
                   }
                ).ToList();
        }
    }
}
