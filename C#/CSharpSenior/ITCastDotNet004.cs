using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 集合ArrayList;
/**
* @author zlim
* @create 2020/5/15 18:26:05
*/

namespace 泛型集合练习 {

    public class Program {
        static void Main0(string[] args) {
            List<string> list = new List<string>();
            list.Add("Hello");
            list.Add("World");
            //Dictionary<>
            Dictionary<int, string> pairs = new Dictionary<int, string>();
            pairs.Add(22,"Z");
            pairs.Add(19, "Tim");
            
            foreach (var key in pairs.Keys) {
                Console.WriteLine(key);
            }
            Console.WriteLine("===========");
            foreach (var value in pairs.Values) {
                Console.WriteLine(value);
            }
            Console.WriteLine("===========");
            foreach (KeyValuePair<int,string> pair in pairs) {
                Console.WriteLine(pair.Key);
                Console.WriteLine(pair.Value);
            }
        }

        static void Main1(string[] args) {

            int[] arrInt = new int[] { 1,2,3,4,5,6,7,8,9};
            List<int> oddList = new List<int>();
            for (int i = 0; i < arrInt.Length; i++) {
                if (arrInt[i] % 2 != 0) {
                    oddList.Add(arrInt[i]);
                }
            }
            int[] newArray = oddList.ToArray();
            for (int i = 0; i < newArray.Length; i++) {
                Console.WriteLine(newArray[i]);
            }


        }

        static void Main2(string[] args) {

            string str = "1壹 2贰 3叁 4肆 5伍 6陆 7柒 8捌 9玖 0零";
            Dictionary<char, char> dict = new Dictionary<char, char>();
            string[] parts = str.Split(' ');
            for (int i = 0; i < parts.Length; i++) {
                dict.Add(parts[i][0],parts[i][1]);
                // parts[i] 是字符串 string 类型
                // 而 string 类型可以通过索引值获得每一个字符
            }
            Console.WriteLine("pls input an number:");
            string num = Console.ReadLine();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < num.Length; i++) {
                sb.Append(dict[num[i]]);
            }
            Console.WriteLine(sb);

        }

        static void Main3(string[] args) {

            string msg = "Hello,World!";
            Dictionary<char, int> dict = new Dictionary<char, int>();
            for (int i = 0; i < msg.Length; i++) {
                if (char.IsLetter(msg[i])) {
                    // 如果字典中不存在该“键”，则通过键获取值时会报异常
                    if (dict.ContainsKey(msg[i])) {
                        dict[msg[i]]++;
                    } else {
                        dict.Add(msg[i], 1);
                    }
                }
                }
            foreach (KeyValuePair<char,int> pair in dict) {
                Console.WriteLine($"字母 {pair.Key} 出现了{pair.Value}次");
            }

            // 字典还可以使用集合初始化器进行添加操作
            Dictionary<string, int> dict1 = new Dictionary<string, int>() { { "Tim",22},{ "Zlim",21} };
            foreach (KeyValuePair<string,int> pair in dict1) {
                Console.WriteLine($"{pair.Key} ---> {pair.Value}");
            }

        }

        static void Main(string[] args) {
            
        }

    }

}


namespace Hashtable集合{
    public class Program {
        static void Main0(string[] args) {
            Hashtable map = new Hashtable();
            map.Add("xzl",new Person() { Name = "Tim"});

            // 键值对集合的“键”一定不能重复。也即“键”一定要唯一
            //map.Add("xzl", new Person() { Name = "Tom" });
            // Unhandled exception. System.ArgumentException: Item has already been added. Key in dictionary: 'xzl'  Key being added: 'xzl'

            // 判断集合中是否存在某个键
            if (!map.ContainsKey("hl")) {
                map.Add("hl",20);
            }

            // 通过键获取值
            //Console.WriteLine(map["xzl"].ToString());

            Person p1 = map["xzl"] as Person;
            Console.WriteLine(p1.Name);

            // 遍历 Hashtable
            Console.WriteLine("=============");
            foreach (var key in map.Keys) {
                Console.WriteLine(key);
            }
            Console.WriteLine("=============");
            foreach (var value in map.Values) {
                Console.WriteLine(value);
            }
            Console.WriteLine("=============");
            foreach (DictionaryEntry entry in map) {
                Console.WriteLine(entry.Key);
                Console.WriteLine(entry.Value);
            }
        }

        static void Main1(string[] args) {
            // 类的初始化器是通过属性的 get; set; 进行赋值操作的
            // 只有当类中有无参构造函数时，才可以直接跟 {} 而不用加 () 
            Student stu = new Student/*("Zlim", 22)*/ { 
                Name = "Tim",
                SId = 101
            };
            Console.WriteLine(stu.Name);
            Console.WriteLine(stu.SId);
        }

    }

