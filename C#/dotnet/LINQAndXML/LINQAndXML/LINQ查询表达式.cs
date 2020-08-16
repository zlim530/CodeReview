using System;
using System.Collections.Generic;
using System.Linq;

/**
 * @author zlim
 * @create 2020/8/12 18:33:09
 */
namespace LINQAndXML {
    public class LINQ查询表达式 {
        public static void Main0(string[] args) {
            Teacher fg = new Teacher { Name = "FenGe" };
            Teacher fish = new Teacher { Name = "Fish"};

            // Key：fg
            Major csharp = new Major { Name = "C#",Teacher = fg};
            Major SQL = new Major { Name = "SQL",Teacher = fg};
            Major javascript = new Major { Name = "javascript",Teacher = fg};

            // Key：fish
            Major UI = new Major { Name = "UI",Teacher = fish};

            IList<Student> students = new List<Student> {
                new Student{ Score = 98,Name = "Tim",Majors = new List<Major>{ csharp, SQL } },
                new Student{ Score = 89,Name = "Tom",Majors = new List<Major>{ csharp, SQL, javascript } },
                new Student{ Score = 86,Name = "Jack",Majors = new List<Major>{ csharp} },
                new Student{ Score = 78,Name = "Whatever",Majors = new List<Major>{ csharp, javascript } },
                new Student{ Score = 90,Name = "Whocares",Majors = new List<Major>{ csharp, SQL , javascript ,UI} },
            };

            IList<Major> majors = new List<Major> { csharp, SQL, javascript, UI };

            // group 对 ... 进行分组，其中分组后得到的对象的 key 就是分组依据，也即 by 关键字后的对象
            //var groupExample = from m in majors group m by m.Teacher;
            // interface System.Linq.IGrouping<out TKey,out TElemnt> 表示具有公共键的对象的集合
            IEnumerable<IGrouping<Teacher,Major>> groupExample = from m in majors
                               group m by m.Teacher;

            /*
            key     Item=ELement
            fg      C#
                    javascript
                    SQL
            --------------------
            fish    UI
            */
            foreach (var groupItem in groupExample) {
                //LINQAndXML.Teacher : FenGe
                //LINQAndXML.Teacher : Fish
                Console.WriteLine($"{groupItem.Key.GetType()} : {groupItem.Key.Name}");
                foreach (var item in groupItem) {
                    Console.WriteLine($"{item.GetType()} : {item.Name}");
                    // LINQAndXML.Major : C#
                }
                Console.WriteLine();
            }

            // ==============================================================================
            var major = from m in new List<Major> { csharp, SQL, javascript, UI }
                        join t in new List<Teacher> { fg, fish }
                        on m.Teacher equals t
                        where t.Name == "FenGe"
                        // 可以被一句替代：where m.Teacher.Name == "FenGe"
                        select m;

            foreach (var item in major) {
                Console.WriteLine($"{item.Teacher} : {item.Name}");
                //LINQAndXML.Teacher : C#
                //LINQAndXML.Teacher ：SQL
                //LINQAndXML.Teacher : javascript
            }

            // ==============================================================================

            /*
            Teacher:Name        Major:      Name        Teacher
            fg     :FenGe       csharp      C#          fg
            fish   :Fish        SQL         SQL         fg
            noone  :Noone       javascript  javascript  fg
                                UI          UI          fish
            t join m 即 t left join m：因此 t 序列中的所有元素都会显示出来
            */
            Teacher noone = new Teacher { Name = "Noone"};
            var infos = from t in new List<Teacher> { fg, fish, noone }
                         join m in new List<Major> { csharp, SQL, javascript, UI }
                         on t equals m.Teacher into mt
                         from result in mt.DefaultIfEmpty()// 返回指定序列汇总的元素：如果序列为空，则返回单一实例集合中的类型参数的默认值
                         select new { Teacher = t.Name,major = result?.Name ?? "没有课程"};
            Console.WriteLine();
            foreach (var info in infos) {
                Console.WriteLine($"{info.Teacher} : {info.major}");
                //FenGe ：C#
                //FenGe : SQL
                //FenGe : javascript
                //Fish : UI
                //Noone : 没有课程
            }

            // ==============================================================================

            /*
            Teacher:Name        Major:      Name        Teacher
            fg     :FenGe       csharp      C#          fg
            fish   :Fish        SQL         SQL         fg
            noone  :Noone       javascript  javascript  fg
                                UI          UI          fish
                                java        Java        NULL
            m join j 即 m left join t：因此 m 序列中的所有元素都会显示出来
            */
            Major java = new Major { Name = "Java"};
            var info1s = from m in new List<Major> { csharp, javascript, SQL, java }
                         join t in new List<Teacher> { fg, fish, noone }
                         on m.Teacher equals t into mt
                         from result in mt.DefaultIfEmpty()
                         select new { major = m.Name, teacher = result?.Name ?? "NULL" };
            Console.WriteLine();
            foreach (var info in info1s) {
                Console.WriteLine($"{info.teacher} : {info.major}");
                /*
                FenGe : C#
                FenGe : javascript
                FenGe : SQL
                NULL : Java
                */
            }

            // ==============================================================================
            // 全连接：也叫笛卡尔集，没什么意义，不常用
            var info2s = from t in new List<Teacher> { fg, fish, noone }
                         from m in new List<Major> { csharp, javascript, SQL, java }
                         select new { teacher = t.Name, major = m.Name };

            Console.WriteLine();
            foreach (var info in info2s) {
                Console.WriteLine($"{info.teacher} : {info.major}");
                /*
                FenGe : C#
                FenGe : javascript
                FenGe : SQL
                FenGe : Java
                Fish : C#
                Fish : javascript
                Fish : SQL
                Fish : Java
                Noone : C#
                Noone : javascript
                Noone : SQL
                Noone : Java
                */
            }

            // ==============================================================================
            /*
            多个字段:组成的连接条件：使用匿名类进行比较
             //internal Teacher Teacher { get; set; }  将Teacher类拆分成Name和Age
            internal string TeacherName { get; set; }
            internal int TeacherAge { get; set; }


            //集合内容相应的改变
            Teacher fg = new Teacher { Name = "大飞哥", Age = 38 };
            Teacher fish = new Teacher { Name = "小鱼", Age = 21 };

            Major csharp = new Major { Name = "C#", TeacherName = "大飞哥", TeacherAge = 38 };
            Major SQL = new Major { Name = "SQL", TeacherName = "大飞哥", TeacherAge = 23 };
            Major Javascript = new Major { Name = "Javascript", TeacherName = "大飞哥", TeacherAge = 38 };
            Major UI = new Major { Name = "UI", TeacherName = "小鱼", TeacherAge = 21 };

            var majors = from t in new List<Teacher> { fg, fish }
                         join m in new List<Major> { csharp, SQL, Javascript, UI }
                         //使用组合关键字的匿名类进行比较
                         on new { name = t.Name, age = t.Age } equals new { name = m.TeacherName, age = m.TeacherAge }
                         select new { teacher = t.Name, major = m.Name };
            */

            // ==============================================================================

            var info3s = from t in new List<Teacher> { fg, fish } // 一对多关系里面的“一”放在最前面
                         join m in new List<Major> { csharp, javascript, SQL,UI }
                         on t equals m.Teacher into mt            // 注意这个：into
                         select new { teacher = t, major = mt };  // 需要自己在结果集中指定 key 和 value

            Console.WriteLine();
            foreach (var info in info3s) {
                Console.WriteLine($"{info.teacher.Name} : {info.major.DefaultIfEmpty()}");
                /*
                FenGe : System.Linq.Enumerable+DefaultIfEmptyIterator`1[LINQAndXML.Major]
                Fish : System.Linq.Enumerable+DefaultIfEmptyIterator`1[LINQAndXML.Major]
                */
            }

            // ==============================================================================

            var info4s = from s in students
                         let sm = s.Majors // 把所有的 Major 先暴露出来
                         from m in sm      // 后面就可以使用 let 指定的 sm
                         select new { student = s.Name, major = m };

            Console.WriteLine();
            foreach (var info in info4s) {
                Console.WriteLine($"{info.student} : {info.major.Teacher.Name} {info.major.Name}");
            }

            // ==============================================================================




        }
    }

    public class Teacher {
        public string Name { get; set; }
    }

    public class Major {
        public string Name { get; set; }

        public Teacher Teacher { get; set; }
    }

    public class Student {
        public int Score { get; set; }

        public string Name { get; set; }

        public List<Major> Majors { get; set; }
    }
}
