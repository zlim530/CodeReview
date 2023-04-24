using TelePrompterConsole;

namespace CodeCheck
{
    internal class Program
    {
        /// <summary>
        /// 探索字符串内插值
        /// </summary>
        /// <param name="args"></param>
        static void Main00(string[] args)
        {
            // 定义了具有 Name、Price 和 perPackage 成员的 tuple
            var item = (Name: "eggplant", Price: 1.99m, perPackage: 3);
            var date = DateTime.Now;
            // 可通过在内插表达式后接冒号（“:”）和格式字符串来指定格式字符串。 “d”是标准日期和时间格式字符串，表示短日期格式。 “C2”是标准数值格式字符串，用数字表示货币值（精确到小数点后两位）。
            Console.WriteLine($"On {date:d}, the price of {item.Name} was {item.Price:C2} per {item.perPackage} items");
            // 将 {date:d} 中的“d”更改为“t”（显示短时间格式）、“y”（显示年份和月份）和“yyyy”（显示四位数年份）。 将 {price:C2} 中的“C2”更改为“e”（用于指数计数法）和“F3”（使数值在小数点后保持三位数字）。
            Console.WriteLine($"On {date:t}, the price of {item.Name} was {item.Price:F3} per {item.perPackage} items");
            Console.WriteLine("===========");
            var inventory = new Dictionary<string, int>() 
            {
                ["hammer, ball pein"] = 18,
                ["hammer, cross pein"] = 5,
                ["screwdriver, Phillips #2"] = 14
            };

            Console.WriteLine($"Inventory on {DateTime.Now:d}");
            Console.WriteLine(" ");
            // 项目名称为左对齐，其数量为右对齐。 通过在内插表达式后面添加一个逗号（“,”）并指定“最小”字段宽度来指定对齐方式。 如果指定的值是正数，则该字段为右对齐。 如果它为负数，则该字段为左对齐。
            // 尝试删除 {"Item",-25} 和 {item.Key,-25} 代码中的负号，然后再次运行该示例。 此时，项名为右对齐。
            Console.WriteLine($"|{"item", -25}|{"Quantity", 10}|");// 打印表头
            foreach (var i in inventory)
            {
                Console.WriteLine($"|{i.Key, -25}|{i.Value,10}|");
            }
            /*
            Inventory on 2023/4/24

            |item                     |  Quantity|
            |hammer, ball pein        |        18|
            |hammer, cross pein       |         5|
            |screwdriver, Phillips #2 |        14| 
            */

            Console.WriteLine($"[{DateTime.Now, -20:d}] Hour [{DateTime.Now, -10:HH}] [{1063.342,15:N2}] feet");
        }

