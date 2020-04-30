using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmRassh
{
    class Program
    {
        static void Main(string[] args)
        {
            Reflection reflection = new Reflection();
            reflection.a = "5";
            reflection.b = "6";
            Type type = typeof(Reflection);
            FieldInfo fieldInfo = type.GetField("a");
            Console.WriteLine(fieldInfo.GetValue(reflection));
        }
    }

    class Reflection
    {
        public string a;
        public string b;
    }
}
