//#define word // 只能在当前文件中生效
using System;
using System.IO;
using System.Reflection;

namespace ReflectAndAttribute{
    public class Program{
        /// <summary>
        /// 反射入门：最简单的反射
        /// </summary>
        /// <param name="args"></param>
        public static void Main0(string[] args){
            // 运行时
            Console.WriteLine("".GetType().Module);
            // System.Private.CoreLib.dll
            // 编译时
            Console.WriteLine(typeof(Int32).Assembly);
            // System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e
        }


        /// <summary>
        /// 条件编译符(Condition Complier)：#if ... #elif ... #endif：编译时就选择哪一条语句编译
        /// </summary>
        /// <param name="args"></param>
        public static void Main1(string[] args) {

#if word
            Console.WriteLine("This is XML.");
#else
            Console.WriteLine("This is Memory.");
#endif


#if XML // 条件编译符：使用时可以在 using 前面写 #define XML 来选择编译那条语句（但是这种写法需要修改源码）：这种写法的作用域为当前文件
        // 或者选择编辑项目属性，在生成 -> 常规 -> 条件编译和符号中填写 XML 或者 Memory（这种写法如果需要更换选择则需要重新编译项目）：这种写法的作用域是整个项目
            IRepoistory<Arcticle> repo = new XArticleRepository<Arcticle>();
#elif Memory
            IRepoistory<Arcticle> repo = new XRepository<Arcticle>();
#endif

        }


        /// <summary>
        /// 反射：调用方法
        /// </summary>
        /// <param name="args"></param>
        public static void Main2(string[] args) {
            /*string path = Path.Join(Environment.CurrentDirectory,"config.txt");
            string classType = File.ReadAllText(path);*/

            // 如果找不到这个类，就返回 null
            Type target = Type.GetType("ReflectAndAttribute.Student");  // 必须是类的全类名，也即 Assembly 中的名称，一般为名称空间名+类名（注意是类名而不是类文件名，在C#中文件名通常就是类名）
            //Type complied = typeof(Student);


            // GetConstructor(Type[] types) 获取构造函数信息；Invoke(Object []) 调用构造函数并传入参数
            // 这里表示调用 Student 类中的无参构造函数，返回类的实例
            object objStu = target.GetConstructor(new Type[] { }).Invoke(null);

            // 这里表示调用 Student 中一个参数为 string 类型的构造函数，并传入参数 "Zlim"
            object objStu2 = target.GetConstructor(new Type[] { typeof(string) }).Invoke(new object[] { "Zlim"});
            // 类似于：Student s = new Student("Zlim");

            //objStu.Greet(); --- 不行，因为 objStu 是 object 类型的，而不是 Student 类型
            // 调用类的成员（方法）：Invoke 第一参数是调用哪个实例的方法，第二个参数是传入调用方法的参数值，如果方法不需要传参则写 null
            target.GetMethod("Greet").Invoke(objStu,null);
        }


        /// <summary>
        /// 反射：调用方法2：通过接口/基类让反射更灵活
        /// </summary>
        /// <param name="args"></param>
        public static void Main3(string[] args) {
            string path = Path.Join(Environment.CurrentDirectory, "config.txt");
            string classType = File.ReadAllText(path);

            Type target = Type.GetType(classType);
            IPerson person = null;
            try {
                //person = (IPerson)target.GetConstructor(new Type[] { typeof(string) }).Invoke(new object[] { "ZLim"});
                person = target.GetConstructor(new Type[] { typeof(string) }).Invoke(new object[] { "ZLim" }) as IPerson; 

            } catch (Exception e) {
                Console.WriteLine("Something wrong happend .... ");
                throw;
            }

            person.Greet();
        }


        /// <summary>
        /// 反射：调用方法3：忽视访问修饰符 / 枚举与位运算
        /// </summary>
        /// <param name="args"></param>
        public static void Main4(string[] args) {
            /*Type tStu = typeof(Student);
            // 必须加上 BindingFlags 的两个参数
            Console.WriteLine(tStu.GetField("_age",BindingFlags.NonPublic | BindingFlags.Instance).GetValue(new Student()));// 23*/


            /*Console.WriteLine(1| 2);
            Console.WriteLine(2 | 4);
            Console.WriteLine(1 & 2);
            Console.WriteLine(2 & 4);

            Console.WriteLine(((1 | 2) & 1) == 1);// true
            Console.WriteLine(((1 | 2) & 2) == 2);// true*/

            Student s = new Student();
            s.AddRole(Role.Student);
            Console.WriteLine(s.Roles);

            Teacher t = new Teacher();
            t.AddRole(Role.Teacher);
            t.AddRole(Role.TeamLeader);
            Console.WriteLine(t.Roles);// Teacher, TeamLeader
            t.Roles = t.Roles ^ Role.TeamLeader;// 移除某权限：异或该权限即可
            Console.WriteLine(t.Roles);// Teacher


        }


        /// <summary>
        /// 特性
        /// </summary>
        /// <param name="args"></param>
        public static void Main5(string[] args) {
            Student s = new Student();

            Attribute attribute = MyOwnAttribute.GetCustomAttribute(typeof(Student), typeof(MyOwnAttribute));
            Console.WriteLine((attribute as MyOwnAttribute).Fee);
        }

    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple =true)]// 表示此特性仅能使用与 class 与 method,并且可以标记多次
    public class MyOwnAttribute:Attribute {
        public MyOwnAttribute() {
            Console.WriteLine("MyOwn is init ... ");
        }

        // 构造函数的参数可以在 Attribute 标记时赋值
        public MyOwnAttribute(string city) {
            Console.WriteLine($"working in {city}");
        }

        // 属性可以在 Attribute 标记时赋值
        public double Fee { get; set; }

        // Attribute 中同样可以有方法
        public void SuperVise() {

        }

    }


    [Flags]
    public enum Role {
        Student = 1,
        Teacher = 2,
        TeacherAssist = 4,
        TeamLeader = 8,
        DormitoryHead = 16
    }
}
