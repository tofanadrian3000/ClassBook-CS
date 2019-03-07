using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Domain;

namespace ConsoleApp1.Repository
{
    class DataReader
    {
        public static List<T> ReadData<T>(string fileName, CreateEntity<T> createEntity)
        {
            List<T> list = new List<T>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    T entity = createEntity(s);
                    list.Add(entity);
                }
            }
            return list;
        }

        public static void WriteData<E, ID>(string fileName, IList<E> entities) where E : HasID<ID>
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                ((List<E>)entities).ForEach(x => sw.Write(x.ToString()+"\n"));
            }
        }
    }
}
