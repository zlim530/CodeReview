using FileAndEncode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

//Console.WriteLine("Hello World!");
//SystemInfo.FileSystemInfo();
//SystemInfo.WorkWithDrives();
//SystemInfo.WorkWithDirectories();
//SystemInfo.WorkWithFiles();

//Encodings.Coding("你好，世界！");
//Encodings.Coding("Hello,World!");

//第三部分：序列化
Console.WriteLine("Json Serialization");
List<Person> people = new()
{
    new Person(49)
    {
        FirstName = "ok",
        LastName = "ok",
        DateOfBirth = new DateTime(2010, 2, 12)
    },
    new Person(19)
    {
        FirstName = "yz",
        LastName = "yzzy",
        DateOfBirth = new DateTime(1970, 10, 12)
    }
};
Console.WriteLine(Environment.CurrentDirectory);
string jsonPath = Path.Combine(Environment.CurrentDirectory, "person.json");
//using (StreamWriter jsonStream = File.CreateText(jsonPath))
//{
//    Newtonsoft.Json.JsonSerializer jss = new();
//    jss.Serialize(jsonStream, people);
//}
//读取 Json
using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open))
{
    List<Person> loaded = await JsonSerializer.DeserializeAsync(jsonLoad, typeof(List<Person>)) as List<Person>;
    if (loaded is not null)
    {
        foreach (var person in loaded)
        {
            Console.WriteLine(person);
        }
    }
}

