using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public interface HasID<ID>
    {
        ID id { get; set; }

        object GetID();
    }
}
