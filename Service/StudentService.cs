using ConsoleApp1.Domain;
using ConsoleApp1.Repository;
using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Configuration;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ConsoleApp1.Service
{
    class StudentService
    {
        private CrudRepository<int, Student> studentRepository;

        public StudentService()
        {
            string fileName = "..\\..\\Data\\students.txt";
            this.studentRepository = new StudentRepository(new StudentValidator(),fileName);
        }

        public Student AddStudent(Student student)
        {
            return this.studentRepository.Save(student);
        }

        public Student DeleteStudent(int id)
        {
            Student oldStudent = this.studentRepository.FindOne(id);
            if (oldStudent == null)
            {
                throw new Exception("the student does not exist!");
            }
            else
            {
                oldStudent.status = false;
                return this.studentRepository.Update(oldStudent);
            }

        }

        public Student UpdateStudent(Student student)
        {
            Student s = this.studentRepository.Update(student);
            return s;
        }

        public IList<Student> GetStudents()
        {
            return this.studentRepository.FindAll();
        }

        public Student FindStudent(int id)
        {
            return this.studentRepository.FindOne(id);
        }

        public IList<Student> GetAvailableStudents()
        {
            IList<Student> students = this.studentRepository.FindAll();
            IList<Student> availableStudents = new List<Student>();
            availableStudents = students.Where(x => x.status == true).ToList();
            return availableStudents;
        }
    }
}
