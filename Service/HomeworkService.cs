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
    class HomeworkService
    {
        private CrudRepository<int, Homework> homeworkRepository;

        public HomeworkService()
        {
            string fileName = "..\\..\\Data\\homeworks.txt";
            this.homeworkRepository = new HomeworkRepository(new HomeworkValidator(),fileName);
        }

        public Homework AddHomework(Homework homework)
        {
            Homework newHomework = this.homeworkRepository.Save(homework);
            return newHomework;
        }

        public Homework ExtendDeadline(int homeworkNumber, int newDeadline)
        {
            int currentWeek = MyData.CurrentWeek;
            Homework foundHomework = this.homeworkRepository.FindOne(homeworkNumber);
            if (foundHomework == null)
            {
                throw new Exception("the homework does not exist!");
            }
            else
            {
                if (currentWeek <= newDeadline && newDeadline>foundHomework.deadlineWeek)
                {
                    foundHomework.deadlineWeek = newDeadline;
                    return this.homeworkRepository.Update(foundHomework);
                }
                else
                {
                    throw new Exception("the new deadline must be greater than the current week");
                }
            }
        }

        public Homework FindHomework(int id)
        {
            return this.homeworkRepository.FindOne(id);
        }

        public IList<Homework> GetHomeworks()
        {
            return this.homeworkRepository.FindAll();
        }
    }
}
