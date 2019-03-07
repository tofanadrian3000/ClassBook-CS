using ConsoleApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Repository
{
    public interface CrudRepository<ID, E> where E : HasID<ID>
    {
        IList<E> FindAll();

        E FindOne(ID id);

        E Save(E entity);

        E Delete(ID id);

        E Update(E entity);
    }
}
