using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * @author zlim
 * @create 2020/5/15 18:26:05
 */


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
    }

    public class Person {
        public int Age { get; set; }
        public string Name { get; set; }
    }
}
