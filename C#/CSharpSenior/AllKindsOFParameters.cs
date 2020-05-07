using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;


namespace N{
    class A{
        public class B{}

        // static void Main(){new A.B();} // N.A.B

        static void Main0(){new global::A.B();} // A.B

    }
}

namespace A{
    class B{}
}


namespace CSharpSenior {

    public class A{
    public int Counter = 1;
    }

    public class B : A{
        public new int Counter = 2;
        // '“B.Counter”隐藏 继承的成员“A.Counter”。如果是有意隐藏，请使用关键字 new。
        // 这里的 new 修饰符只是消除运行时的 warning 
    }

    class Program0{
        static void Main0(){
            A a = new A();
            System.Console.WriteLine(a.Counter);// 1
            
            B b = new B();
            System.Console.WriteLine(b.Counter);// 2
            
            A x = new B();
            System.Console.WriteLine(x.Counter);// 1
            // 虽然 x 引用着一个 B 类型的对象
            // 但是引用变量 x 的类型是 A 类型的
            // 故输出为 1 
            System.Console.WriteLine(x.GetType());// CSharpSenior.B
            System.Console.WriteLine(x.GetType().BaseType);// CSharpSenior.A
            System.Console.WriteLine();

        }
    }

    interface IDo{
        void Do();
    }

    // 当父类中的 Do 没有使用 virtual 修饰，子类中的 Do 没有使用 override 修饰时
    // public class Parent : IDo{
    //     public void Do() => System.Console.WriteLine("Parent");
    // }

    // public class Child : Parent{
    //     public void Do() => System.Console.WriteLine("Child");
    // }

    public class Parent : IDo{
        public virtual void Do() => System.Console.WriteLine("Parent");
    }

    public class Child : Parent{
        public override void Do() => System.Console.WriteLine("Child");
    }

    class Program1 {
        static void Main(){
            
            // 当父类中的 Do 没有使用 virtual 修饰，子类中的 Do 没有使用 override 修饰时
            // Child c = new Child();
            // c.Do();// Child

            // ((Parent)c).Do();// Parent
            // ((IDo)c).Do();// Parent 
            // // 因为 Parent 中的 Do 方法才是对 IDo 接口的直接实现
            // // 故这里访问的是 Parent 中的 Do 方法

            Child c = new Child();
            c.Do();// Child
            ((Parent)c).Do();// Child
            ((IDo)c).Do();// Child 
            // 此时虽然将 c 变量分别转换为了 Parent 和 IDo
            // 但是由于子类 Child 中重写了 Parent 中的 Do 方法
            // 相当于进行了覆盖，不管是哪个类型变量实际上都只能看到
            // 一个 Do 方法，那就是子类 Child 中的 Do 方法

            Parent p = new Parent();
            p.Do();// Parent
            ((IDo)p).Do();// Parent
            // 此时只有 Parent 中的 Do 是 IDo 接口唯一知道的实现方法
            // 故只会输出 Parent 
        }
    }

