using System;

/**
 * @author zlim
 * @create 2020/5/13 17:31:35
 */



/*
什么是异常？
    程序运行时发生的错误。（错误的出现并不总是程序员的原因，有时应用程序会因为最终用户或运行代码的环境改变而发生错误。比如：1.连接数据库时数据库
    服务停电了；2.操作文件时文件没了、权限不足等；3.计算器用户输入的被除数为0；4.使用对象时对象为 null等等）
    .NET 为我们把“发现错误(try)”的代码与“处理错误(catch)”的代码分离开来
异常处理一般代码模式：
    try{
        // 可能发生异常的代码
    } catch {
        // 对异常的处理
    } finally {
        // 无论发生异常、是否捕获异常都会执行的代码
    }
try 块：可能出现问题的代码。当遇到异常时，后续代码不会执行
catch 块：对异常的处理，记录日志（log4NET），继续向上抛出等操作（只有发生了异常才会执行）
finally 块：代码清理、资源释放等。无论是否发生异常都会执行
*/
namespace 异常处理 {
    public class Program {
        static void Main0(string[] args) {
            string s = "Hello,World!";
            s = null;
            try {
                // 当 try 块中某行代码发生异常后，从该行代码开始后面的代码都不会继续执行了
                // 程序会直接跳转到 catch 块中执行
                //Console.WriteLine(s.Length);
                //Console.WriteLine(s.ToString());
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            // ================catch 块的几种写法 ================
            try {
                int n = 10, m = 0;
                int r = n / m;
                Console.WriteLine(r);
            } /*catch  { // 第一种写法：可以捕获 try 块中的所有异常
                Console.WriteLine("发生异常了");
            }*/ 
            /*catch (Exception e) { // 第二种写法：可以捕获 try 块中的所有异常
                Console.WriteLine("发生异常了");
                Console.WriteLine(e.Message);// Attempted to divide by zero.
                Console.WriteLine(e.Source);// CSharpSenior
                Console.WriteLine(e.StackTrace);
                // at 异常处理.Program.Main(String[] args) in C:\Users\Lim\Desktop\code\CodeReview\C#\CSharpSenior\ITCastDotNet003.cs:line 44
            }*/ 
            // 对不同的异常，使用不同的方法是来处理（也即使用多个不同的 catch 块来捕获异常）
            catch (NullReferenceException e) {
                Console.WriteLine($"空指针异常：{e.Message}");
            } catch (DivideByZeroException e) {
                Console.WriteLine($"除数为0异常：{e.Message}");
            } catch (ArgumentException e) {
                Console.WriteLine($"参数异常：{e.Message}");
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }

        }

        static void Main1(string[] args) {
            try {
                Console.WriteLine("11111");
                int n = 10, m = 0;
                int r = n / m;
                Console.WriteLine(r);
                Console.WriteLine("222222");
            } catch (Exception) {
                Console.WriteLine("3333333");
            } finally {
                /*
                如果希望代码无论如何都要被执行，则一定要将代码放在 finally 块中
                1.当 catch 块中有无法捕获到的异常时，程序崩溃，但是程序在崩溃之前还是会执行 finally 块中的代码，
                  而 finally 块后面的代码则由于程序崩溃将无法执行
                2.如果在 catch 块中又引发了异常，则 finally 块中的代码也会在继承引发异常之前执行，但是 finally 
                  块后面的代码则不会执行
                3.当 catch 块中有 return 语句时，finally 块中的代码也会在 return 语句之前执行，但是 finally 块
                  后面的代码就不会执行了
                */
                Console.WriteLine("4444444");
            }
            Console.WriteLine("$$$$$$");

        }

        static void Main2(string[] args) {
            while (true) {
                try {
                    Console.WriteLine("pls input a name:");
                    string name = Console.ReadLine();
                    if (name == "Tim") {
                        // 手动抛出异常
                        // 尽量使用逻辑判断来避免异常处理代码，即尽量不要手动抛出异常，因为异常比较消耗资源
                        throw new Exception("name is illeage!");
                    } else {
                        Console.WriteLine($"name is lleage:{name}");
                    }
                } catch (Exception e) {
                    Console.WriteLine("Happen Exception");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                } 
            }

        }

        static void Main3(string[] args) {
            try {
                M2();
            } catch (Exception) {
                // 此时在 Main 方法中处理异常，就不能再向上抛出，因为 Main 方法就是顶点了
                Console.WriteLine("M2 的在 Main 方法中调用发生了异常");
                //throw; // 此时再使用 throw 语句程序会崩溃
            }
        }
        static void M2() {
            Console.WriteLine("=======");
            Console.WriteLine("=======");
            try {
                M1();
            } catch (Exception) {
                Console.WriteLine("M1 的在 M2 方法中调用发生了异常");
                // M2 处理完异常，继续向上抛出，也即抛给了调用 M2 方法的方法，也即 Main 方法
                throw;
            }
            Console.WriteLine("=======");
            Console.WriteLine("=======");
        }

        static void M1() {
            try {
                int n = 10, m = 0;
                int r = n / m;
                Console.WriteLine($"r = {r}");
            } catch (Exception) {
                Console.WriteLine("M1 方法执行发生了异常");
                // 这种写法只能在 catch 块中写
                // 在 catch 块中使用 throw 语句，并且这种写法只能用在 catch 块中
                // 表示将当前异常继续向上抛出，在这里就是抛给 M2 方法
                throw;
            }
        }

        static void Main4(string[] args) {
            int n = M3();
            Console.WriteLine(n);// 101

            int n2 = M4();
            Console.WriteLine(n2);// 102
        }

        static int M4() {
            int result = 100;
            try {
                result = result + 1;// 101
                int x = 10, y = 0;
                Console.WriteLine(x / y);// 发生异常，try 块后续的代码将不会执行，而是执行 catch 块中的代码和 finally 块中的代码
                return result;
            } catch (Exception) {
                Console.WriteLine("catch 块被执行了。。。");
                result = result + 1;
                return result;
            } finally {
                Console.WriteLine("finally 块被执行了。。。");
                result = result + 1;// finally 中的代码一定是会执行的，但是最后 M1 方法的返回值仍然是101 
            }
        }

        static int M3() {
            int result = 100;
            try {
                result = result + 1;// 101
                return result;
            } catch (Exception) {
                result = result + 1;
                return result;
            } finally {
                result = result + 1;// finally 中的代码一定是会执行的，但是最后 M1 方法的返回值仍然是101 
            }
        }
        // 因为.NET 编译器在编译 C# 代码时，生成的中间语言 IL 在方法被调用时会为方法的参数和返回值分别创建变量
        // 也即就算我写一个方法 这个方法体就一条 return 语句：return 1000；
        // 我没有在方法内存生成任何变量，但在编译之后生成的 IL 语句中就会有一个变量，那个变量就是这个方法的返回值
        // 这里也是的，编译器为 M3 方法的返回值创建了一个变量，并在执行 try 块 return result; 语句时将这个变量赋值为
        // result + 1; 而后续 finally 块中的代码也得到了执行，只不过它这里是对 result + 1，而不是对那个返回值变量
        // 进行了 + 1,编译后的 IL 源码如下所示
        /*
        private static int M3(){
            int CS$1$0000;
            int result = 100;
            try {
                result++;
                CS$1$0000 = result;
            } catch (Exception) {
                result++;
                CS$1$0000 = result;
            } finally {
                result ++;
            }   
            return CS$1$0000;
        }

        */

        static void Main5(string[] args) {
            Person p = GetPerson();
            Console.WriteLine(p.Age);// 102 :没有引发异常时输出为 102，引发异常时输出为 103
        }

        static Person GetPerson() {
            Person p = new Person();
            p.Age = 100;
            try {
                p.Age = p.Age + 1;
                // =======引发异常代码=======
                int x = 10, y = 0;
                Console.WriteLine(x / y);
                // =======引发异常代码=======
                return p;
            } catch (Exception) {
                p.Age = p.Age + 1;
                return p;
            } finally {
                p.Age = p.Age + 1;
            }
        }
        /*
        编译之后的源码为：
        private static Person GetPerson(){
            Person CS$1$0000;
            Person p = new Person{
                Age = 100;
            };
            try {
                p.Age ++;
                int x = 10;
                int y = 0;
                Console.WriteLine((int)(x / y));
                CS$1$0000 =  p;// 引用变量赋值，就表示 CS$1$0000 和 p 都指向了堆内存中的同一个 Person 对象
            } catch (Exception) {
                p.Age ++;
                CS$1$0000 =  p;
            } finally {
                p.Age ++;// 因此在这里通过 p 去修改 Person 对象 Age 属性的值，再通过 CS$1$0000 去访问同一个对象的
                         // Age 属性值，自然而然也会修改· 
            }
            return CS$1$0000;
        }
        */

    }

    public class Person {

        public int Age {
            get { return _Age; }
            set { _Age = value; }
        }
        private int _Age;

    }
}


/*
类型转换 Cast 是在内存级别上的转换，内存中的数据没有变化，只是观看的视角不同而已。
什么情况下会发生隐式类型转换？
    1.把子类类型赋值给父类类型的时候会发生隐式类型转换
    2.把占用字节数小的数据类型赋值给占用字节数大的数据类型可以发生隐式类型转换（前提是这两种数据库类型在内存的同一个区域：例如均在栈内存中）
Math.Round(); 四舍五入
Convert.ToInt32(); 四舍五入
*/
namespace 显式类型转换 {
    public class ITCastDotNet003 {
        static void Main0(string[] args) {
            Console.WriteLine(sizeof(bool));// 1 
            Console.WriteLine(sizeof(byte));// 1
            Console.WriteLine(sizeof(char));// 2
            Console.WriteLine(sizeof(short));// 2
            Console.WriteLine(sizeof(int));// 4
            Console.WriteLine(sizeof(long));// 8
            Console.WriteLine(sizeof(float));// 4
            Console.WriteLine(sizeof(double));// 8
            Console.WriteLine(sizeof(decimal));// 16
        }
    }
}


