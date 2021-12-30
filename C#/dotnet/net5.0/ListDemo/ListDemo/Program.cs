using System;
using System.Collections.Generic;
using ListDemo;

List<double> list1 = new List<double> { 80.0, 70.0, 60.0, 50.0, 40.0, 30.0, 20.0, 10.0 };
Book book1 = new Book { Id = 1, Name = "Book-1", Price = 10 };
Book book2 = new Book { Id = 2, Name = "Book-2", Price = 20 };
Book book3 = new Book { Id = 3, Name = "Book-3", Price = 30 };
Book book4 = new Book { Id = 4, Name = "Book-4", Price = 40 };
Book book5 = new Book { Id = 1, Name = "Book-1", Price = 10 };
List<Book> list2 = new List<Book> { book1, book2, book3, book4 };


//使用二分查找之间必须对集合进行排序也即二分查找只能对有序集合进行查找才能找到正确的结果
//list1.Sort();
//Console.WriteLine(string.Join(",",list1));
//var res = list1.BinarySearch(30);
//Console.WriteLine(res);

// 当我们调用集合的 Sort 方法，集合就会去调用元素的 CompareTo 方法，让元素之间进行互相比较，最后得出一个顺序来
// double 类型的值 DotNet 已经为我们实现好了 CompareTo 方法，可以 Book 类型是没有 CompareTo 这个方法的，因此
// 对 list2 调用 Sort 方法会报错，因此我们需要为 Book 类添加 CompareTo 方法，但是不能直接添加此方法，而是需要通过
// 实现 IComparable<T> 泛型接口的方式去添加此方法
list2.Sort();
var res = list2.BinarySearch(book5);
Console.WriteLine(res);// 0



//var res = list1.FindIndex(e => e % 3 == 0);
//Console.WriteLine(res);
//var re2s = list1.FindIndex(3, e => e % 3 == 0);
//Console.WriteLine(re2s);
//var re3s = list1.FindIndex(3,2, e => e % 3 == 0);
//Console.WriteLine(re3s);


//var re4s = list2.FindLastIndex(e => e.Price % 4 == 0);
//Console.WriteLine(re4s);
//var re5s = list2.FindLastIndex(2, e => e.Price % 4 == 0);
//Console.WriteLine(re5s);
//var re6s = list2.FindLastIndex(2,2, e => e.Price % 4 == 0);
//Console.WriteLine(re6s);


//var res = list1.Find(e => e % 3 == 0);
//Console.WriteLine(res); // 60
//var re2s = list2.Find(e => e.Price % 4 == 0);
//Console.WriteLine(re2s); // {"Id":2,"Name":"Book-2","Price":20}
//var re3s = list1.FindLast(e => e % 3 == 0);
//Console.WriteLine(re3s); // 30
//var re4s = list2.FindAll(book => book.Price % 4 == 0);
//Console.WriteLine(string.Join(",",re4s)); // {"Id":2,"Name":"Book-2","Price":20},{"Id":4,"Name":"Book-4","Price":40}





#region 泛型List类（Part-3）

//List<double> list1 = new List<double> { 100.0, 200.0, 300.0, 400.0, 100.0, 200.0, 300.0/*, 400.0*/ };
//Book book1 = new Book { Id = 1, Name = "Book-1", Price = 10 }, book6 = book1;
//Book book2 = new Book { Id = 2, Name = "Book-2", Price = 20 };
//Book book3 = new Book { Id = 3, Name = "Book-3", Price = 30 };
//Book book4 = new Book { Id = 4, Name = "Book-4", Price = 40 };
//Book book5 = new Book { Id = 1, Name = "Book-1", Price = 10 };
//List<Book> list2 = new List<Book> { book1, book2, book3, book4 };

//int res = list1.LastIndexOf(300);
//Console.WriteLine(res); // 6
//int re2s = list1.LastIndexOf(300, 5);
//Console.WriteLine(re2s); // 2
//int re3s = list1.LastIndexOf(300, 4, 2);
//Console.WriteLine(re3s); // -1


