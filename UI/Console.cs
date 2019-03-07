using ConsoleApp1.Domain;
using ConsoleApp1.Service;
using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.UI
{
    class MyConsole
    {
        private StudentService studentService;
        private HomeworkService homeworkService;
        private GradeService gradeService;

        public MyConsole()
        {
            this.studentService = new StudentService();
            this.homeworkService = new HomeworkService();
            this.gradeService = new GradeService();
        }

        private Student ReadStudent()
        {
            string name,group, email, teachersName;
            int id;

            Console.Write("Enter the id: ");
            id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the student's name: ");
            name = Console.ReadLine();
            Console.Write("Enter the group's number: ");
            group = Console.ReadLine();
            Console.Write("Enter the email: ");
            email = Console.ReadLine();
            Console.Write("Enter the teacher's name: ");
            teachersName = Console.ReadLine();

            return new Student
            {
                id = id,
                name = name,
                groupNumber = group,
                email = email,
                teacherName = teachersName,
                status=true
            };
        }

        public void UIAddStudent()
        {

            try
            {
                Student s = this.studentService.AddStudent(this.ReadStudent());

                if (s == null)
                {
                    Console.WriteLine("Successfully added the student!");
                }
                else
                {
                    Console.WriteLine("The student could not be added!");
                }

            }

            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

        }

        private void UIUpdateStudent()
        {

            try
            {
                Student s = this.studentService.UpdateStudent(this.ReadStudent());
                if (s == null)
                {
                    Console.WriteLine("Successfully modified the student!");
                }
                else
                {
                    Console.WriteLine("The student could not be modified!");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void UIDeleteStudent()
        {
            Console.Write("Enter the id: ");

            try
            {
                int id = Convert.ToInt32(Console.ReadLine());
                Student s = this.studentService.DeleteStudent(id);
                if (s == null)
                {
                    Console.WriteLine("Successfully deleted the student!");
                }
                else
                {
                    Console.WriteLine("The student could not be deleted!");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void UIViewAvailableStudents()
        {
            ((List<Student>)this.studentService.GetAvailableStudents()).ForEach(x => Console.WriteLine(x.ToString()));
        }

        private Homework ReadHomework()
        {
            string description;
            int number, deadlineWeek, receivedWeek;
            Console.Write("Enter the number: ");
            number = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the description: ");
            description = Console.ReadLine();
            Console.Write("Enter the received week: ");
            receivedWeek = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the deadline week: ");
            deadlineWeek = Convert.ToInt32(Console.ReadLine());

            return new Homework
            {
                number=number,
                description = description,
                deadlineWeek = deadlineWeek,
                receivedWeek = receivedWeek
            };
        }

        private void UIAddHomework()
        {
            try
            {
                Homework h = this.homeworkService.AddHomework(this.ReadHomework());
                if (h == null)
                {
                    Console.Write("Successfully added the homework!");
                }
                else
                {
                    Console.Write("The homework could not be added!");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void UIExtendDeadline()
        {
            int homeworkId, newDeadline;


            try
            {
                Console.Write("Enter homework's id: ");
                homeworkId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the new deadline: ");
                newDeadline = Convert.ToInt32(Console.ReadLine());

                Homework h = this.homeworkService.ExtendDeadline(homeworkId, newDeadline);
                if (h == null)
                {
                    Console.Write("Successfully extended the deadline!");
                }
                else
                {
                    Console.Write("The deadline could not be extended!");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void UIViewHomeworks()
        {
            ((List<Homework>)this.homeworkService.GetHomeworks()).ForEach(x => Console.WriteLine(x.ToString()));
        }

        private void UIAddGrade()
        {
            int studentId, homeworkId, week,noExemptions;
            double grade;
            string feedback;
            try
            {
                Console.Write("Enter the id of the student: ");
                studentId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the id of the homework: ");
                homeworkId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the week: ");
                week = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the number of exemptions: ");
                noExemptions = Convert.ToInt32(Console.ReadLine());
                Student student = studentService.FindStudent(studentId);
                Homework homework = homeworkService.FindHomework(homeworkId);
                if (student != null && homework != null)
                {
                    Console.WriteLine("The maximum grade is: " + gradeService.GetMaximumGradeForStudentOnHomeworkIntoWeek(student, homework, week, noExemptions));
                    Console.Write("Enter the grade: ");
                    grade = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Enter the feedback: ");
                    feedback = Console.ReadLine();
                    Grade gradeObj = this.gradeService.AddGrade(new Grade(studentId, homeworkId, grade, week, feedback), noExemptions);
                    if (gradeObj == null)
                    {
                        Console.Write("Successfully added the grade!");
                    }
                    else
                    {
                        Console.Write("Failed to add the grade!");
                    }
                }
                else Console.Write("The student or the homework doesn't exist!");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }


        }

        private void UIViewGrades()
        {
            ((List<Grade>)this.gradeService.GetGrades()).ForEach(x => Console.Write(x.ToString()+"\n"));
        }

        private void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("0.Exit");
            Console.WriteLine("1.Add Student.");
            Console.WriteLine("2.Delete Student.");
            Console.WriteLine("3.Update Student.");
            Console.WriteLine("4.All Students");
            Console.WriteLine("5.Add Homework");
            Console.WriteLine("6.Update Homework");
            Console.WriteLine("7.All homeworks");
            Console.WriteLine("8.Add Grade");
            Console.WriteLine("9.All Grades");
            Console.WriteLine("10.Homework Grades");
            Console.WriteLine("11.Homework Group Grades");
            Console.WriteLine("12.Current Week Grades");
            Console.WriteLine("13.Group Grades");
            Console.WriteLine("14.Final Grades");
            Console.WriteLine("15.Hardest Homeworks");
            Console.WriteLine("16.Passable Students");

        }

        public void Run()
        {
            Console.Write("Please enter the current week: ");
            MyData.CurrentWeek = Convert.ToInt32(Console.ReadLine());
            while (true)
            {
                this.PrintMenu();
                Console.Write("Please enter the command: ");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "0":
                        return;
                        break;
                    case "1":
                        this.UIAddStudent();
                        break;
                    case "2":
                        this.UIDeleteStudent();
                        break;
                    case "3":
                        this.UIUpdateStudent();
                        break;
                    case "4":
                        this.UIViewAvailableStudents();
                        break;
                    case "5":
                        this.UIAddHomework();
                        break;
                    case "6":
                        this.UIExtendDeadline();
                        break;
                    case "7":
                        this.UIViewHomeworks();
                        break;
                    case "8":
                        this.UIAddGrade();
                        break;
                    case "9":
                        this.UIViewGrades();
                        break;
                    case "10":
                        this.UIHomeworkGrades();
                        break;
                    case "11":
                        this.UIHomeworkGroupGrades();
                        break;
                    case "12":
                        this.UICurrentWeekGrades();
                        break;
                    case "13":
                        this.UIGroupGrades();
                        break;
                    case "14":
                        this.UIFinalGrade();
                        break;
                    case "15":
                        UIHardestHomeworks();
                        break;
                    case "16":
                        UIPassableStudents();
                        break;
                }

            }
        }

        private void UIFinalGrade()
        {
            gradeService.FinalGrades().ForEach(x => Console.WriteLine(x.studentName + ": " + x.grades[0].grade.ToString() + "\n"));
        }

        private void UIPassableStudents()
        {
            gradeService.PassedStudents().ForEach(x => Console.WriteLine(x.ToString()));
        }

        private void UIHardestHomeworks()
        {
            Console.WriteLine(gradeService.HardestHomework().ToString());
        }

        private void UIGroupGrades()
        {
            string group;
            try
            {
                Console.Write("Enter the group: ");
                group = Console.ReadLine();
                ((List<StudentGradeDTO>)gradeService.GroupGrades(group)).ForEach(x => Console.Write(x.ToString()));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void UICurrentWeekGrades()
        {
            try
            {
                (gradeService.CurrentWeekGrades()).ForEach(x => Console.Write(x.ToString()));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void UIHomeworkGroupGrades()
        {
            int homeworkId;
            string group;
            try
            {
                Console.Write("Enter the number of the homework: ");
                homeworkId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the group: ");
                group = Console.ReadLine();
                ((List<StudentGradeDTO>)gradeService.HomeworkGroupGrades(group,homeworkId)).ForEach(x => Console.Write(x.ToString()));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void UIHomeworkGrades()
        {
            int homeworkId;
            try
            {
                Console.Write("Enter the number of the homework: ");
                homeworkId = Convert.ToInt32(Console.ReadLine());
                ((List<double>)gradeService.HomeworkGrades(homeworkId)).ForEach(x => Console.Write(x.ToString()+"  "));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