    class FirstCSharp{
        static void Main0(string[] args){

            // Console.WriteLine(1.0.GetType());// System.Double
            // Console.WriteLine(1e06.GetType());// System.Double
            // Console.WriteLine(1.GetType());// System.Int32
            // Console.WriteLine(0xF000_0000.GetType());// System.UInt32
            // Console.WriteLine(0x10000_0000.GetType());// System.Int64
            // Console.WriteLine(0b1100_0010.GetType());// System.Int32

            // int i = 10000_0001;
            // float f = i;
            // int i2 = (int)f;

            // Console.WriteLine(i);//  100000001
            // Console.WriteLine(f);//  100000000
            // Console.WriteLine(i2);// 100000000

            // Console.WriteLine(1.0 / 0.0 );// ∞
            // Console.WriteLine(-1.0 / 0.0 );// -∞
            // Console.WriteLine(1.0 / -0.0 );// -∞
            // Console.WriteLine(-1.0 / -0.0 );// ∞

            // Console.WriteLine(0.0 / 0.0 );// NaN
            // Console.WriteLine((1.0 / 0.0) - (1.0 / 0.0) );// NaN

            // Console.WriteLine( (0.0 / 0.0 ) == double.NaN );// False

            // Console.WriteLine(object.Equals(0.0 / 0.0 ,double.NaN) );// True
            
            // decimal m = 1m / 6m;
            // double d = 1.0 / 6.0;

            // decimal mm = m + m + m + m + m + m;
            // double dd = d + d + d + d + d + d;

            // Console.WriteLine(mm == 1m);// False
            // Console.WriteLine(dd < 1.0);// True
    
    
            // int[,] matrix = new int[3,3];
            // for(int i = 0;i < matrix.GetLength(0);i++){

            //     for(int j = 0;j < matrix.GetLength(1);j++){
            //         matrix[i,j] = i *  3 + j;
            //         System.Console.Write(matrix[i,j]);
            //         System.Console.Write("\t");
            //     }
            //     System.Console.WriteLine();

            // }
            /*
            0       1       2
            3       4       5
            6       7       8
            */
    
            // int[,] matrix2 = new int[,]{
            //     {0,1,2},
            //     {3,4,5},
            //     {6,7,8}
            // };
    
            // int[][] matrix = new int[3][];

            // for(int i = 0;i < matrix.GetLength(0);i ++){
                
            //     matrix[i] = new int[i + 3];
            //     for(int j = 0;j < matrix[i].Length;j++){
            //         matrix[i][j] = i * (matrix[i].Length - 1) + j;

            //         System.Console.Write(matrix[i][j]);
            //         System.Console.Write("\t");
            //     }
            //     System.Console.WriteLine();

            // }
            /*
            0       1       2
            3       4       5       6
            8       9       10      11      12
            */

            // int[][] matrix = new int[3][]{
            //     new int[]{1,2,3},
            //     new int[]{1,2,3,4},
            //     new int[]{1,2,3,4,5}
            // };

            // char[] vowels = new char[]{'a','e','i','o','u'};
            // char[] vowels1 = {'a','e','i','o','u'};
            
            // var vowels2 = new char[] {'a','e','i','o','u'};

            // for(int i = 0;i < vowels2.Length;i++){
            //     System.Console.WriteLine(vowels[i]);
            // }
            // System.Console.WriteLine(vowels2.GetType());
            // System.Char[]

            // Split("Stevie Ray Vaughan",out string a,out string b);
            // System.Console.WriteLine(a);// Stevie Ray
            // System.Console.WriteLine(b);// Vaughan

            // Split("Stevie Ray Vaughan",out string c,out _);
            // System.Console.WriteLine(c);// Stevie Ray
        
        }

        // static int x;

        static void Main1(string[] args){
            // Foo(out x);

            // int total = Sum(1,2,3,4);
            // // int total = Sum(new int[]{1,2,3,4});
            // System.Console.WriteLine(total);// 10

            // Foo(1);
            // Foo();
            /*
            1,0
            0,0
            */

            // Foo(1);// 1,System.Int32[]

            // Foo2(1,2);      // 1,2
            // Foo2(x:1,y:2);  // 1,2

            // Foo2(x:1,y:2);  // 1,2
            // Foo2(y:2,x:1);  // 1,2

            // int a = 0;
            // Foo2(y:++a,x:--a);// 0,1: ++a is evaluated first

            // int b = 0;
            // Foo2(x:--b,y:++b);// -1,0

            // int[] numbers = {0,1,2,3,4};
            // ref int numRef = ref numbers[2];
            // numRef *= 10;
            // System.Console.WriteLine(numRef);// 20
            // System.Console.WriteLine(numbers[2]);// 20

            ref string xRef = ref GetX();// Assign result to a ref local
            xRef = "New Value";
            System.Console.WriteLine(X);// New Value


        }

        static ref string GetX() => ref X;// This method returns a ref
        static string X = "Old Value";

        static void Foo2(int x,int y){
            System.Console.WriteLine(x + "," + y);
        }

        static void Foo(int x =0, params int[] y ){
            System.Console.WriteLine(x + "," + y);
        }

        static int Sum(params int[] ints){
            int sum = 0;
            for(int i = 0;i < ints.Length;i++){
                sum += ints[i];
            }
            return sum;
        }

        // static void Foo(out int y){
        //     System.Console.WriteLine(x);// x is 0
        //     y = 1;// Mutate y
        //     System.Console.WriteLine(x);// x is 1
        // }

        static void Split(string name,out string firstNames,out string lastName){

            int i = name.LastIndexOf(' ');
            firstNames = name.Substring(0,i);
            lastName = name.Substring(i+1);
        }

