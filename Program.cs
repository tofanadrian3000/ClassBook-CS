using ConsoleApp1.Domain;
using ConsoleApp1.Service;
using ConsoleApp1.UI;
using ConsoleApp1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            MyConsole myConsole = new MyConsole();
            myConsole.Run();
        }
    }
}
