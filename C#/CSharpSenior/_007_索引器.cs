using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CSharpSenior {
    //若要在类或结构上声明索引器，请使用 this 关键字，如以下示例所示：
    /*
    public int this[int index] { // Indexer declaration  
        // get and set accessors  
    } 
    索引器的签名由其形参的数目和类型所组成。 它不包含索引器类型或形参的名称。 如果要在相同类中声明多个索引器，则它们的签名必须不同。
    索引器值不分类为变量；因此，无法将索引器值作为 ref 或 out 参数来传递

    可靠编程
    提高索引器的安全性和可靠性有两种主要方法：
        1.请确保结合某一类型的错误处理策略，以处理万一客户端代码传入无效索引值的情况。 
        在本主题前面的第一个示例中，TempRecord 类提供了 Length 属性，使客户端代码能在将输入传递给索引器之前对其进行验证。 
        也可将错误处理代码放入索引器自身内部。 请确保为用户记录在索引器的访问器中引发的任何异常。
        2.在可接受的程度内，为 get 和 set 访问器的可访问性设置尽可能多的限制。 这一点对 set 访问器尤为重要。 
    */
    class _007_索引器 {
        static void Main3(string[] args) {
            var stringCollection = new SampleCollection<string>();
            stringCollection[0] = "Hello, World";
            System.Console.WriteLine(stringCollection[0]);// Hello, World
        }

        static void Main4(string[] args) {
            var stringCollection = new SampleCollection2<string>();
            stringCollection.Add("Hello,World2");
            System.Console.WriteLine(stringCollection[0]);// Hello,World2
        }

        static void Main5(string[] args) {
            var stringCollection = new SampleCollection3<string>();
            stringCollection[0] = "Hello,World3";
            System.Console.WriteLine(stringCollection[0]);
        }

        static void Main6(string[] args) {
            TempRecord tempRecord = new TempRecord();
            tempRecord[3] = 58.3f;
            tempRecord[5] = 60.1f;

            for (int i = 0; i < 10; i++) {
                Console.WriteLine("Element #{0} = {1}",i,tempRecord[i]);
            }
        }

        static void Main(string[] args) {
            var week = new DayCollection();
            Console.WriteLine(week["Fri"]);
            try {
                Console.WriteLine(week["Made-up day"]);
            } catch (ArgumentOutOfRangeException e) {
                Console.WriteLine($"Not supported input:{e.Message}");
            }
        }
    }

    /*
    下列示例演示如何声明专用数组字段 temps 和索引器。 
    索引器可以实现对实例 tempRecord[i] 的直接访问。 
    若不使用索引器，则将数组声明为公共成员，并直接访问其成员 tempRecord.temps[i]。
    请注意，当评估索引器访问时（例如在 Console.Write 语句中），将调用 get 访问器。 因此，如果不存在 get 访问器，则会发生编译时错误。 
    */
    class TempRecord {
        private float[] temps = new float[] { 56.2f,56.7f,56.5f,56.9f,58.8f,61.3f,65.9f,62.1f,59.2f,57.5f};

        public TempRecord() { 

        }

        public int Length => temps.Length;

        public float this[int index] {
            get => temps[index];
            set => temps[index] = value;
        }
    }

    /*
    使用其他值进行索引:
        C# 不将索引参数类型限制为整数。
        例如，对索引器使用字符串可能有用。 
        通过搜索集合内的字符串并返回相应的值，可以实现此类索引器。 由于访问器可被重载，字符串和整数版本可以共存。
        下面的示例声明了存储星期几的类。 
        get 访问器采用字符串（星期几）并返回对应的整数。 例如，“Sunday”返回 0，“Monday”返回 1，依此类推。
    */
    class DayCollection {
        string[] days = { "Sun","Mon","Tues","Wed","Thurs","Fri","Sat"};

        public DayCollection() { 
        }
        
        public int this[string day] => FindDayIndex(day);

        private int FindDayIndex(string day) {
            for (int i = 0; i < days.Length; i++) {
                if (days[i] == day) {
                    return i;
                }
            }
            throw new ArgumentOutOfRangeException(nameof(day),$"Day {day} is not supported.Day input must be in te form \"Sun\", \"Mon\",etc");
        }
    }


    /*
    索引器（C# 编程指南）
    索引器概述：
        1.使用索引器可以用类似于数组的方式为对象建立索引。
        2.get 取值函数返回值。 set 取值函数分配值。
        3.this 关键字用于定义索引器。
        4.value 关键字用于定义 set 索引器所赋的值。
        5.索引器不必根据整数值进行索引；由你决定如何定义特定的查找机制。
        6.索引器可被重载。
        7.索引器可以有多个形参，例如当访问二维数组时。
        8.与属性一样，如果索引器的 Get 访问器包含单个返回值的语句或其 Set 访问器执行简单的赋值，则 Get 和 Set 访问器包含表达式主体定义。

    索引器允许类或结构的实例就像数组一样进行索引。 无需显式指定类型或实例成员，即可设置或检索索引值。 索引器类似于属性，不同之处在于它们的访问器需要使用参数。
    以下示例定义了一个泛型类，其中包含用于赋值和检索值的简单 get 和 set 访问器方法。 Program 类创建了此类的一个实例，用于存储字符串。
    */

    class SampleCollection<T> {
        private T[] arr = new T[100];
        public T this[int i] {
            get { return arr[i]; }
            set { arr[i] = value; }
        }

        public SampleCollection() {

        }
    }
    /*
    表达式主体定义
    索引器的 get 或 set 访问器包含一个用于返回或设置值的语句很常见。 为了支持这种情况，表达式主体成员提供了一种经过简化的语法。 自 C# 6 起，可以表达式主体成员的形式实现只读索引器，如以下示例所示。
    */
    class SampleCollection2<T> {
        private T[] arr = new T[100];
        int nextIndex = 0;

        public SampleCollection2() {

        }

        public T this[int i] => arr[i];

        public void Add(T value) {
            if (nextIndex >= arr.Length) {
                throw new IndexOutOfRangeException($"The collection can hold only {arr.Length} elements.");
            }
            arr[nextIndex++] = value;
        }
    }

    /*
    请注意，=> 引入了表达式主体，并未使用 get 关键字。
    自 C# 7.0 起，get 和 set 访问器均可作为表达式主体成员实现。 在这种情况下，必须使用 get 和 set 关键字。 例如： 
    */
    class SampleCollection3<T> {
        private T[] arr = new T[100];
        int nextIndex = 0;

        public SampleCollection3() {

        }

        public T this[int i] {
            get => arr[i];
            set => arr[nextIndex++] = (nextIndex <= arr.Length) ? value : throw new IndexOutOfRangeException($"The collection can hold only {arr.Length} elements.");
        }
    }

    /*
    属性和索引器之间的比较（C# 编程指南）
        索引器与属性相似。除下面所示差别外，对属性访问器定义的所有规则也适用于索引器访问器。
    Property                                索引器
    允许以将方法视为公共数据成员的方式            通过在对象自身上使用数组表示法，允许访问对象内部稽核的元素
    调用方法
    通过简单名称访问                           通过索引访问
    可以为静态成员或者实例成员                   必须是实例成员
    属性的 get 访问器没有任何参数                索引器的 get 访问器具有与索引器相同的形参列表
    属性的 set 访问器包含隐式 value  参数        索引器的 set 访问器具有与索引器相同的形参列表，value 参数也是如何
    通过自动实现的属性支持简短语法                支持仅适用索引器的 表达式主体(expression-bodied) 成员
    */
}
