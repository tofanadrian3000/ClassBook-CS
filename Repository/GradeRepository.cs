using ConsoleApp1.Domain;
using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Repository
{
    class GradeRepository : AbstractFileRepo<GradeId, Grade>
    {
        public GradeRepository(Validator<Grade> vali, string fileName) : base(vali, fileName, EntityToFileMapping.CreateGrade)
        {
        }
    }
}