    public class Student {

        public Student() {

        }

        public Student(string name,int sid) {
            this.Name = name;
            this.SId = sid;
        }

        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name;

        public int SId {
            get { return _SId; }
            set { _SId = value; }
        }
        private int _SId;

    }


}


/*
集合命名空间：
    System.Collection.Generic; 非泛型集合
    System.Collection; 泛型集合
常用集合：
    “类似数组”集合：ArrayList、List<T>
    “键值对”集合（“哈希表”集合）：Hashtable、Dirctionary<K,V>
    “堆栈”集合：Stack、Stack<T>(LIFO)Last In First Out
    “队列”集合：Queue、Queue<T>(FIFO)First In Fisrt Out
    “可排序键值对”集合：（插入、检索没有“哈希表”集合高效）
    SortedList、SortedList<K,V>（占用内存更少，可以通过索引访问）
    SortedDictionary<K,V>（占用内存更多，没有索引，但插入、删除元素的速度比 SortedList 快）
    Set 集合：无序、不重复。HashSet<T> 可以将其视为不包含值的 Dictionary 集合，与 List<T> 类似，SortedSet<T>：.NET 
             4.0支持有序无重复集合
    “双向链表”集合：LinkedList<T>，增加、删除速度快
*/

namespace 集合ArrayList {
    public class ITCastDotNet004 {
        static void Main0(string[] args) {
            ArrayList arrayList = new ArrayList();
            Console.WriteLine("集合中元素的个数是：{0}",arrayList.Count);// 0
            Console.WriteLine("集合现在的容量是：{0}",arrayList.Capacity);// 0
            // 向集合中添加元素
            arrayList.Add(1);
            Console.WriteLine("集合中元素的个数是：{0}", arrayList.Count);// 1
            Console.WriteLine("集合现在的容量是：{0}", arrayList.Capacity);// 4

            arrayList.Add("1");
            Console.WriteLine("集合中元素的个数是：{0}", arrayList.Count);// 2
            Console.WriteLine("集合现在的容量是：{0}", arrayList.Capacity);// 4

            arrayList.Add("Tim");
            Console.WriteLine("集合中元素的个数是：{0}", arrayList.Count);// 3
            Console.WriteLine("集合现在的容量是：{0}", arrayList.Capacity);// 4

            Person p = new Person();
            p.Name = "Tom";
            arrayList.Add(p);
            Console.WriteLine("集合中元素的个数是：{0}", arrayList.Count);// 4
            Console.WriteLine("集合现在的容量是：{0}", arrayList.Capacity);// 4
            
            arrayList.Add(true);
            Console.WriteLine("集合中元素的个数是：{0}", arrayList.Count);// 5
            Console.WriteLine("集合现在的容量是：{0}", arrayList.Capacity);// 8

            arrayList.AddRange(new int[] { 1,3,5,7,8,10,12});
            Console.WriteLine("集合中元素的个数是：{0}", arrayList.Count);// 12
            Console.WriteLine("集合现在的容量是：{0}", arrayList.Capacity);// 16

            // 通过下标来获取集合中的元素
            Console.WriteLine("通过下标来获取集合中的元素");
            for (int i = 0; i < arrayList.Count; i++) {
                Console.WriteLine(arrayList[i]);
            }

            // 向指定位置插入一个元素
            Console.WriteLine("向指定位置插入一个元素:Insert(int index,object obj)");
            arrayList.Insert(0,"Lim");

            Console.WriteLine("向指定位置插入一个元素:InsertRange(int index, ICollection c)");
            arrayList.InsertRange(5,new string[] { "a","b","c"});

            Console.WriteLine("删除元素：使用 Clear() 方法");
            // 这样不能讲 arrayList 中的元素都删除掉，因为每删除一个元素 arrayList 的 Count 都会发生变化
            for (int i = 0; i < arrayList.Count; i++) {
                arrayList.RemoveAt(i);
            }
            Console.WriteLine("集合中元素的个数是：{0}", arrayList.Count);// 8
            Console.WriteLine("集合现在的容量是：{0}", arrayList.Capacity);// 16


        }

        static void Main1(string[] args) {

            ArrayList array = new ArrayList();
            array.Add(99);
            array.Add("Tim");
            Person p1 = new Person();
            p1.Name = "Tom";
            p1.Age = 22;

            array.Add(p1);
            Person p2 = new Person();
            p2.Name = "Tom";
            p2.Age = 22;

            array.Add(p2);

            Person p3 = new Person();
            p3.Name = "Tom";
            p3.Age = 22;

            array.Remove(p3);
            Console.WriteLine("集合中元素的个数是：{0}", array.Count);// 4

            p3 = p2;
            array.Remove(p3);
            Console.WriteLine("集合中元素的个数是：{0}", array.Count);// 3

            string name = new string(new char[] { 'T', 'i', 'm' });
            array.Remove(name);// 根据对象的 Equals 方法和 == 来删除元素
            // Contains() 方法的判断机制与 Remove() 方法一致
            Console.WriteLine("集合中元素的个数是：{0}", array.Count);// 2
            Console.WriteLine("把集合转换为数组的一般方法：array.ToArray();");
        }

