using System;
//使用using关键字 引用System命名空间 因为我们需要用到System命名空间中的Console类中定义的方法
//命名空间的声明 一个命名空间可以包含多个类
using Tools;
//我们引用了我们自定义的类库supercalculator 并引用了类库中的名叫Tools的名称空间
//在我们自定义的类库SuperCalulator中包含了一个名叫Tool的名称空间 
//在Tool的名称空间中又包含一个名叫Calculator的类 在这个类中定义了四个静态方法 Add Sub Mul Div
//因为这四个方法均为静态方法 所以需要用“类名.方法名()”的形式来调用它

namespace HellowordlApplication
{
    //类的声明 一个类中可以包含多个方法 这里 HelloWorld类只有一个Main方法
    class HelloWorld
    {
        //定义Main方法 同时Main方法也是所有C#程序的入口点 
        static void Main(string[] args)
        {
            //WriteLine是一个定义在System命名空间中的Console类的一个方法
            Console.WriteLine("Hello World!");
            //Console.ReadKey();
            //专门针对VS.NET用户 使程序会等待一个按键的动作 
            //防止程序从VS.NET启动时屏幕快速关闭而不能看到我们所期望的效果
            Console.WriteLine(Calculator.Mul(3.6,5.3));
            Console.WriteLine(Calculator.Div(6.33,0));
        }
    }
}
/*
    总结：
        C#是大小敏感的
        所有的语句与表达式都必须以;（分号）结尾!!!!
        所有C#程序的执行是从Main方法开始的
     
     */