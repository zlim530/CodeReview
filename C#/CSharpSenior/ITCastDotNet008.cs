using System;
using System.Reflection;

/**
 * @author zlim
 * @create 2020/7/14 20:14:20
 */

/*
 * 程序集：
 * ·程序集是.NET 中的概念；
 * .NET 中的 dll 与 exe 文件都是程序集
 * 程序集：Assembly 可以看做是一推相关类的一个包，相当于 java 中的 jar 包
 * 程序集中包含：类型元数据（描述在代码中定义的每一类型和成员二进制形式）、程序集元数据（程序集清单、版本号、名称等）、IL 代码（在 exe 或者 dll 文件中）、资源文件。每个程序集都有自己的名称、版本等信息。这些信息可以通过 AssemblyInfo.cs 文件来自己定义
 * 使用程序集的好处：
 * ·程序中只引用必须的程序集，减小程序的尺寸
 * ·程序集可以锋装一些代码，只提供必要的访问接口
 * 如何添加程序集的引用：
 * ·添加路劲、项目引用、GAC（全局程序集缓存）
 * ·不能循环添加引用
 * ·在 C# 中添加其他其他语言编写的 dll 文件的引用：extern（参考 P/Inovke 在.NET 中调用非程序集的 dll）
 */

/*
 *反射：
 * ·反射无处不在，VS 的智能提示，就是通过反射获取到类的属性、方法等。还有反编译工具也是通过反射实现
 * ·反射就是动态获取程序集的元数据（提供程序集的类型信息）的功能，通过动态获取程序集中的元数据来操作类型
 * ·Type 类是实现反射的一个重要的类，通过它我们可以获取类中的所有信息包括方法、属性等，并且可以动态的调用类的属性、方法
 * ·Type 是对类的描述。
 * ·反射就是直接通过 dll 来创建对象，调用成员
 * ·通过类型元数据来获取对象的一些相关信息，并且还可以实例化对象调用其方法等
 * ·反射让创建对象的方式发生了改变
 */

 /*
  Type 类的使用：
  ·通过类获取 Type：Type t = typeof(Person);
  ·通过对象获取类的 Type：Type t = p.GetType();
  ·通过 Assembly 对象获取：Assembly asm = Assembly.LoadFile("c:\abc.dll");
  ·调用 Assembly 的 GetExportedTypes 方法可以得到 Assembly 中定义的所有的 public 类型
  ·调用 Assembly 的 GetTypes 方法可以得到 Assembly 中定义的所有的类型
  ·调用 Assembly 的 GetType(string
   name) 方法可以得到 Assembly 中定义的全名为 name 的类型信息
   动态创建对象：
   ·Activator.CreateInstance(Type t) 会动态调用类的无参构造函数创建一个对象，返回值就是创建的对象，如果类没有无参构造函数就会报错
   ·GetConstructor(参数列表);// 这个是找到带参数的构造函数
   Type 类的方法：在编写调用插件的程序时，需要做一系列的验证
    ·bool IsAssignableFrom(Type e)：是否可以从 c 赋值，判断当前类型的变量是否可以接受 c 类型变量的赋值
    ·typeof(IPlugin).IsAssignableFrom(t)
    ·bool IsInstanceOfType(object o)：判断对象 o 是否是当前类的实例（当前类可以是 o 的类、父类、接口）
    ·bool IsSubclassOf(Type c)：判断当前类是否是类 c 的子类
    ·bool IsAbstract()：判断是否为抽象的，含接口
 */
