using ConsoleApp1.Domain;
using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Repository
{
    public class Repository<ID, E> : CrudRepository<ID, E> where E : HasID<ID>
    {
        protected IList<E> entities;
        private Validator<E> validator;

        public Repository(Validator<E> validator)
        {
            this.entities = new List<E>();
            this.validator = validator;
        }

        virtual public E Delete(ID id)
        {
            if (id == null)
            {
                throw new Exception("invalid parameter");
            }
            E e = ((List<E>)entities).Find(x => x.id.Equals(id));
            if (e == null)
            {
                return default(E);
            }
            else
            {
                this.entities.Remove(e);
                return e;
            }

        }

        public IList<E> FindAll()
        {
            return this.entities;
        }

        public E FindOne(ID id)
        {
            if (id == null)
            {
                throw new Exception("invalid parameter");
            }
            E e = ((List<E>)entities).Find(x => x.id.Equals(id));
            return e;
        }

        virtual public E Save(E entity)
        {
            if (entity == null)
            {
                throw new Exception("invalid parameter");
            }
            this.validator.validate(entity);

            E e = ((List<E>)entities).Find(x => x.id.Equals(entity.GetID()));
            if (e == null)
            {
                this.entities.Add(entity);
                return default(E);
            }
            else
            {
                return entity;
            }
        }

        virtual public E Update(E entity)
        {
            if (entity == null)
            {
                throw new Exception("invalid parameter");
            }
            this.validator.validate(entity);
            E e = ((List<E>)entities).Find(x => x.GetID().Equals(entity.GetID()));
            if (e != null)
            {
                int index = this.entities.IndexOf(entity);
                this.entities[index] = entity;
                return default(E);
            }
            else
            {
                return entity;
            }
        }
    }
}
