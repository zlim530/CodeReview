using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Loader;
using BabyStroller.SDK;
using System.Linq;

namespace BabyStroller.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // 显示当前项目生成的可执行文件的路径
            // Console.WriteLine(Environment.CurrentDirectory);
            var folder = Path.Combine(Environment.CurrentDirectory,"Animals");
            var files = Directory.GetFiles(folder);
            var animalTypes = new List<Type>();
            foreach (var file in files)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
                var types = assembly.GetTypes();
                foreach (var t in types)
                {
                    //if (t.GetMethod("Voice") != null)
                    //{
                    //    animalTypes.Add(t);
                    //}
                    // 第一次过滤掉那些没有实现IAnimal接口的类
                    if ( t.GetInterfaces().Contains(typeof(IAnimal)))
                    {
                        // 第二次过滤掉那些实现了IAnimal接口但是又被Unfinished修饰的类
                        var isUnfinished = t.GetCustomAttributes(false).Any(a => a.GetType() == typeof(UnfinishedAttribute));
                        if ( isUnfinished)
                        {
                            continue;
                        }
                        animalTypes.Add(t);
                    }
                }
            }

            while (true)
            {
                for (int i = 0; i < animalTypes.Count; i++)
                {
                    Console.WriteLine($"{i+1}.{animalTypes[i].Name}");
                }
                Console.WriteLine("===================");
                Console.WriteLine("Please choose animal:");
                int index = int.Parse(Console.ReadLine());
                if ( index > animalTypes.Count || index < 1)
                {
                    Console.WriteLine("No such an animal. Try again!");
                    continue;
                }

                Console.WriteLine("How many times?");
                int times = int.Parse(Console.ReadLine());
                var t = animalTypes[index-1];
                //var m = t.GetMethod("Voice");
                var o = Activator.CreateInstance(t);
                var a = o as IAnimal;
                // 使用接口开发后，可以直接调用Voice方法
                a.Voice(times);
                //m.Invoke(o,new object[] { times});
            }


        }
    }
}
