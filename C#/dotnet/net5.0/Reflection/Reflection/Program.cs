using System;
using System.Reflection;

namespace Reflection
{
    /// <summary>
    /// 反射：就是操作 DLL 文件的一个类库
    /// 怎么使用：1--查找 DLL 文件; 2--通过 Reflection 反射类库里的各种方法来操作 DLL 文件
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Assembly assembly = Assembly.Load("Reflection");// 方式一：这个 DLL 文件要在当前启动项目下
            // 如果当前项目不是可启动项目，如类库项目，则可以添加需要加载 DLL 文件所在项目的引用或者直接把 DLL 文件复制到当前项目 \bin\Debug\net5.0 下
        }
    }
}
