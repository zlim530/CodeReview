using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayAndDictionaryOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] myIntArrar = new int[] { 1,2,3,4,5};
            // 注意这里的{}不是构造器而是初始化器
            Console.WriteLine(myIntArrar[0]);
            Console.WriteLine(myIntArrar[myIntArrar.Length-1]);
            // 对于数组来说 元素访问操作符一定中一定是整型数字 但并不是所有的元素访问下标都为整型
            // 例如 索引字典中的元素：
            Dictionary<string, Student> stuDic = new Dictionary<string, Student>();

            for (int i = 1; i <= 100; i++)
            {
                Student stu = new Student();
                stu.Name = "s_" + i.ToString();
                stu.Score = 100 + i;
                stuDic.Add(stu.Name,stu);
            }

            Student number6 = stuDic["s_6"];
            Console.WriteLine(number6.Score);
            
        }

    }

    class Student
    {
        public string Name;
        public int Score;
    }
}