//int res = list1.IndexOf(300, 3, 4);
//Console.WriteLine(res); // 6
//int re2s = list1.IndexOf(300, 3, 3);
//Console.WriteLine(re2s);// -1

// 在list1集合中找到所有元素等于300的索引值
//int i = -1;
//while (true)
//{
//    i = list1.IndexOf(300, i + 1);
//    if (i == -1) break;
//    Console.WriteLine(i);
//}
//当我们找到最后一个300时，索引值为6，此时300的后面不再有元素，而我们再对i进行+1并放入IndexOf中查找并不会报错，因此IndexOf 有一定的容错性：它与迭代器的Current属性一样，在第一个位置的前部与最后一个位置的尾部都指向了类型的默认值


// 在 list1 集合中从索引值为3的位置处开始查找300，包含索引值为3位置的元素
//int res = list1.IndexOf(300, 3);
//Console.WriteLine(res);// 6

//300或300 .0都会找到list1集合中的第一个300这说明 IndexOf 与 Contains 一样底层都是通过是否相等即 == 操作符进行判断
//int res = list1.IndexOf(300);
//Console.WriteLine(res);// 2

//bool res = list1.TrueForAll(x => x % 100 == 0);
//Console.WriteLine(res);// True

//bool res = list2.Exists(b => b.Price <= 40);
//Console.WriteLine(res);

// 如果我们需要Book类型对象判断相等的条件是Id、Name与Price同时相等，那么我们可以重写Object类的 Equals 方法，此时即可实现上述效果：
//bool res = list2.Contains(book6);
//Console.WriteLine(res); // True：因此 book5 与 book1 中的所有属性值都相等



//bool res = list2.Contains(book5);
//Console.WriteLine(res); // False 
//// 尽管Book类型的引用变量book5与book1中所有属性的值都一样，但他们分别引用了两个Book类型的实例对象，因此在list2中并不包含book5；
//bool re2s = list2.Contains(book6);
//Console.WriteLine(re2s); // True
// 而对于book6而言，它与book1指向了同一个Book的实例对象，因此通过 Contains 方法会发现 list2 中包含 book6


//bool res = list1.Contains(400.0);
//Console.WriteLine(res); // True
//var re2s = list1.Contains(400);
//Console.WriteLine(re2s); // True
//bool a = 400 == 400.0;
//Console.WriteLine(a);

#endregion


#region 泛型List类（Part-2）

//List<int> intList = new List<int> { 100, 200, 300, 400 }; // int is Value Type
//List<Book> bookList = new List<Book>(); // Book is Reference Type
//for (int i = 1; i < 10; i++)
//    bookList.Add(new Book { Id = 1, Name = $"Book-{i}", Price = 10 * i});

//Console.WriteLine($"intList:{intList.Count}/{intList.Capacity}");
//Console.WriteLine($"bookList:{bookList.Count}/{bookList.Capacity}");
//Console.WriteLine("===================");

//// ForEach 方法进一步简化了 foreach 语句的迭代
//int sum = 0;
////intList.ForEach(val => sum += val);
//intList.ForEach(val =>
//{
//    Console.WriteLine(val);
//    sum += val;
//});

//Console.WriteLine(sum);

//可以改变对象中属性的值，因为这不会改变对象本身的引用也不会改变迭代器中元素的个数，所以没有影响
//foreach (var val in bookList)
//{
//    val.Price++;
//}

//Console.WriteLine(string.Join(",", bookList));


// foreach 语句帮助我们避免了迭代器在一开始和最后会指向没有元素的空位置的情况，同样是迭代我们的集合
//foreach (var val in intList)
//{
//    Console.WriteLine(val);
//}

//foreach (var val in bookList)
//{
//    Console.WriteLine(val);
//}



//var e = bookList.GetEnumerator();
//Console.WriteLine(e.Current);
//while (e.MoveNext())
//{
//    Console.WriteLine(e.Current);
//}

