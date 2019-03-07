using ConsoleApp1.Domain;
using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Repository
{
    public delegate E CreateEntity<E>(string line);

    abstract class AbstractFileRepo<ID, E> : Repository<ID, E> where E : HasID<ID>
    {
        protected string fileName;
        protected CreateEntity<E> createEntity;

        public AbstractFileRepo(Validator<E> vali, String fileName, CreateEntity<E> createEntity) : base(vali)
        {
            this.fileName = fileName;
            this.createEntity = createEntity;
            if (createEntity != null)
                loadFromFile();
        }

        protected virtual void loadFromFile()
        {
            List<E> list = DataReader.ReadData(fileName, createEntity);
            list.ForEach(x => entities.Add(x));
        }

        protected virtual void WriteAll()
        {
            DataReader.WriteData<E,ID>(fileName, entities);
        }

        override public E Save(E entity)
        {
            E e = base.Save(entity);
            if (e == null)
                WriteAll();
            return e;
        }

        override public E Delete(ID id)
        {
            E e = base.Delete(id);
            if (e != null)
                WriteAll();
            return e;
        }

        override public E Update(E entity)
        {
            E e = base.Update(entity);
            if (e == null)
                WriteAll();
            return e;
        }

    }
}
