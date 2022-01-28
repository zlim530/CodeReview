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
            // 【1】加载 DLL 文件
            //Assembly assembly = Assembly.Load("Reflection");
            // 方式一：Load():这个 DLL 文件要在当前启动项目下
            // 如果 DLL 没有在当前启动项目下，可以添加需要加载 DLL 文件所在项目的引用或者直接把 DLL 文件复制到当前项目 \bin\Debug\net5.0 文件夹下

            //Assembly assembly2 = Assembly.LoadFile(@"E:\localcode\CodeReview\C#\dotnet\net5.0\Reflection\Reflection\bin\Debug\net5.0\Reflection.dll");
            // 方式二：LoadFile():待加载 DLL 文件的完整路径

            //Assembly assembly3 = Assembly.LoadFrom(@"E:\localcode\CodeReview\C#\dotnet\net5.0\Reflection\Reflection\bin\Debug\net5.0\Reflection.dll");
            // 方式三：LoadFrom():可以放完整路径
            // 也可以放 DLL 的名称：需带文件类型后缀
            Assembly assembly4 = Assembly.LoadFrom("Reflection.dll");// 推荐使用这种方法加载 DLL 文件

            // 【2】获取指定类型
            Type type = assembly4.GetType("Reflection.ReflectionTest");

            // 【3】实例化
            //ReflectionTest reflectionTest = new ReflectionTest();// 这种实例化是知道具体类型--静态
            object objectTest1 = Activator.CreateInstance(type);// 动态实例化--调用无参构造方法
            object objectTest2 = Activator.CreateInstance(type, new object[] { "zLim530"});// 动态实例化--调用有参构造方法

            Console.ReadLine();
        }
    }
}
