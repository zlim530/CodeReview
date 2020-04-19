using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpSenior {
    /*
    使用泛型可以带来以下好处：
        1，代码(算法)复用。
        2，类型安全。
        3，提高性能，避免了装箱和拆箱。
        4，扩展性。
        声明泛型类型跟声明普通类型差不多，不同的地方在于，泛型在类名后面放一对尖括号，并且使用了类型参数。
    */
    class _008_泛型 {

        // 内部类
        private class ExampleClass {
            public ExampleClass() {

            }
        }

        static void Main0() {
            GenericList<int> genericList = new GenericList<int>();
            genericList.Add(1);

            GenericList<string> genericList1 = new GenericList<string>();
            genericList1.Add("");

            GenericList<ExampleClass> genericList2 = new GenericList<ExampleClass>();
            genericList2.Add(new ExampleClass());
        }

        static void Main1(string[] args) {
            GenericList<int> list = new GenericList<int>();
            for (int i = 0; i < 10; i++) {
                list.AddHead(i);
            }

            foreach (int i in list) {
                Console.WriteLine(i + "");
            }
            Console.WriteLine("\nDone");
        }

        static void Main2(string[] args) {
            MyGenericArray<int> intArray = new MyGenericArray<int>(5);

            for (int i = 0; i < 5; i++) {
                intArray.setItem(i,i * 5);
            }
            for (int i = 0; i < 5; i++) {
                Console.WriteLine(intArray.getItem(i) + "");                                                                                                      
            }
            Console.WriteLine("\n");
            MyGenericArray<char> charArray = new MyGenericArray<char>(5);

            for (int i = 0; i < 5; i++) {
                charArray.setItem(i, (char)(i + 97));
            }
            for (int i = 0; i < 5; i++) {
                Console.WriteLine(charArray.getItem(i) + "");
            }
            Console.WriteLine("\nDone");
        }

        static void Swap<T>(ref T lhs,ref T rhs) {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        static void Main3(string[] agrs) {
            int a, b;
            char c, d;
            a = 10;
            b = 20;
            c = 'I';
            d = 'V';

            Console.WriteLine("Int value before calling swap:");
            Console.WriteLine("a = {0}, b = {1}",a,b);
            Console.WriteLine("Char values before calling swap:");
            Console.WriteLine("c = {0}, d = {1}",c,d);

            Swap<int>(ref a,ref b);
            Swap<char>(ref c,ref d);

            Console.WriteLine("Int value after calling swap:");
            Console.WriteLine("a = {0}, b = {1}", a, b);
            Console.WriteLine("Char values after calling swap:");
            Console.WriteLine("c = {0}, d = {1}", c, d);

        }

        /*
        泛型（Generic）委托
        可以通过类型参数定义泛型委托。例如：
        delegate T NumberChanger<T>(T n);
        */

        delegate T NumberChanger<T>(T n);

        static int num = 10;
        public static int AddNum(int p) {
            num += p;
            return num;
        }

        public static int MultNum(int q) {
            num *= q;
            return num;
        }

        public static int getNum() {
            return num;
        }

        static void Main(string[] args) {
            NumberChanger<int> nc1 = new NumberChanger<int>(AddNum);
            NumberChanger<int> nc2 = new NumberChanger<int>(MultNum);
            nc1(25);
            Console.WriteLine("Value of Num:{0}",getNum());
            nc2(5);
            Console.WriteLine("Value of Num:{0}",getNum());
        }

        /*
        在声明泛型方法/泛型类的时候，可以给泛型加上一定的约束来满足我们特定的一些条件。
        */
        public class CacheHelper<T> where T :new() {

        }
        /*
        泛型限定条件：
         T：结构（类型参数必须是值类型。可以指定除 Nullable 以外的任何值类型）
         T：类 （类型参数必须是引用类型，包括任何类、接口、委托或数组类型）
         T：new() （类型参数必须具有无参数的公共构造函数。当与其他约束一起使用时new() 约束必须最后指定）
         T：<基类名> 类型参数必须是指定的基类或派生自指定的基类
         T：<接口名称> 类型参数必须是指定的接口或实现指定的接口。可以指定多个接口约束。约束接口也可以是泛型的。
        */

    }

    public class MyGenericClacc<TRequest,TResponse> where TRequest:class where TResponse:class {

    }

    public class GenericList<T> {

        private class Node {

            private Node next;
            public Node Next {
                get => next;
                set => next = value;
            }

            private T data;

            public T Data { get; set; }

            public Node(T t) {
                next = null;
                data = t;
            }
        }

        private Node head;

        public GenericList() {
            head = null;
        }

        public void Add(T input) { }

        public void AddHead(T t) {
            Node n = new Node(t);
            n.Next = head;
            head = n;
        }

        public IEnumerator<T> GetEnumerator() {
            Node current = head;

            while (current != null) {
                yield return current.Data;
                current = current.Next;
            }
        }
    }

    public class MyGenericArray<T> {
        private T[] array;
        public MyGenericArray(int size) {
            array = new T[size + 1];
        }
        public T getItem(int index) {
            return array[index];
        }
        public void setItem(int index,T value) {
            array[index] = value;
        }

    }
}
