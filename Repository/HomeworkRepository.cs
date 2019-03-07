using ConsoleApp1.Domain;
using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Repository
{
    class HomeworkRepository : AbstractFileRepo<int, Homework>
    {
        public HomeworkRepository(Validator<Homework> vali, string fileName) : base(vali, fileName, EntityToFileMapping.CreateHomework)
        {
        }
    }
}
