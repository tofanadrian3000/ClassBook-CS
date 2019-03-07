using ConsoleApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Utils
{
    class GradeValidator:Validator<Grade>
    {
        public void validate(Grade entity)
        {
            var errors = "";
            if (entity.feedback.Length == 0)
            {
                errors += "The feedback can not be empty!";
            }
            if (entity.grade < 0 || entity.grade > 10)
            {
                errors += "The grade must be between 0 and 10";
            }
            if (errors.Length != 0)
            {
                throw new ValidationException(errors);
            }
        }
    }
}