//Console.WriteLine(e.Current);
// 前后两个空行是由于我们尝试打印null值造成的，
// 引用类型的默认值为null
/*

{"Id":1,"Name":"Book-1","Price":10}
{"Id":1,"Name":"Book-2","Price":20}
{"Id":1,"Name":"Book-3","Price":30}
{"Id":1,"Name":"Book-4","Price":40}
{"Id":1,"Name":"Book-5","Price":50}
{"Id":1,"Name":"Book-6","Price":60}
{"Id":1,"Name":"Book-7","Price":70}
{"Id":1,"Name":"Book-8","Price":80}
{"Id":1,"Name":"Book-9","Price":90}

*/


// List<int>.Enumerator 是一个枚举类，它实现了 IEnumerator<T>, IEnumerator, IDisposable 接口
//Current属性：当前迭代器的所拿到的成员元素
//这里看一下当我们一拿到迭代器时，当前成员元素是哪个
//List<int>.Enumerator e = intList.GetEnumerator();
//Console.WriteLine(e.Current);
//while (e.MoveNext())
//{
//    Console.WriteLine(e.Current);
//}
//Console.WriteLine(e.Current);
/*
0   ：当我们第一次拿到迭代器时，当前的成员元素在第一个元素的前面一个，而0的前面在集合中是没有元素的，因此这里返回了当前集合类型（int）的默认值0，而后依次遍历集合中的所有元素
100
200
300
400
0   ：当我们遍历完集合，MoveNext() 方法返回 false 跳出 while 循环后再次访问 Current 属性，此时我们在集合最后一个元素的后面，而集合最后一个元素的后面是没有值的，因此这里同样返回的对应类型的默认值，由于迭代器的特性，当我们集合元素中也有0时，我们需要注意分清这个0是集合中的元素还是类型的默认值
*/

/*
引用类型的集合进行GetRange操作时发生了浅拷贝
（shadow clone），这里将栈内存中存储的堆内存
地址拷贝过去了，因此这里的seg集合第一个元素引
用的堆内存变量与bookList里第二个元素引用的堆
内存变量一样，因此我们通过seg去修改对应堆内存
变量中的对象，会导致引用着同样堆内存变量地址的
bookList也发生更改
*/
//var se2g = bookList.GetRange(1, 2);
//for (int i = 0; i < se2g.Count; i++)
//{
//    se2g[i].Price++;
//}

//Console.WriteLine(string.Join(",", se2g));
//Console.WriteLine(string.Join(",", bookList));


// 值类型的集合使用 GetRange 方法实际上是将需要的元素复制了一份，对截取后的集合进行修改并不会改变原集合中的值
// 因为值类型的集合在
//var seg = intList.GetRange(1, 2);
//for (int i = 0; i < seg.Count; i++)
//{
//    seg[i]++;
//}

//Console.WriteLine(string.Join(",", seg));
//Console.WriteLine(string.Join(",", intList));



//for (int i = 0; i < intList.Count; i++)
//{
//    intList[i]++;
//}

//Console.WriteLine(string.Join(",",intList));



//for (int i = 0; i < intList.Count; i++)
//{
//    if (intList[i] % 100 == 0)
//    {
//        intList.RemoveAt(i);
//        i--;    
//        /*
//        这里需要手动将i的值进行自减操作，因为当我们将索引为i上的元素去除时，会导致原集合中的所有元素往前移一个，
//        如果这里不进行自减操作，则会漏掉集合中某些元素导致无法对满足条件（被100整除）的所有元素进行移除
//        */
//    }
//}

//Console.WriteLine(string.Join(",", intList));



//for (int i = 0; i < intList.Count; i++)
//{
//    if (intList[i] % 100 == 0)
//    {
//        intList.Insert(i, intList[i] - 1);
//        i++;    // 如果这里不手动对 i 再进行一次自增操作，那么程序在这里会陷入死循环，
//                // 因为 intList 第一个元素是100，它可以对100进行整除，此时索引i的值为0，当我们在0的位置上插入 intList[i] - 1时
//                // 集合索引为0的位置上是99，而后i进行i++操作，i 变为1，而集合此时索引为1的位置上值为100，因为就会陷入死循环
//    }
//}