namespace 程序集_反射介绍 {
    public class ITCastDotNet008 {
        /// <summary>
        /// Type 类型的简单介绍
        /// </summary>
        /// <param name="args"></param>
        public static void Main0(string[] args) {
            MyClass m = new MyClass();
            // 如何通过一个类来获取一个类型 Type（该类型的类型元数据）
            Type type = m.GetType();// 获取了类型 MyClass 对应的 Type
            // 通过 typeof 关键字，不需要获取 MyClass 类型的对象就可以获得 MyClass 的 Type 对象
            Type type2 = typeof(MyClass);

            // 拿到 Type 后可以：
            // 1.获取当前类型的父类
            Console.WriteLine(type.BaseType.ToString());
            // System.Object
            // Console.WriteLine(type.BaseType.BaseType?.ToString());
            // Console.WriteLine(type.BaseType.BaseType?.BaseType?.ToString());
            
            // 2.获取当前类型中的所有字段信息
            // 当前方法只能获取非私有字段，如果想获取私有字段需要采用其他方法
            FieldInfo[] fields = type.GetFields();
            for (int i = 0; i < fields.Length; i++) {
                Console.WriteLine(fields[i].Name);
            }

            PropertyInfo[] infos = type.GetProperties();
            for (int i = 0; i < infos.Length; i++) {
                Console.WriteLine(infos[i].Name);
            }

        }

        /// <summary>
        /// 反射的部分方法调用
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) {
            #region 动态加载程序集并且调用类型的成员

            // 动态加载程序集
            // 根据程序的路径，动态加载一个程序集
            Assembly assembly = Assembly.Load(@"C:\Users\Lim\Desktop\code\CodeReview\C#\ReflectTest\bin\Debug\ReflectTest.dll");

            // 1.获取该程序集中的所有类型
            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++) {
                Console.WriteLine(types[i].FullName);
            }

            // 1.2 获取所有的 public 的类型
            Type[] types = assembly.GetExportedTypes();
            for( int i = 0;i < types.Length;i ++){
                Console.WriteLine(types[i].FullName);
            }

            // 1.3 获取指定的类型
            Type typePerson = assembly.GetType("_07TestDll.Person");
            // 2.获取某个类型中的成员：
            // 2.1 获取 SayHi 方法
            MethodInfo method = typePerson.GetMethod("SayHi");
            // 创建一个 Person 类型，根据指定的 Type 创建一个该类型的对象
            object obj = Activator.CreateInstance(typePerson);
            // 通过该类型的对象调用其方法
            method.Inovke(obj,null);

            // ================调用 SayHello 的有参数重载
            // 调用重载就是通过第二个参数 Type[] 数组中的元素类型来区分调用的哪个方法
            MethodInfo method = typePerson.GetMethod("SayHello",new Type[] {typeof(string) });
            // 调用该重载方法
            method.Inovke(Activator.CreateInstance(typePerson),new object[] {"Hello,World！"});
            // 如果该方法有返回值，直接接收 Invoke() 方法的返回值即可

            // ================通过 Type 来创建对象
            // 1.根据 Person 的 Type 创建一个 Person 类型
            // typePerson.GetMethod().GetParameters()[0].ParameterType
            object obj = Activator.CreateInstance(typePerson);

            // 通过调用指定的构造函数来创建对象
            ConstructorInfo ctor = typePerson.GetConstructor(new Type[] { typeof(string),typeof(string),typeof(int)});
            // 调用构造函数创建对象
            object obj = ctor.Inovke(new object[] { "Tim",25,"Tim@ct.edu"});
            // 通过反射获取指定对象的属性的值
            PropertyInfo pInfo = typePerson.GetProperty("Name");
            string name = pInfo.GetValue(obj,null).ToString();
            Console.WriteLine(name);

            #endregion
        }
        
        
    }

    public class MyClass {

        public string[] _bfs;

        public string[] _gfs;

        public string Email { get; set; }

        public int Age { get; set; }
        
        public string Name { get; set; }

        public void Say() {
            Console.WriteLine("Stop Sleeping!");
        }
        
    }

    public class Person{
        public Person(){

        }

        public Person(string name,string email,int age){
            this.Name = name;
            this.Email = email;
            this.Age = age;
        }

        public string Name{ get; set; }

        public string Email{ get; set; }

        public int Age{ get; set; }

        public void SayHi(){
            Console.WriteLine("Hi!");
        }

        public void SayHello(string msg){
            Console.WriteLine(msg);
        }

        public void SayHello(){
            Console.WriteLine("Hi,我是 SayHi 的无参重载方法！");
        }

        public int Add(int n1,int n2){
            return n1 + n2;
        }
    }
    
}

