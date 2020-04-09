using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpSenior {
    class _001_属性详解 {
        static void Main(string[] args) {
            var person = new Person();
            // 外部访问属性
            // 为属性赋值
            //person.Name = "Bob";
            //person.Age = 19;

            // 读取属性的值
            int age = person.Age;
            Console.WriteLine(age);
            Console.WriteLine(person.Name);
        }

        /*
         可以给访问器设置可访问性。
         例如把set访问器设为private，不允许外部直接赋值。通常是限制set访问器的可访问性。
         可以省略掉某个访问器。通常是省略掉set访问器可使属性为只读。
         另外，属性的本质是方法，所以接口中可以包含属性。
        */
        class Person {
            private readonly string _name;
            private int _age;

            /*
             属性访问器的表达式体形式(C#7)
             在 C# 7.0 中，对于老式的读/写属性，我们可以把get访问器或set访问器改写为表达式体(lambda)。

             注意：要求访问器的方法体能够改写为lambda表达式（必须是单行代码）
            */
            // C# 5:
            //public int Age {
            //    get {
            //        return _age;
            //    }
            //    set {
            //        _age = value;
            //    }
            //}

            // C# 7:
            //全部改写为Lambda表达式体
            //public int Age { get => _age; set => _age = value; }
            //只改写set访问器
            //public int Age { get { return _age; } set => _age = value; }
            //只改写get访问器
            public int Age { get => _age; set { _age = value; } }

            /*
             只读属性(使用readonly关键字修饰)的表达式体形式有2个限制：

             只包含 get访问器
             要求 get访问器的方法体能够改写为lambda表达式（必须是单行代码）
             */
            //C# 5
            //public string Name {
            //    get {
            //        return _name;
            //    }
            //}

            // C# 6
            public string Name => _name;

            // 属性初始化器
            // 在 C# 6 和更高版本中，你可以像字段一样初始化自动实现属性
            //public string Name { get; set; } = "Hello,World";
            //public int Age { get; set; } = 18;

            // ========================================================================

            /*
                自动实现的属性(新)
                当属性访问器中不需要任何其他逻辑时，我们可以使用自动实现的属性，它会使属性声明更加简洁。

                自动实现的属性在编译后，也是生成了老式的读/写属性。

                VS中使用快捷键prop可以快速生成自动实现属性
            */
            //public string Name { get; set; }

            //public int Age { get; set; }

            // ========================================================================

            // 这种方式是C#中最基础的，也是最早出现的读写属性的方式。称它为老式的读/写属性
            // Declare a Name property of type string:
            //public string Name {
            //    get {
            //        return _name;
            //    }
            //    set {
            //        _name = value;
            //    }
            //}

            //public int Age {
            //    get {
            //        return _age;
            //    }
            //    set {
            //        _age = value;
            //    }
            //}
        }
    }
}
