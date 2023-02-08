namespace NewFeatureDemo;

public class Program
{
    static void Main(string[] args)
    {
        Student s1 = GetData();
        Console.WriteLine(s1.Name.ToLower());
        //Console.WriteLine(s1.PhoneNumber!.ToLower());// 使用 ! 可以消除‘可能为 null ’的警告

        Person p1 = new(1,"tim","cock");
        Person p2 = new(1,"tim","cock");
        Person p3 = new(1,"tim","Pan");
        Console.WriteLine(p1.ToString()); // Person { Id = 1, FirstName = tim, LastName = cock }
        Console.WriteLine(p2==p3); // False
        Console.WriteLine(p2==p1); // True
        Console.WriteLine(p2.Equals(p1)); // True
        Console.WriteLine(object.ReferenceEquals(p1,p2)); // False

        var p4 = p1 with { };// 创建了 p1 的副本，不是同一个对象，但是属性内容值完全一样：with 关键字相当于深拷贝
        Console.WriteLine(p4.ToString());
        Console.WriteLine(p4 == p1);// True
        Console.WriteLine(object.ReferenceEquals(p1, p4)); // False
        p4.Age = 25;
        Console.WriteLine(p4.ToString());
        Console.WriteLine(p4 == p1);// False

        var p5 = p3 with { FirstName = "NewJack", Age = 24};// 可以在大括号中修改想要更改的属性
        Console.WriteLine(p5.ToString());
    }

    static Student GetData()
    {
        //Student student = new("tim");
        //return student;
        return new("tim");
    }
}

/// <summary>
/// 新 record 类型：会自动重写 ToString、Equals、GetHashCode 等方法，本质上就是一个普通的类
/// </summary>
/// <param name="Id">默认属性都是 init：只读不写的，仅可在构造函数初始化时进行唯一的一次赋值</param>
/// <param name="FirstName"></param>
/// <param name="LastName"></param>
/// <param name="Age">如果需要可写可读的属性，那么可以在大括号中声明，还可以重载构造函数，并且不会因为这个可读可写的属性就在其他方法重写时忽略它，而是也会把它(们)包含其中</param>
public record Person(int Id, string FirstName, string LastName)
{
    public int Age { get; set; }

    public Person(int id,string firstName,string lastName, int age) : this(id, firstName, lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }
}

public class Student
{
    public string Name { get; set; }

    public string? PhoneNumber { get; set; }

    public Student(string name)
    {
        Name = name;
    }
}