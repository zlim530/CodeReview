namespace NewFeatureDemo;

public class Program
{
    static void Main(string[] args)
    {
        Student s1 = GetData();
        Console.WriteLine(s1.Name.ToLower());
        //Console.WriteLine(s1.PhoneNumber!.ToLower());// ʹ�� ! ��������������Ϊ null ���ľ���

        Person p1 = new(1,"tim","cock");
        Person p2 = new(1,"tim","cock");
        Person p3 = new(1,"tim","Pan");
        Console.WriteLine(p1.ToString()); // Person { Id = 1, FirstName = tim, LastName = cock }
        Console.WriteLine(p2==p3); // False
        Console.WriteLine(p2==p1); // True
        Console.WriteLine(p2.Equals(p1)); // True
        Console.WriteLine(object.ReferenceEquals(p1,p2)); // False

        var p4 = p1 with { };// ������ p1 �ĸ���������ͬһ�����󣬵�����������ֵ��ȫһ����with �ؼ����൱�����
        Console.WriteLine(p4.ToString());
        Console.WriteLine(p4 == p1);// True
        Console.WriteLine(object.ReferenceEquals(p1, p4)); // False
        p4.Age = 25;
        Console.WriteLine(p4.ToString());
        Console.WriteLine(p4 == p1);// False

        var p5 = p3 with { FirstName = "NewJack", Age = 24};// �����ڴ��������޸���Ҫ���ĵ�����
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
/// �� record ���ͣ����Զ���д ToString��Equals��GetHashCode �ȷ����������Ͼ���һ����ͨ����
/// </summary>
/// <param name="Id">Ĭ�����Զ��� init��ֻ����д�ģ������ڹ��캯����ʼ��ʱ����Ψһ��һ�θ�ֵ</param>
/// <param name="FirstName"></param>
/// <param name="LastName"></param>
/// <param name="Age">�����Ҫ��д�ɶ������ԣ���ô�����ڴ����������������������ع��캯�������Ҳ�����Ϊ����ɶ���д�����Ծ�������������дʱ������������Ҳ�����(��)��������</param>
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