        static void Main2(string[] args){
            // string s1 = "something";
            // string s2 = s1 ?? "nothing";
            // System.Console.WriteLine(s2);// something
            
            // System.Text.StringBuilder sb = null;
            // // string s = sb.ToString();// 抛出了 System.NullReferenecException
            // string s = sb?.ToString();// No error: s instead evaluates to null
            // string s3 = sb == null ? null : sb.ToString();
            // System.Console.WriteLine(s);
            // System.Console.WriteLine(s3);

            // System.Text.StringBuilder sb = null;
            // string s =  sb?.ToString() ?? "nothing";// s evaluates to "nothing"
            // System.Console.WriteLine(s);// nothing

            // // Declare variables with declaration statements:
            // string s;
            // int x,y;
            // System.Text.StringBuilder stringBuilder;

            // // Expression statements
            // x = 1 + 2;// Assignment expression
            // x++;      // Increment expression  
            // y = Math.Max(x,5);// Assignment expression 
            // Console.WriteLine(y);// Method call expression 
            // stringBuilder = new StringBuilder();// Assignment expression
            // new StringBuilder();// Object instantiation expres

            // new StringBuilder();// Legal,but useless
            // new string('c',3);  // Legal,but useless
            // x.Equals(y);        // Legal,but useless
            
            









        }

        static void TellMeTheType(object x){
            switch(x){
                case bool b when b == true:
                    System.Console.WriteLine("True!");
                    break;
                case bool b:
                    System.Console.WriteLine("False!");
                    break;
                case null:
                    System.Console.WriteLine("Nothing here");
                    break;
            }

            // switch(x){
            //     case int i :
            //         System.Console.WriteLine("It's an int!");
            //         System.Console.WriteLine($"The square of {i} is {i * i}");
            //         break;
            //     case string s:
            //         System.Console.WriteLine("It's a string");
            //         System.Console.WriteLine($"The length of {s} is {s.Length}");
            //         break;
            //     default:
            //         System.Console.WriteLine("I don't know what x is");
            //         break;
            // }
        }

        static void ShowCard(int cardNumber){
            switch (cardNumber){
                case 13:
                case 12:
                case 11:
                    System.Console.WriteLine("Face card");
                    break;
                default:
                    System.Console.WriteLine("Plain card");
                    break;
            }
            // switch(cardNumber){
            //     case 13:
            //         System.Console.WriteLine("King");
            //         break;
            //     case 12:
            //         System.Console.WriteLine("Queen");
            //         break;
            //     case 11:
            //         System.Console.WriteLine("Jack");
            //         break;
            //     case -1:
            //         goto case 12;
            //     default:
            //         System.Console.WriteLine(cardNumber);
            //         break;
            // }
        }


    }



    class AllKindsOFParameters {

        static void Main1(string[] args) {
            var myList = new List<int>() { 12,11,9,14,15};
            bool result = AllGreaterThanTen(myList);
            Console.WriteLine(result);
            bool result2 = myList.All(i => i > 10);
            Console.WriteLine(result2);

        }

        static bool AllGreaterThanTen(List<int> intList) {
            foreach (var item in intList) {
                if (item <= 10) {
                    return false;
                }
            }

            return true;
        }

        static void Main0(string[] args) {
            //Student stu = null;

            //if (StudentFactory.Create("Tim",33,out stu)) {
            //    Console.WriteLine("Student {0} ,age is {1}",stu.Name,stu.Age);
            //}

            var outterStu = new Student() { Age = 24,Name = "Tim"};
            Console.WriteLine("HashCode = {0},Name = {1}", outterStu.GetHashCode(), outterStu.Name);
            Console.WriteLine("=========================================");
            IWantSideEffect(ref outterStu);
            Console.WriteLine("HashCode = {0},Name = {1}", outterStu.GetHashCode(), outterStu.Name);
            Console.WriteLine("=========================================");
            string outterStuAddr = getMemory(outterStu);
            Console.WriteLine(outterStuAddr);
        }

        static void IWantSideEffect(ref Student stu) {
            stu = new Student() { Age = 23, Name = "Tom"};
            Console.WriteLine("HashCode = {0},Name = {1}",stu.GetHashCode(),stu.Name);
            //string stus = Convert.ToString(stu,2).PadLeft(32,'0');
        }

        public static string getMemory(object o) {// 获取引用类型的内存地址方法  
            GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);

            IntPtr addr = GCHandle.ToIntPtr(h);

            return "0x" + addr.ToString("X");
        } 


    }

    [StructLayout(LayoutKind.Sequential)]
    class Student {
        public int Age { get; set; }
        public string Name { get; set; }
    }

    class StudentFactory {
        public static bool Create(string stuName,int stuAge,out Student stu) {
            stu = null;
            if (string.IsNullOrEmpty(stuName)) {
                return false;
            }
            if ( stuAge < 20 || stuAge > 80) {
                return false;
            }
            stu = new Student() { Name = stuName,Age = stuAge};
            return true;
        }
    }

}
