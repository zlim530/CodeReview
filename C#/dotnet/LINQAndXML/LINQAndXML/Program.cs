using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LINQAndXML {
    class Program {
        /// <summary>
        /// LINQ
        /// </summary>
        /// <param name="args"></param>
        static void Main0(string[] args) {
            List<Car> myCars = new List<Car>() { 
                new Car(){ VIN = "A1",Make = "BMW",Model = "550i",StikerPrice = 55000,Year = 2009},
                new Car(){ VIN = "B2",Make = "Toyota",Model = "4Runner",StikerPrice = 35000,Year = 2010},
                new Car(){ VIN = "C3",Make = "BMW",Model = "74li",StikerPrice = 75000,Year = 2008},
                new Car(){ VIN = "D4",Make = "Ford",Model = "Escape",StikerPrice = 25000,Year = 2008},
                new Car(){ VIN = "E5",Make = "BMW",Model = "55i",StikerPrice = 57000,Year = 2010},
            };

            // LINQ query
            var bmws = from car in myCars
                       where car.Make == "BMW"
                       && car.Year == 2010
                       select car;

            var orderedCars = from car in myCars
                              orderby car.Year descending
                              select car;

            // LINQ method
            var bmw2s = myCars.Where(c => c.Make == "BMW" && c.Year == 2010);

            var orderedCar2s = myCars.OrderByDescending(c => c.Year);

            var firstBMW = myCars.OrderByDescending(c => c.Year).First();
            Console.WriteLine(firstBMW.VIN);

            Console.WriteLine(myCars.TrueForAll(c => c.Year > 2007));

            myCars.ForEach(c => c.StikerPrice -= 3000);// 使用 ForEach 会对原链表进行修改
            myCars.ForEach(c => Console.WriteLine("{0} {1:C}", c.VIN, c.StikerPrice));

            Console.WriteLine(myCars.Exists(c => c.Model == "74li"));
            Console.WriteLine(myCars.Sum(c => c.StikerPrice));

            foreach (var car in bmws)
            {
                Console.WriteLine($"{car.VIN} {car.Make} {car.Year}");
            }

            Console.WriteLine(myCars.GetType());
            var orderedCar3s = myCars.OrderByDescending(c => c.Year);
            Console.WriteLine(orderedCars.GetType());

            var bmw3s = myCars.Where(c => c.Make == "BMW" && c.Year == 2010);
            Console.WriteLine(bmws.GetType());

            var newCars = from car in myCars
                          where car.Make == "BMW"
                          // 匿名对象的初始化
                          select new { car.Make, car.Model };
            // System.Linq.Enumerable+WhereSelectListIterator`2[LINQAndXML.Car,<>f__AnonymousType0`2[System.String,System.String]]：匿名对象
            Console.WriteLine(newCars.GetType());
        }


        /// <summary>
        /// 子句
        /// </summary>
        /// <param name="args"></param>
        static void Main1(string[] args){
            /*int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var query = from n in arr
                        where IsEven(n)
                        select n;
            foreach (var item in query) {
                Console.WriteLine(item);
            }

            int[] numbers = { 1, 3, 5, 7, 9, 2, 4, 6, 8, 0 };

            var query = from num in numbers
                        let n = num % 2// 创建一个范围变量来存储结果
                        where n == 0
                        select num;
            foreach (var item in query) {
                Console.WriteLine(item);
            }

            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var query = from n in arr
                        where n > 1 && n < 6
                        orderby n descending
                        select n;

            foreach (var item in query) {
                Console.WriteLine(item);
            }

            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var query = from n in arr
                        where n > 1 && n < 6
                        group n by n % 2;

            foreach (var item in query) {
                foreach (var i in item) {
                    Console.WriteLine(i);
                }
                Console.WriteLine("========================");
            }

            int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var query = from n in arr
                        where n > 1 && n < 6
                        group n by n % 2 into lists// into：临时标识符，存储数据
                        from item in lists
                        select item;

            foreach (var item in query) {
                Console.WriteLine(item);
            }

            int[] arrA = {0,1,2,3,4,5,6,7,8,9 };
            int[] arrB = { 0,2,4,6,8};

            var query = from a in arrA
                        where a < 7
                        join b in arrB on a equals b// 将 arrA 和 arrB 数组进行行联接，同时满足 a，b 相同的条件，其中 b 是 arrB 中的元素
                        select a;
            foreach (var item in query) {
                Console.WriteLine(item);
            }*/

        }

        static bool IsEven(int n){
            if(n % 2 == 0){
                return true;
            }
            return false;
        }


        public static void Main2(string[] args) {
            /*int[] scores = new int[] { 97,92,81,60};
            //IEnumerable<int> 
            var scoreQuert = from score in scores
                             where score > 80
                             orderby score descending
                             select score;
            foreach (var item in scoreQuert) {
                Console.WriteLine(item);
            }*/


            string[] languages = { "Java","C#","C++","Delphi","VB.net","VC.net","C++Builder","Kylix","Perl","Python"};
            var query = from item in languages
                        group item by item.Length into lengthGroups
                        // 按照字符串长度进行排序，并保存在 lengthGroups 临时变量中
                        orderby lengthGroups.Key
                        select lengthGroups;

            foreach (var item in query) {
                Console.WriteLine($"string of length:{item.Key}");
                foreach (var str in item) {
                    Console.WriteLine(str);
                }
            }

        }


        public static void Main3(string[] args) {
            Dog dog = new Dog();
            Animal animal = dog;

            List<Dog> dogs = new List<Dog>();
            //List<Animal> animals = dogs; // 错误

            // out：协变：表示输出结果，也即返回值；in：逆变，表示输入参数；一个泛型参数标记为 out，代表它是用来输出的，作为结果返回
            // 如果有一个泛型参数标记为 in，代表它是用来输入的，只能作为参数
            List<Animal> animals2 = dogs.Select(d => (Animal)d).ToList();
            // T 隶属 IEnumerable<out T>
            IEnumerable<Dog> someDogs = new List<Dog>();
            IEnumerable<Animal> someAnimals = someDogs;// 强制转换合法

            //  delegate void System.Action<in T>（T obj）
            Action<Animal> actionAnimal = new Action<Animal>(a => { });
            Action<Dog> actionDog = actionAnimal;
        }
    }


    public abstract class Animal {

    }

    public class Dog:Animal {

    }



    class Car {
        public string VIN { get; set; }
        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public double StikerPrice { get; set; }
    }
}
