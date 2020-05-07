using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpSenior {
    
    public class _009_CSharp常见类库介绍之Console与Math类 {
        /*
        1. System 命名空间
        System空间，是C#的基础命名空间，里面定义了常用值和数据类型以及各种类型的基类，当然也包括了很多C#程序运行中用到类，具体可以访问微软的官方API说明。
        这里简单介绍一下 我们在开发中最常用到的几个类。

        1.1 Console
        Console 控制台类，表示一个控制台应用程序的标准输入流、输出流和错误流。这是微软官方文档给的内容。
        实际上，Console类在一些其他类型的项目中也可以使用。因为Console类是程序与终端的交互，所以当程序持有一个终端的时候，该类就可以正确输出内容。

        照例，我们先看一下它的声明：public static class Console 。可知这是一个静态类，需要明确一个概念：
            ·在 C# 甚至大多数编程语言（支持静态类）中，静态类不可以被继承，而且静态类的方法都是工具方法；
            ·静态类没有构造方法，也不能构造对象；
            ·静态类里的方法都是静态方法
            ·访问一个类的静态方法需要通过类名.方法名来访问
        */
        static void Main0(string[] args) {

            Console.Write("打印测试...");
            /*
            C# 关于控制台的输出还有一个方法：WriteLine，从名字来看是写一行的意思，实际表现也是如此，该方法每次输出都会产生新的一行内容，
            而Write只会在上一次输出的结尾继续输出
            */

            Console.Write("Write 输出测试");
            // 与Write不同的地方是，WriteLine允许无参调用，表示输出一个空行。
            Console.WriteLine();
            Console.WriteLine("这行是调用 WriteLine");
            Console.WriteLine("这行也是调用 WriteLine 输出的");

            /*
            获取用户输入：
                public static int Read ();
                public static string ReadLine ();
            Console在读这方面就没有写那么花里胡哨了，只有两个是我们常用的读。
            第一个是，从输入流中读取一个字符，如果没有输入则返回-1；第二个是读取输入的一行字符。
            第二个，就很有意思了，获取输入的一行内容，而不是一个字符，也就是说当用户决定这行内容输入完成点击换行后程序就能读取到输入的结果。
            */
            Console.WriteLine("Read 测试");
            Console.WriteLine("请输入一个任意内容并按回车：");
            var key = Console.Read();
            Console.WriteLine($"输入的是：{key}");
            Console.WriteLine();
            Console.ReadLine();
            key = Console.Read();
            Console.WriteLine($"输入的是：{key}");
            Console.ReadLine();
            Console.WriteLine("ReadLine 测试");
            Console.WriteLine("请输入任意内容，并换行：");
            var line = Console.ReadLine();
            Console.WriteLine($"输入的是：{line}");
            Console.WriteLine("示例结束");
            /*
            示例中，我在每次调用Read前，都调用了一个ReadLine，这是因为在控制台中一次输入字符，然后按下回车并换行，这是有两个输入，
            所以在第二次Read时会将上次未读取的继续读取出来，所以我利用ReadLine的特性将未读取的内容一次性读取出来，
            保证下次调用都必须从控制台读取用户输入。
            */

        }

        /*
        1.2 Math
        C#中的数学工具类，为三角函数、对数函数和其他通用数学函数提供常数和静态方法。这个类也是一个静态类，当然这不会影响我们对它的好奇。

            public static T Ceiling (<T> d); //T 代表 decimal、double，返回大于或等于指定数字的最小整数值。
            public static T Floor (<T> d); //T 代表 decimal、double，返回小于或等于指定双精度浮点数的最大整数值。
            public static T Truncate (<T> d);//T 代表 decimal、double，计算一个数字的整数部分。
        虽然这三个方法计算的结果都是整数，但返回类型并不是整数，所以在使用的时候需要我们进行一次类型转换。Math类还有两个值得注意的字段：
            public const double E = 2.7182818284590451;// 表示自然对数的底，它由常数 e 指定。
            public const double PI = 3.1415926535897931;// 表示圆的周长与其直径的比值，由常数 π 指定。
        这两个也是Math里唯一的两个字段，这是数学中著名的两个无理数，这里只截取了一部分有效值。
        */

        /*
        1.3 Random
        C# 中Random表示伪随机数生成器，这是一种能够产生满足某些随机性统计要求的数字序列的算法。这里大概讲解一下Random的使用，具体的原理等我研究一下哈。

        Random是一个类，所以与之前的两个类不同地方就是使用Random生成随机数需要事先构造一个Random对象。Random常用的方法有以下几组：
            public virtual int Next ();// 返回一个数值小于整型最大值的随机数
            public virtual int Next (int maxValue);//返回一个小于所指定最大值的非负随机整数
            public virtual int Next (int minValue, int maxValue);//返回在指定范围内的任意整数。
            public virtual double NextDouble ();//返回一个大于或等于 0.0 且小于 1.0 的随机浮点数。
        */
        static void Main(string[] args) {
            Random rand = new Random();
            Console.WriteLine(int.MaxValue);
            for (int i = 0; i < 10; i++) {
                Console.WriteLine($"第{i + 1}次生成：{rand.Next()}");
            }
            /*
            可以看出直接Next返回的结果数字都比较大，所以在使用的时候，一般会使用Next (int minValue, int maxValue) 限定返回值。

            回到开头，Random是一个类，每次初始化的时候系统会自动计算一个种子给它，如果快速重复构建Random对象，可能会生成一个重复序列，
            即每次调用的结果一致。
            */
            for (int i = 0; i < 5; i++) {
                Random random = new Random();
                for (int j = 0; j < 10; j++) {
                    Console.WriteLine($"第{i}个 Random 第{j}次生成：{random.Next(1, 100)}");
                }
            }
            Console.ReadLine();
        }

    }
}
