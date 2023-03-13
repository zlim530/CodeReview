// See https://aka.ms/new-console-template for more information
using DDDEFCoreOfRicherModel.DbContexts;
using DDDEFCoreOfRicherModel.Models;

using var ctx = new MyDbContext();
User user = new User("Zack");
user.Tag = "MyTag";
user.ChangePassword("123456");
ctx.Users.Add(user);
ctx.SaveChanges();

User u1 = ctx.Users.First(u => u.UserName == "Zack");
Console.WriteLine(u1);
