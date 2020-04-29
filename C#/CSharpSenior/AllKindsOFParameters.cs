using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;

namespace CSharpSenior {

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

        static void Main(string[] args){
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