        /// <summary>
        /// 字符串内插的高级方案
        /// </summary>
        /// <param name="args"></param>
        static void Main01(string[] args)
        {
            // 若要逐字解释转义序列，可使用逐字字符串文本。 内插逐字字符串以 $ 字符开头，后跟 @ 字符。 可以按任意顺序使用 $ 和 @ 标记：$@"..." 和 @$"..." 均为有效的内插逐字字符串。
            // 若要在结果字符串中包含大括号 "{" 或 "}"，请使用两个大括号 "{{" 或 "}}"。
            var xs = new int[] { 1, 2, 7, 9 };
            var ys = new int[] { 7, 9, 12 };
            Console.WriteLine($"Find the intersection of the {{{string.Join(", ", xs)}}} and {{{string.Join(", ", ys)}}} sets.");

            var userName = "Jane";
            var stringWithEscapes = $"C:\\Users\\{userName}\\Documents";
            var verbatimInterpolated = $@"C:\Users\{userName}\Documents";
            Console.WriteLine(stringWithEscapes);
            Console.WriteLine(verbatimInterpolated);

            // Expected output:
            // Find the intersection of the {1, 2, 7, 9} and {7, 9, 12} sets.
            // C:\Users\Jane\Documents
            // C:\Users\Jane\Documents

            //在内插表达式中使用三元条件运算符 ?:
            //因为冒号(:) 在具有内插表达式的项中具有特殊含义，为了在表达式中使用条件运算符，请将表达式放在括号内，如下例所示：
            var rand = new Random();
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"Coin flip: {(rand.NextDouble() < 0.5 ? "heads" : "tails")}");
            }
        }

        /// <summary>
        /// 读取和回显文件
        /// </summary>
        /// <param name="args"></param>
        static async Task Main(string[] args)
        {
            //var lines = ReadFrom("sampleQuotes.txt");
            //foreach (var line in lines)
            //{
            //    //Console.WriteLine(line);
            //    Console.Write(line);
            //    if (!string.IsNullOrWhiteSpace(line))
            //    {
            //        var pause = Task.Delay(200);
            //        pause.Wait();
            //    }
            //}
            await RunTeleprompter();
        }

        private static async Task RunTeleprompter()
        {
            var config = new TelePrompterConfig();
            var displayTask = ShowTeleprompter(config);

            var speedTask = GetInput(config);
            //此处的一种新方法是 WhenAny(Task[]) 调用。 这会创建 Task，只要自变量列表中的任意一项任务完成，它就会完成。
            await Task.WhenAny(displayTask, speedTask);
        }

        private static async Task ShowTeleprompter(TelePrompterConfig config)
        {
            var words = ReadFrom("sampleQuotes.txt");
            foreach (var word in words)
            {
                Console.Write(word);
                if (!string.IsNullOrWhiteSpace(word))
                {
                    await Task.Delay(config.DelayInMilliseconds);
                }
            }
            config.SetDone();
        }

        private static async Task GetInput(TelePrompterConfig config)
        {
            Action work = () =>
            {
                do
                {
                    var key = Console.ReadKey(true);
                    if (key.KeyChar == '>')
                        config.UpdateDelay(-10);
                    else if (key.KeyChar == '<')
                        config.UpdateDelay(10);
                    else if (key.KeyChar == 'X' || key.KeyChar == 'x')
                        config.SetDone();
                } while (!config.Done);
            };
            await Task.Run(work);
        }

        private static async Task ShowTeleprompter()
        {
            var words = ReadFrom("sampleQuotes.txt");
            foreach (var word in words)
            {
                Console.Write(word);
                if (!string.IsNullOrWhiteSpace(word))
                {
                    await Task.Delay(200);
                }
            }
        }

        private static async Task GetInput()
        {
            var delay = 200;
            Action work = () =>
            {
                do
                {
                    var key = Console.ReadKey(true);
                    if (key.KeyChar == '>')
                    {
                        delay -= 10;
                    }
                    else if (key.KeyChar == '<')
                    {
                        delay += 10;
                    }
                    else if (key.KeyChar == 'X' || key.KeyChar == 'x')
                    {
                        break;
                    }
                } while (true);
            };
            await Task.Run(work);
        }


        /// <summary>
        /// 这是一种称为“iterator 方法”的特殊类型 C# 方法。 迭代器方法返回延迟计算的序列。 也就是说，序列中的每一项是在使用序列的代码提出请求时生成。 迭代器方法包含一个或多个 yield return 语句。 ReadFrom 方法返回的对象包含用于生成序列中所有项的代码。 在此示例中，这涉及读取源文件中的下一行文本，然后返回相应的字符串。 每当调用代码请求生成序列中的下一项时，代码就会读取并返回文件中的下一行文本。 读取完整个文件时，序列会指示没有其他项。
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        static IEnumerable<string> ReadFrom(string file)
        {
            string? line;
            using (var reader = File.OpenText(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    //yield return line;
                    var words = line.Split(' ');
                    foreach (var word in words)
                    {
                        yield return word + " ";
                    }
                    yield return Environment.NewLine;
                }
            }
        }
    }
}