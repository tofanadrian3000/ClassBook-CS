using ConsoleApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Utils
{
    class HomeworkValidator : Validator<Homework>
    {
        public void validate(Homework entity)
        {
            string errors = "";

            if (entity.deadlineWeek < 1 || entity.deadlineWeek > 14)
            {
                errors += "The deadline should be between 1 and 14!";
            }

            if (entity.receivedWeek < 1 || entity.receivedWeek > 14)
            {
                errors += "The received week should be between 1 and 14!";
            }

            if (entity.description == "" || entity.description == null)
            {
                errors += "The description should not be empty!";
            }

            if (errors.Length != 0)
            {
                throw new ValidationException(errors);
            }
        }
    }
}
