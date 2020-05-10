using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/**
 * @author zlim
 * @create 2020/5/10 0:51:37
 */
namespace CSharpSenior {
    public class FileFinder {

        static void Main0(string[] args) {

            var drivers = GetDrivers();
            var results = Concat(drivers.Select(OverDirectories).ToArray());
            Console.WriteLine("请输入要查找的文件名：");
            var search = Console.ReadLine().Trim();
            var keys = results.Keys.Where(p => p.Contains(search));

            foreach (var key in keys) {
                var list = results[key];
                Console.WriteLine("查找到的路径是：");
                foreach (var path in list) {
                    Console.WriteLine(path);
                }
            }
        }

        public static Dictionary<string, List<string>> Concat(params Dictionary<string,List<string>>[] dicts) {
            var dict = new Dictionary<string, List<string>>();
            foreach (var dir in dicts) {
                foreach (var key in dir.Keys) {
                    if (!dict.ContainsKey(key)) {
                        dict[key] = new List<string>();
                    }
                    dict[key].AddRange(dir[key]);
                }
            }
            return dict;
            
        }

        public static Dictionary<string, List<string>> OverDirectories(DirectoryInfo rootDirectory) {
            var dict = new Dictionary<string, List<string>>();
            IEnumerable<FileInfo> files = new List<FileInfo>();
            try {
                files = rootDirectory.EnumerateFiles();
            } catch (Exception e) {
                Console.WriteLine($"错误信息：{e}");
            }

            foreach (var file in files) {
                var key = Path.GetFileNameWithoutExtension(file.Name);
                if (!dict.ContainsKey(key)) {
                    dict[key] = new List<string>();
                }
                dict[key].Add(file.FullName);
            }

            try {
                var dicts = rootDirectory.EnumerateDirectories().Select(OverDirectories);
                return Concat(dicts.Append(dict).ToArray());
            } catch (Exception e) {
                Console.WriteLine($"错误信息：{e}");
            }

            //var dirs = rootDirectory.EnumerateDirectories().Select(OverDirectories);
            //foreach (var dir in dirs) {
            //    foreach (var key in dir.Keys) {
            //        if (!dict.ContainsKey(key)) {
            //            dict[key] = new List<string>();
            //        }
            //        dict[key].AddRange(dir[key]);
            //    }
            //}

            return dict;
        }

        public static List<DirectoryInfo> GetDrivers() {
            var drivers = DriveInfo.GetDrives();
            return drivers.Select(p => p.RootDirectory).ToList();
        }

        //public static void GetDrivers() {
        //    var drivers = DriveInfo.GetDrives();
        //    foreach (var driver in drivers) {
        //        Console.WriteLine($"驱动器名称：{driver.Name}:\t{driver.RootDirectory}");
        //    }
        //}
    }

    
}
