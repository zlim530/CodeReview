// See https://aka.ms/new-console-template for more information
using DDDEFCoreOfRicherModel.DbContexts;
using DDDEFCoreOfRicherModel.Models;

using var ctx = new MyDbContext();

//User user = new User("Zack");
//user.Tag = "MyTag";
//user.ChangePassword("123456");
//ctx.Users.Add(user);
//ctx.SaveChanges();

User u1 = ctx.Users.First(u => u.UserName == "Zack");
//u1.Remark = "123";// 无法为属性或索引器“User.Remark”赋值 - 它是只读的
u1.Tag = "test";
Console.WriteLine(u1);
