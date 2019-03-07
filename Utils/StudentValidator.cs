using ConsoleApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Utils
{
    class StudentValidator : Validator<Student>
    {
        public void validate(Student entity)
        {
            var errors = "";

            if (entity.name.Length == 0 || entity.name == null)
            {
                errors += "The name can not be empty!";
            }

            if (entity.teacherName.Length == 0 || entity.teacherName == null)
            {
                errors += "The teacher's name can not be empty!";
            }

            if (entity.groupNumber.Length == 0)
            {
                errors += "The group's number can't be empty!";
            }

            if (entity.email.Length == 0)
            {
                errors += "The email can't be empty!";
            }
            if (errors.Length != 0)
            {
                throw new ValidationException(errors);
            }
        }
    }
}
