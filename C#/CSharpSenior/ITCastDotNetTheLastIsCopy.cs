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
        public static void Main00(string[] args) {
            Person person = new Person() { Name = "XXXX",Age = 1111,Email = "XXXX@hub.com",Bike = new Bike() { Name = "XXXX"} };
            // 需要注意的是，这里并没有复制 Person 对象本身，而是复制了对它的引用。如果你想要复制整个 Person 对象，需要使用相应的复制或克隆方法。这就是浅拷贝：浅拷贝是指只复制对象引用，而不复制对象本身的数据。
            Person perso2n = person;
            // 第二行代码将 person 变量的值赋给了 person2 变量。由于 person 变量持有的是一个引用，而不是实际的对象数据，因此赋值操作只是将 person2 变量也指向了同一个 Person 对象。
            // 这意味着对 person2 的任何更改都会反映在 person 变量和对应的 Person 对象中。
            //perso2n.Age = 25;
            //Console.WriteLine(person);

            //下面的代码实现对象的"浅拷贝"
            Console.WriteLine("下面的代码实现对象的\"浅拷贝\"");
            Person personOne = new Person() { Name = "Shallow copy", Age = 1111, Email = "shallowcopy@hub.com", Bike = new Bike() { Name = "shallow copy" } };
            Console.WriteLine($"浅拷贝之前的 personOne 对象：{personOne.ToString()}");
            //此时 personOne 和 PersonTwo 是两个不同的对象，在内存堆空间中有两个 Person 对象，所以此时已经发生了对象的浅拷贝
            //但对象 personOne 和 personTwo 均指向了同一个堆内存变量，这就是浅拷贝
            Person personTwo = new Person();
            personTwo.Name = personOne.Name;
            personTwo.Age = personOne.Age;
            personTwo.Email = personOne.Email;
            personTwo.Bike = personOne.Bike;
            Console.WriteLine($"浅拷贝产生的 personTwo 对象：{personTwo.ToString()}");
            Console.WriteLine(object.ReferenceEquals(personOne, personTwo));// False：本质是上两个栈局部变量引用了同一个堆内存对象
            Console.WriteLine("修改 personTwo 的 Bike 属性后，personOne 对象的值：");
            personTwo.Bike.Name = "Speed 500";// 由于两个 Person 对象均指向了同一个堆内存变量，因此这里对 personTwo 对象修改 Bike 属性，发现 personOne 相应的属性也发生了改变
            Console.WriteLine(personOne.ToString());


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
            Console.WriteLine(personThree);
            //如果你需要在两个变量中保留独立的 Person 对象，你可以使用深拷贝来创建一个完全独立的副本。深拷贝会递归地复制对象及其所有子对象的数据。可以使用序列化和反序列化或手动复制字段来实现深拷贝
            personFour.Bike = new Bike() { Name = "CT 100"};
            personFour.Bike.Name = "Four 400";
            personFour.Age = 25;
            Console.WriteLine(personThree);
        }


        /// <summary>
        /// 利用方法实现浅拷贝和深拷贝
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) {
            Person person = new Person() { Name = "XXXX", Age = 1111, Email = "XXXX@hub.com", Bike = new Bike() { Name = "XXXX" } };

            Person perso2n = person.ShallowCopy();
            //Person perso2n = person;
            var personName = person.Name;
            var perso2nName = perso2n.Name;

            var personEmail = person.Email;
            var perso2nEmail = perso2n.Email;

            var personBike = person.Bike;
            var perso2nBike = perso2n.Bike;

            // 无论是浅复制还是深复制，副本中的值类型都是全新的！
            // 因此就算我们修改了 perso2n 对象的 Age 属性，由于 Age 属性是 int 值类型，因此不会更改 person 对象中的 Age 属性，因此值类型没有引用，而是直接在栈内存空间中存储其值本身
            perso2n.Age = 24;
            Console.WriteLine(perso2n.Age == person.Age);// False

            // 浅复制中原始对象和副本的引用类型指向同一内存地址，所以，修改了 perso2n 的 Bike 会同时影响 person 的 Bike
            perso2n.Bike.Name = "Speed 500";
            Console.WriteLine(perso2n.Bike.Name == person.Bike.Name);// True

            Person perso3n = person.DeepCopy();
            var perso3nName = perso3n.Name;
            var perso3nEmail = perso3n.Email;
            var perso3nBike = perso3n.Bike;
            perso3n.Bike.Name = "CT 100";
            Console.WriteLine(perso3n.Bike.Name == person.Bike.Name);// False
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

        public override string ToString()
        {
            return $"The {this.Name} person is {this.Age} years old, its Email is {this.Email}, and its {this.Bike}";
        }
    }

    [Serializable]
    public class Bike {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Bike's Name is {this.Name}";
        }
    }
}
