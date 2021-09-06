using System;
using System.Collections.Generic;
using System.Linq;

/**
 * @author zlim
 * @create 2020/8/16 16:08:22
 */
namespace LINQMethod {
    public class LINK方法 {
        /// <summary>
        /// LINQ 方法1：Where 与自实现的 Mimic.Where
        /// </summary>
        /// <param name="args"></param>
        public static void Main0(string[] args) {
            Teacher fg = new Teacher { Name = "FenGe" };
            Teacher fish = new Teacher { Name = "Fish" };

            // Key：fg
            Major csharp = new Major { Name = "C#", Teacher = fg };
            Major SQL = new Major { Name = "SQL", Teacher = fg };
            Major javascript = new Major { Name = "javascript", Teacher = fg };

            // Key：fish
            Major UI = new Major { Name = "UI", Teacher = fish };

            IList<Student> students = new List<Student> {
                new Student{ Score = 98,Name = "Tim",Majors = new List<Major>{ csharp, SQL } },
                new Student{ Score = 89,Name = "Tom",Majors = new List<Major>{ csharp, SQL, javascript } },
                new Student{ Score = 86,Name = "Jack",Majors = new List<Major>{ csharp} },
                new Student{ Score = 78,Name = "Whatever",Majors = new List<Major>{ csharp, javascript } },
                new Student{ Score = 90,Name = "Whocares",Majors = new List<Major>{ csharp, SQL , javascript ,UI} },
            };

            IEnumerable<Major> majors = new List<Major> { csharp, SQL, javascript, UI };

            var excellent = students.MimicWhere(s => s.Score > 85);
            foreach (var item in excellent) {
                Console.WriteLine(item.Name);
            }


        }


        /// <summary>
        /// LINQ 方法2：GroupBy 与 Join 方法
        /// </summary>
        /// <param name="args"></param>
        public static void Main1(string[] args) {
            Teacher fg = new Teacher { Name = "FenGe" ,Id = 1};
            Teacher fish = new Teacher { Name = "Fish" ,Id = 2};
            IEnumerable<Teacher> teachers = new List<Teacher>() { fg,fish};

            Major csharp = new Major { Name = "C#", TeacherId = 1 };
            Major SQL = new Major { Name = "SQL", TeacherId = 1 };
            Major javascript = new Major { Name = "javascript", TeacherId = 1 };
            Major UI = new Major { Name = "UI", TeacherId = 2 };
            IEnumerable<Major> majors = new List<Major> { csharp, SQL, javascript, UI };

            IList<Student> students = new List<Student> {
                new Student{ Score = 98,Name = "Tim",Majors = new List<Major>{ csharp, SQL } },
                new Student{ Score = 89,Name = "Tom",Majors = new List<Major>{ csharp, SQL, javascript } },
                new Student{ Score = 86,Name = "Jack",Majors = new List<Major>{ csharp} },
                new Student{ Score = 78,Name = "Whatever",Majors = new List<Major>{ csharp, javascript } },
                new Student{ Score = 90,Name = "Whocares",Majors = new List<Major>{ csharp, SQL , javascript ,UI} },
            };

            var groupedMajor = majors.GroupBy(m => m.TeacherId).Select(gr => new { 
                gr.Key,
                // Count()：返回序列中的元素数量
                Count = gr.Count()
            });
            foreach (var item in groupedMajor) {
                Console.WriteLine($"{item.Key}老师负责了{item.Count}门课程");
            }

            // Join() 方法：第一个参数是待联接的另一个集合，第二个参数是当前集合的哪个元素与另一个集合进行联接，
            // 第三个参数是另一个集合中需要联接的元素，第四个参数是需要返回的元素
            var joinedLists = majors.Join(teachers,m => m.TeacherId,t => t.Id,(m,t) => new { m,t});
            foreach (var item in joinedLists) {
                Console.WriteLine($"major.Name = {item.m.Name},major.TeacherId = {item.m.TeacherId}," +
                    $"teacher.Name = {item.t.Name},teacher.Id = {item.t.Id}");
            }

            var joinedList1s = majors.Join(teachers, m => m.TeacherId, t => t.Id, (m, t) => new { 
                MName = m.Name,
                NTId = m.TeacherId,
                TName = t.Name,
                TId = t.Id
            });
            foreach (var item in joinedList1s) {
                Console.WriteLine($"{item.TName}老师负责了{item.MName}课程");
            }

        }


        /// <summary>
        /// LINQ 方法3：SelectMany
        /// </summary>
        /// <param name="args"></param>
        public static void Main2(string[] args) {
            Teacher fg = new Teacher { Name = "FenGe", Id = 1 };
            Teacher fish = new Teacher { Name = "Fish", Id = 2 };
            IEnumerable<Teacher> teachers = new List<Teacher>() { fg, fish };

            Major csharp = new Major { Name = "C#", TeacherId = 1 };
            Major SQL = new Major { Name = "SQL", TeacherId = 1 };
            Major javascript = new Major { Name = "javascript", TeacherId = 1 };
            Major UI = new Major { Name = "UI", TeacherId = 2 };
            IEnumerable<Major> majors = new List<Major> { csharp, SQL, javascript, UI };

            IList<Student> students = new List<Student> {
                new Student{ Score = 98,Name = "Tim",Majors = new List<Major>{ csharp, SQL } },
                new Student{ Score = 89,Name = "Tom",Majors = new List<Major>{ csharp, SQL, javascript } },
                new Student{ Score = 86,Name = "Jack",Majors = new List<Major>{ csharp} },
                new Student{ Score = 78,Name = "Whatever",Majors = new List<Major>{ csharp, javascript } },
                new Student{ Score = 90,Name = "Whocares",Majors = new List<Major>{ csharp, SQL , javascript ,UI} },
            };

            var selectMany = students.SelectMany(s => s.Majors,(s,m) => new { s,m});
            foreach (var item in selectMany) {
                //Console.WriteLine($"{item.s.Name}同学学习了{item.m.Name}课程");
            }

            // 找出课程名称中包含 s 的同学
            var selectMan1y = students.SelectMany(s => s.Majors,(s,m) => new { s,m}).Where(s => s.m.Name.Contains("s"));
            foreach (var item in selectMan1y) {
                Console.WriteLine($"{item.s.Name},{item.m.Name}");
            }
            /*
            延迟(deferred)执行：LINQ 的查询表达式/方法并不进行查询，它只是一个命令，直到被以下方法调用：
                foreach、ToList、ToArray、First、Single、SingleOrDefault
                Sum、Count、Min、Max、Average
                Reverse
            这样做的好处：
                ·提高灵活性，进行多种条件查询的拼接转换
                ·提高性能，使用最终表达式进行查询
            */

        }




    }

    /// <summary>
    /// 自实现一个 MimicWhere 已实现与 Where 方法一样的效果
    /// </summary>
    public static class Mimic {
        /// <summary>
        /// 将返回值设置为 IEnumerable<T> 使其适用性最广
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> MimicWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate) {
            List<T> result = new List<T>();

            foreach (var item in source) {
                if (predicate(item)) {
                    result.Add(item);
                }
            }
            
            return result;
        }
    }


    public class Teacher {
        public string Name { get; set; }

        public int Id { get; set; }
    }

    public class Major {
        public string Name { get; set; }

        public Teacher Teacher { get; set; }

        public int TeacherId { get; set; }
    }

    public class Student {
        public int Score { get; set; }

        public string Name { get; set; }

        public List<Major> Majors { get; set; }
    }
}
