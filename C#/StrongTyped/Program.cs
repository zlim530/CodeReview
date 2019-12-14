using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StrongTyped
{
    class Program
    {
        // 反射示例
        static void Main(string[] args)
        {
            Type myType = typeof(Form);
            Console.WriteLine(myType.Name);
            Console.WriteLine(myType.FullName);
            Console.WriteLine(myType.BaseType);
            Console.WriteLine(myType.UnderlyingSystemType);
            Console.WriteLine(myType.IsClass);
            PropertyInfo[] pinfos = myType.GetProperties();
            MethodInfo[] mInfos = myType.GetMethods();
            //foreach (var p in pinfos)
            //{
            //    Console.WriteLine(p.Name);
            //}

            //foreach (var m in mInfos)
            //{
            //    Console.WriteLine(m.Name);
            //}

            int[] array = new int[100];
            Console.WriteLine(array[99]);


        }
    }
}