        static void Main2(string[] args) {
            ArrayList arr = new ArrayList(new int[] { 1,32,87,48,2,5});
            // 默认 Sort() 方法是升序排序
            arr.Sort();
            // 如果想实现降序排序，可以先使用 Sort() 再使用 Reverse() 方法
            arr.Reverse();
            // 注意在调用完 Sort() 方法和 Reverse() 方法后会对原集合进行修改
            for (int i = 0; i < arr.Count; i++) {
                //Console.WriteLine(arr[i]);
            }

            ArrayList arr2 = new ArrayList();
            Person p1 = new Person();
            p1.Name = "Tim";
            p1.Age = 19;

            Person p2 = new Person();
            p2.Name = "Jerry";
            p2.Age = 18;

            Person p3 = new Person();
            p3.Name = "Jack";
            p3.Age = 17;

            Person p4 = new Person();
            p4.Name = "LimYoonA";
            p4.Age = 16;

            arr2.Add(p1);
            arr2.Add(p2);
            arr2.Add(p3);
            arr2.Add(p4);

            Console.WriteLine("通过 Sort() 方法实现 IComparable 接口实现自然排序。");
            arr2.Sort();
            // 要想让任何一种类型能在集合中实现 Sort 方法排序，那就要让这个类型实现 IComparable 接口，这就是接口的一个应用，也即接口是一种规范
            //Unhandled exception. System.InvalidOperationException: Failed to compare two elements in the array.
            //--->System.ArgumentException: At least one object must implement IComparable.
            for (int i = 0; i < arr2.Count; i++) {
                Console.WriteLine(((Person)arr2[i]).Name);
            }

            Console.WriteLine("通过 Sort(IComparer? comparer) 方法实现 IComparer 接口实现定制排序。");
            arr2.Sort(new PersonSortCompareByNameLengthAsc());
            for (int i = 0; i < arr2.Count; i++) {
                Console.WriteLine(((Person)arr2[i]).Name);
            }
        }

        static void Main3(string[] args) {
            // 使用集合初始化器，此时会将 string[] 数组看做一个整体加入到集合中
            ArrayList arrayList = new ArrayList() { new string[] { "a", "b", "c", "d", "e" } };
            Console.WriteLine(arrayList.Count);
            for (int i = 0; i < arrayList.Count; i++) {
                Console.WriteLine(arrayList[i]);
            }
            string[] strs = arrayList[0] as string[];
            for (int i = 0; i < strs.Length; i++) {
                Console.WriteLine(strs[i]);
            }

            ArrayList arrayList1 = new ArrayList(new string[] { "d", "e", "f", "g", "h" });
            for (int i = 0; i < arrayList1.Count; i++) {
                Console.WriteLine(arrayList1[i]);
            }

            //Random random = new Random(8);
            //for (int i = 0; i < 10; i++) {
            //    // 只要种子一样则每次生成的随机数都是一样的
            //    Console.WriteLine(random.Next(1,101));
            //}

            // Random random = new Random();
            // Random 的无参构造函数会调用 Random(Environment.TickCount);

            ArrayList arrayList2 = new ArrayList();
            int count = 0;
            Random random = new Random();
            while (arrayList2.Count < 10) {
                // 生成1到100之间的随机数，左闭右开区间
                int num = random.Next(1, 101);
                if (num % 2 == 0 && !arrayList2.Contains(num)) {
                    arrayList2.Add(num);
                }
                count++;
            }
            for (int i = 0; i < arrayList2.Count; i++) {
                Console.WriteLine(arrayList2[i]);
            }
            Console.WriteLine($"======{count}=======");

        }
    }

    // 这个类实际上就是一个实现 IComparer 接口的比较器
    public class PersonSortCompareByNameLengthAsc : IComparer {
        public int Compare(object x, object y) {
            Person p1 = x as Person;
            Person p2 = y as Person;
            if (p1 != null && p2 != null) {
                return p2.Name.Length - p1.Name.Length;
            } else {
                throw new NullReferenceException("The Element cann't be null.");
            }
        }
    }

    public class Person :IComparable {
        public int Age { get; set; }
        public string Name { get; set; }

        public int CompareTo(object obj) {
            Person p = obj as Person;
            if (p != null) {
                return p.Age - this.Age;// 按照年龄降序排序
                //return this.Age - p.Age;// 按照年龄升序排序
            }
            return 0;
        }


    }


}
