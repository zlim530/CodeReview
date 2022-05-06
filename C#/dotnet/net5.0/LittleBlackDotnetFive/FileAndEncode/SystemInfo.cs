using System;
using System.IO;
using System.Text;

namespace FileAndEncode
{
    public static class SystemInfo
    {
        public static void FileSystemInfo()
        {
            Console.WriteLine($"PathPathSeparator 指的是分隔连续多个路径字符串的分隔符：{Path.PathSeparator}");
            // ;
            Console.WriteLine($"PathDirectorySeparatorChar 指的是分隔同一个路径字符串中的目录的分隔符：{Path.DirectorySeparatorChar}");
            // \
            Console.WriteLine($"PathGetTempPath() 指的是当前系统的临时目录：{Path.GetTempPath()}");
            // C:\Users\Lim\AppData\Local\Temp\ => 不同的操作系统不一样
            Console.WriteLine($"程序当前目录：{Environment.CurrentDirectory}");
            // 使用不同的代码编辑器编译会产生不同的结果
            //Console.WriteLine($"{Environment.CurrentDirectory}");
            // C:\Users\Lim\Desktop\code\mostnewCodeReview\CodeReview\C#\dotnet\net5.0\LittleBlackDotnetFive\FileAndEncode => fluent terminal -> dotnet run 
            Console.WriteLine($"系统当前目录：{Environment.SystemDirectory}");
            // C:\WINDOWS\system32
        }
    
        public static void WorkWithDrives()
        {
            Console.WriteLine("{0, -20} | {1, -15} | {2, -10} | {3, 29} | {4, 20}","NAME", "TYPE", "FORMAT", "SIZE (BYTES)", "FREE SPACE");
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    Console.WriteLine("{0,-20} | {1,-15} | {2,-10} | {3,29:N1} | {4,20:N1}", drive.Name, drive.DriveType, drive.DriveFormat, drive.TotalSize, drive.AvailableFreeSpace);
                }
                else
                {
                    Console.WriteLine("{0,-20} | {1,-15}", drive.Name, drive.DriveType);
                }
            }
        }
    
        public static void WorkWithDirectories()
        {
            string newDir = Path.Combine(Path.GetTempPath(), "Code", "Two", "NewDir");
            Console.WriteLine($"New Folder:{newDir}");
            Console.WriteLine($"Exists?{Directory.Exists(newDir)}");
            Console.WriteLine("Create Dir");
            Directory.CreateDirectory(newDir);
            Console.WriteLine($"Exists?{Directory.Exists(newDir)}");
            Console.WriteLine("Delete Dir");
            Directory.Delete(newDir);
            Console.WriteLine($"Exists?{Directory.Exists(newDir)}");
        }

        public static void WorkWithFiles()
        {
            string newDir = Path.Combine(Path.GetTempPath(), "Code", "Two", "NewDir");
            Directory.CreateDirectory(newDir);
            string textFile = Path.Combine(newDir, "test.txt");
            Console.WriteLine($"Exists?{File.Exists(textFile)}");
            //1.使用 File 
            //File.WriteAllText(textFile,"fffff");
            //byte[] text = Encoding.UTF8.GetBytes("你好，世界！");
            //File.WriteAllBytes(textFile,text);
            //File.AppendAllText(textFile, "24");
            //2.使用 Stream => 推荐
            StreamWriter textWriter = File.CreateText(textFile);
            textWriter.WriteLine("原子！");
            textWriter.Close();
            Console.WriteLine($"Exists?{File.Exists(textFile)}");
            // 读取
            StreamReader textReader = File.OpenText(textFile);
            Console.WriteLine(textReader.ReadToEnd());
            textReader.Close();
            // 备份文件
            string bakFile = Path.Combine(newDir, "test.txt.bak");
            File.Copy(textFile, bakFile, true);

            // 删除
            File.Delete(textFile);
            Console.WriteLine($"Is Clear? {!File.Exists(textFile)}");
            Console.WriteLine($"Is Bak Exists? {File.Exists(bakFile)}");
        }
    }
}