//Console.WriteLine(string.Join(",",intList));



// 索引器不仅可以读取集合中的元素数据，还可以修改集合中的元素数据
//intList[0] = 1000;
//Console.WriteLine(intList[0]);

//int[] array = new int[] { 100,200,300,400 };
//Console.WriteLine(array is IEnumerable<int>);
//List<int> list = new List<int>(array);
//List<int> list2 = new List<int>(list);
//Console.WriteLine($"{list2.Count}/{list2.Capacity}");
//Console.WriteLine(string.Join(",",list2));
//List<int> list = new List<int>(50);
//List<int> list = new List<int>();
//for (int i = 0; i < 100; i++)
//{
//    list.Add(i);
//    Console.WriteLine($"{list.Count}/{list.Capacity}");
//}
//Console.WriteLine(string.Join(",", list));

#endregion


#region 泛型List类（Part-1）

// 集合初始化器：底层是对集合 Add 方法的调用，因此若某一元素序列实现了 Add 方法即可使用数据初始化器进行初始化操作
//List<int> list1 = new List<int>() { 100, 200, 300, 400 };
//// 使用元素初始化器可以省略小括号
//List<int> list2 = new List<int> { 10, 20, 30, 40 };
//list2.AddRange(list1);
//// 在头部即最前端插入一个元素：此时需要把当前数组已有的元素全体往后移一格
//list2.Insert(0, 1000);
//list2.Insert(list2.Count, 1001);
//// list2.Count：模拟 Add 方法在元素序列的尾部插入数据；
//// list2.Count - 1：实际上是在元素序列倒数第二的位置上插入元素
//// 如何理解：跑步超过第二名实际上还是第二名，只有超过第一名才能成为第一名
//Console.WriteLine($"{list2.Count}/{list2.Capacity}");
//Console.WriteLine(string.Join(",", list2));

//List<int> list1 = new List<int>() { 100, 200, 300, 400 };
//List<int> list2 = new List<int> { 10, 20, 30, 40 };
//// 表示在Index为2的位置插入list1元素序列，实现的效果就是在20与30之间插入了list1集合
//list2.InsertRange(2, list1);
//Console.WriteLine($"{list2.Count}/{list2.Capacity}");
//Console.WriteLine(string.Join(",", list2));
//list2.Clear();
//// Clear() 方法会将集合元素个数置为零，但不会改变原数组的容量，这样以后如果增加元素就无须再重新开辟空间（在容量范围内）
//Console.WriteLine($"{list2.Count}/{list2.Capacity}");
//Console.WriteLine(string.Join(",", list2));

//List<int> list1 = new List<int>() { 100, 200, 300, 400 };
//List<int> list2 = new List<int> { 10, 20, 30, 40 };
//list2.AddRange(list1);
//list2.RemoveAt(0); // 去除第一个元素
//list2.RemoveAt(list2.Count - 1); // 去除最后一个元素
//list2.RemoveAt(list2.Count - 1);
//// 去除第一个元素和倒数第二个元素：留下第一个头部和最后一个尾部的元素
//list2.RemoveRange(1, list2.Count - 2);
//Console.WriteLine($"{list2.Count}/{list2.Capacity}");
//Console.WriteLine(string.Join(",", list2));

//List<int> list1 = new List<int>() { 100, 200, 300, 400 };
//List<int> list2 = new List<int> { 10, 20, 30, 400 };
//list2.AddRange(list1);
////list2.Remove(400);// list2 集合中有两个400，此操作会将第一个400移除
//list2.RemoveAll(e => e == 400); // 移除数组中的所有 400，它接受一个委托类型的参数
//Console.WriteLine($"{list2.Count}/{list2.Capacity}");
//Console.WriteLine(string.Join(",", list2));

#endregion
