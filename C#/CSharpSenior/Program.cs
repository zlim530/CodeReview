using System;
using System.Text.RegularExpressions;

namespace CSharpSenior
{
    class Program
    {
        static void Main(string[] args)
        {
            // 正则表达式
            string s = "I am blue cat.";
            string res = Regex.Replace(s,"^","Start:");
            Console.WriteLine(res);// Start:I am blue cat.
            res = Regex.Replace(s,"$","End");
            System.Console.WriteLine(res);// Start:I am blue cat.End

            string s = Console.ReadLine();
            string pattern = @"^\d*$";// * 0个或多个：即任意个
            bool isMatch = Regex.IsMatch(s,pattern);
            System.Console.WriteLine(isMatch);

            string s = "213 *(&(*^((*十大绝对是";
            pattern = @"\d|[a-z]";
            MatchCollection col = Regex.Matches(s,pattern);
            foreach (Match match in col)
            {
                System.Console.WriteLine(match);// 相当于调用match的ToString()方法,会输出match所匹配到的字符串
            }

            



        }


// =====================控制流===========================

        // flow of control
        static void Main1(string[] args){
            string input = Console.ReadLine();
            try
            {
                double score = double.Parse(input);
                if( score >= 60 ){
                    System.Console.WriteLine("Pass!");
                } else
                {
                    System.Console.WriteLine("Failed!");
                }
            }
            /* catch (System.Exception) */
            catch
            {
                System.Console.WriteLine("Not A Number");
            }
        }

// =====================switch-case语句块===========================

        static void Exer(){
            int score = 101;
            switch (score / 10)
            {
                case 10:
                    if (score == 100)
                    {
                        goto case 8;
                    }else
                    {
                        goto default;
                    }
                // 只有单独的标签才能连起来写
                case 8:
                case 9:
                // 一旦有了具体的Section，就必须配套break
                    System.Console.WriteLine("A");
                    break;
                default:
            }
        }

// ====================实例字段与静态字段============================
        
        static void Main2(string[] args){
            var stuList = new List<Student>();
            for (int i = 0; i < 100; i++)
            {
                var stu = new Student{
                    Age = 21;
                    Score = i;
                };// 实例初始化器
                stuList.Add(stu);
            }
            
            int totalAge = 0;
            int totalScore = 0;
            foreach (var stu in stuList)
            {
                totalAge += stu.Age;
                totalScore += stu.Score;
            }

            Student.AverageAge = totalAge / Student.Amount;
            Student.AverageScore = totalScore / Student.Amount;

            Student.ReportAmount();
            Student.ReportAverageAge();
            Student.ReportAverageScore();

        }

        class Student
        {
            public int Age;
            public int Score;

            public static int AverageAge;
            public static int AverageScore;
            public static int Amount;


            public Student()
            {
                Amount++;
            }

            public static void ReportAmount(){
                System.Console.WriteLine(Amount);
            }

            public static void ReportAverageAge(){
                System.Console.WriteLine(AverageAge);
            }

            public static void ReportAverageScore(){
                System.Console.WriteLine(AverageScore);
            }

        }

// ======================Dictionary<TKey,TValue>字典 的基本使用==========================

    // Dictionary<TKey,TValue>字典 的基本使用
    static void Main3(string[] args){

        // 创建Dictionary<TKey,TValue> 对象
        Dictionary<string,string> openWith = new Dictionary<string,string>();

        // 添加元素到字典对象中，共有两种方法。注意：字典中的键不可以重复，但是值可以重复。
        // 方法一：使用字典对象的Add()方法
        openWith.Add("txt","notepad.ext");
        openWith.Add("bmp","paint.exe");
        openWith.Add("dib","paint.exe");
        openWith.Add("rtf","wordpad.exe");

        // 方法二：使用字典对象的索引器Indexr
        openWith["txt"] = "notepad.exe";
        openWith["bmp"] = "paint.exe";
        openWith["dib"] = "paint.exe";
        openWith["rtf"] = "wordpad.exe";

        // 增减元素，注意增加前必须检查要增加的键是否存在
        // 使用ContainsKey()方法
        if (!openWith.ContainsKey("ht"))
        {
            openWith.Add("ht","hypertrm.exe");
            System.Console.WriteLine("增加元素成功!Key={0},Valeu={1}","ht",openWith["ht"]);

        }

        // 删除元素：使用Remove()方法
        if (openWith.ContainsKey("rtf"))
        {
            openWith.Remove("rtf");
            System.Console.WriteLine("删除元素成功！键为rtf");
        }

        if (!openWith.ContainsKey("rft"))
        {
            System.Console.WriteLine("Key = \"rtf\"的元素找不到！");
        }

        // 修改元素，使用索引器
        if ( openWith.ContainsKey("txt"))
        {
            openWith["txt"] = "notepadUpdate.exe";
            System.Console.WriteLine("修改元素成功！Key={0},Value={1}","txt",openWith["txt"]);
        }

        // 遍历元素，因为该类实现了IEnumerable接口，所以可以使用foreach语句，注意字典对象中每个元素的类型为 KeyValuePair<TKey, TValue> 类型
        foreach (KeyValuePair<string,string> kvp in openWith)
        {
            System.Console.WriteLine("Key={0},Value={1}",kvp.Key,kvp.Valeu);
        }
        System.Console.WriteLine("遍历元素完成！");
        Console.ReadLine();
    }


// ======================List<T>列表 的基本使用==========================

    static void Main4(string[] args){

        // 创建List<T>列表对象
        List<string> dinosaurs = new List<string>();

        // 添加元素到列表(或称为初始化)，注意初始化时不能使用索引器进行赋值，因为没有增加元素之前list列表是空的
        dinosaurs.Add("Tyrannosaurs");
        dinosaurs.Add("Amargasaurs");
        dinosaurs.Add("Mamenchisaurs");
        dinosaurs.Add("Deinonychus");
        dinosaurs.Add("Comsognathus");

        // 一个重要的属性：Count
        System.Console.WriteLine("列表中的元素数为：{0}",dinosaurs.Count);

        // 在指定的位置插入元素，使用Insert()方法
        dinosaurs.Insert(2,"Compsognathus");// 将元素插入到指定索引处，原来此位置的元素后移
        System.Console.WriteLine("在索引为2的位置插入了元素{0}",dinosaurs[2]);

        // 删除元素，使用Remove()方法
        dinosaurs.Remove("Compsognathus");
        System.Console.WriteLine("删除第一个名为Compsognathus的元素！");

        // 修改元素，使用索引器
        dinosaurs[0] = "TyrannsaurusUpdate";
        System.Console.WriteLine("修改索引为0的元素成功！");
        
        // 遍历元素，使用foreach语句，元素类型为string
        foreach (string dinosaur in dinosaurs)
        {
            System.Console.WriteLine(dinosaur);
        }

        System.Console.WriteLine("遍历列表完成！");
        Console.ReadLine();

    }




    }
}
