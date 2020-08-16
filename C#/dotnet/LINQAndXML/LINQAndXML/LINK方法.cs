using System;
using System.Collections.Generic;

/**
 * @author zlim
 * @create 2020/8/16 16:08:22
 */
namespace LINQMethod {
    public class LINK方法 {
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
