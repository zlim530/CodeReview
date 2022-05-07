using CSharpTenNew;
using Factory.Packet;
using System;
using System.Collections.Generic;

#region Convert.ToString()
int a = 10;
int b = 6;
// 1010 0110
// << >>
Console.WriteLine($"a << 3 = {a << 3}");
Console.WriteLine($"a * 8 = {a * 8}");
Console.WriteLine($"b >> 1 = {b >> 1}");// 0011
int c = 7;
// 0111
Console.WriteLine($"c >> 1 = {c >> 1}");// 0011

// Convert.ToString
static string FromIntToBinaryString(int value)
{
    return Convert.ToString(value, 2).PadLeft(16, '0');
}
int c1 = 1;
int c2 = 2;
int c3 = 8;
int c4 = 9;
int c5 = 17;
Console.WriteLine(FromIntToBinaryString(c1));
Console.WriteLine(FromIntToBinaryString(c2));
Console.WriteLine(FromIntToBinaryString(c3));
Console.WriteLine(FromIntToBinaryString(c4));
Console.WriteLine(FromIntToBinaryString(c5));
#endregion


#region when
Console.WriteLine("How old are you?");
string age = Console.ReadLine();
try
{
    int ageNumber = int.Parse(age);
    Console.WriteLine($"hi {ageNumber}");
}
catch (FormatException) when (age.Contains("$"))
{
    Console.WriteLine("Money can't buy time.");
}
catch (FormatException)
{
    Console.WriteLine("Other");
}
catch (Exception e)
{
    Console.WriteLine($"{e.GetType()} : {e.Message}");
}
#endregion


#region EnumOfByte
List<Worker> workers = new();
workers.Add(new Worker());
workers.Add(new Worker("张三", new DateTime(1976, 1, 16), WorldCity.Shenzhen));
workers.Add(new Worker("李四", new DateTime(1975, 1, 7), WorldCity.Beijing));
workers.Add(new Worker("ZLim", new DateTime(1998, 4, 28), WorldCity.Shanghai));
workers.Add(new Worker("原子", new DateTime(1111, 2, 12), WorldCity.Guangzhou));

foreach (var worker in workers)
{
    Worker.Surprise(worker);
}
#endregion