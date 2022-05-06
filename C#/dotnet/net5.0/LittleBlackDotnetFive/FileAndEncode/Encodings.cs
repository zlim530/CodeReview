using System;
using System.Text;

namespace FileAndEncode
{
    public class Encodings
    {
        public static void Coding(string message)
        {
            Console.WriteLine("提示：");
            Console.WriteLine("[1] ASCII");
            Console.WriteLine("[2] UTF-8");
            Console.WriteLine("[3] Unicode UTF-16");
            Console.WriteLine("any other key Default");
            Console.WriteLine();
            ConsoleKey number = Console.ReadKey(false).Key;
            Console.WriteLine();
            Encoding encoder = number switch
            {
                ConsoleKey.D1 or ConsoleKey.NumPad1 => Encoding.ASCII,
                ConsoleKey.D2 or ConsoleKey.NumPad2 => Encoding.UTF8,
                ConsoleKey.D3 or ConsoleKey.NumPad3 => Encoding.Unicode,
                _ => Encoding.Default
            };
            byte[] encoded = encoder.GetBytes(message);
            Console.WriteLine($"{encoder.GetType().Name} len: {encoded.Length}");
            Console.WriteLine($"BYTE HEX CHAR");
            foreach (var b in encoded)
            {
                Console.WriteLine($"{b, 4} {b.ToString("X"),4} {(char)b, 5}");
            }

            Console.WriteLine("解码----->");
            string decoded = encoder.GetString(encoded);
            Console.WriteLine(decoded);
        }
    }
}
