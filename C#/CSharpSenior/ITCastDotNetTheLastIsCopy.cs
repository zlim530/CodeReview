using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/**
 * @author zlim
 * @create 2021/1/9 17:16:22
 */
namespace 浅拷贝与深拷贝
{
    public class ITCastDotNetTheLastIsCopy {
        /// <summary>
        /// 浅拷贝与深拷贝的定义
        /// </summary>
        /// <param name="args"></param>
        public static void Main0(string[] args) {
            Person person = new Person() { Name = "XXXX",Age = 1111,Email = "XXXX@hub.com",Bike = new Bike() { Name = "XXXX"} };
            // 这句话没有发生任何对象拷贝，因为内存堆空间中自始至终都只有一个 Person 对象
            Person perso2n = person;

            //下面的代码实现对象的"浅拷贝"
            Console.WriteLine("下面的代码实现对象的\"浅拷贝\"");
            Person personOne = new Person() { Name = "XXXX", Age = 1111, Email = "XXXX@hub.com", Bike = new Bike() { Name = "XXXX" } };
            //此时 personOne 和 PersonTwo 是两个不同的对象，在内存堆空间中有两个 Person 对象，所以此时已经发生了对象的浅拷贝
            //但对象 personOne 和 personTwo 均指向了同一个堆内存变量，这就是浅拷贝
            Person personTwo = new Person();
            personTwo.Name = personOne.Name;
            personTwo.Age = personOne.Age;
            personTwo.Email = personOne.Email;
            personTwo.Bike = personOne.Bike;

            //下面的代码实现对象的"深拷贝"
            Console.WriteLine("下面的代码实现对象的\"深拷贝\"");
            Person personThree = new Person() { Name = "XXXX", Age = 1111, Email = "XXXX@hub.com", Bike = new Bike() { Name = "XXXX" } };
            Person personFour = new Person();
            personFour.Name = personThree.Name;
            personFour.Age = personThree.Age;
            personFour.Email = personThree.Email;
            //此时 personThree 和 personFour 是两个不同的对象，在内存堆空间中有两个 Person 对象
            //这两个 Person 对象中的引用类型成员 Bike 分别指向了两个不同的 Bike 对象
            //此时发生了对象的深拷贝
            personTwo.Bike = new Bike() { Name = "XXXXX"};

        }


        /// <summary>
        /// 利用方法实现浅拷贝和深拷贝
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) {
            Person person = new Person() { Name = "XXXX", Age = 1111, Email = "XXXX@hub.com", Bike = new Bike() { Name = "XXXX" } };

            Person perso2n = person.ShallowCopy();
            var personName = person.Name;
            var perso2nName = perso2n.Name;

            var personEmail = person.Email;
            var perso2nEmail = perso2n.Email;

            var personBike = person.Bike;
            var perso2nBike = perso2n.Bike;

            Person perso3n = person.DeepCopy();
            var perso3nName = perso3n.Name;
            var perso3nEmail = perso3n.Email;
            var perso3nBike = perso3n.Bike;

        }
    }

    [Serializable]
    public class Person {
        public string Name { get; set; }

        public int Age { get; set; }
        
        public string Email { get; set; }
        
        public Bike Bike { get; set; }

        /// <summary>
        /// 通过 MemberwiseClone 方法实现当前对象的浅拷贝
        /// </summary>
        /// <returns></returns>
        // 一句话实现浅拷贝，MemberwiseClone 是 Object 类中 protected 的一个方法，表示创建当前 object 的浅表副本
        // 而后再使用 as 操作符将 object 对象转换为 Person 对象
        public Person ShallowCopy() => this.MemberwiseClone() as Person;

        /// <summary>
        /// 通过序列化与反序列化实现当前对象的深拷贝
        /// </summary>
        /// <returns></returns>
        public Person DeepCopy() {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream()) {
                //序列化成流
                bf.Serialize(ms,this);
                ms.Position = 0;
                //反序列化成对象再返回
                return bf.Deserialize(ms) as Person;
            }
        }
    }

    [Serializable]
    public class Bike {
        public string Name { get; set; }
    }
}
