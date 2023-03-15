// See https://aka.ms/new-console-template for more information
using DDDEFCoreOfRicherModel;
using DDDEFCoreOfRicherModel.Controllers;
using DDDEFCoreOfRicherModel.DbContexts;
using DDDEFCoreOfRicherModel.Models;
using DDDEFCoreOfRicherModel.ValueObject;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zack.Infrastructure.EFCore;

ServiceCollection services = new ServiceCollection();
services.AddDbContext<MyDbContext>(opt =>
{
    string connStr = "Server=127.0.0.1;Database=demoone;User ID=sa;Pwd=q1w2e3R4;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True;";
    opt.UseSqlServer(connStr);
});

// 进程内消息传递的开源库MediatR。事件的发布和事件的处理之间解耦。MediatR中支持“一个发布者对应一个处理者”和“一个发布者对应多个处理者”这两种模式
services.AddMediatR(typeof(Program).Assembly);
services.AddScoped<TestController>();

var sp = services.BuildServiceProvider();
var ctx = sp.GetRequiredService<MyDbContext>();
var t = sp.GetRequiredService<TestController>();

#region EFCore 领域事件发布的时机
User u2 = new User("ZLim");
u2.ChangePassword("q1w2e3r4");
u2.ChangeUserName("Z");
ctx.Users.Add(u2);
await ctx.SaveChangesAsync();
#endregion

//t.TestAsync("Test ");


#region EFCore of RicherModel And ValueObject
/*
//using var ctx = new MyDbContext();

User user = new User("Zack");
user.Tag = "MyTag";
user.ChangePassword("123456");
ctx.Users.Add(user);
//ctx.SaveChanges();

User u1 = ctx.Users.First(u => u.UserName == "Zack");
//u1.Remark = "123";// 无法为属性或索引器“User.Remark”赋值 - 它是只读的
u1.Tag = "test";
Console.WriteLine(u1);

MultilingualString name = new MultilingualString("北京", "Beijing");
Area area = new Area(16410,AreaType.SquareKM);
Geo loc = new Geo(116.4074, 39.9042);
Region city = new Region(name, area, loc, RegionLevel.Province);
city.ChangePopulation(21893100);
ctx.Regions.Add(city);
//ctx.SaveChanges();

Region c1 = ctx.Regions.First();
Console.WriteLine(c1.Area);
Console.WriteLine(c1.Name);
Console.WriteLine(c1.Location);
Console.WriteLine(c1.Level);

// 简化值对象的比较操作：让 MultilingualString 像个整体一样进行比较：内部是使用构建表达式树实现这样的效果：
// ctx.Regions.First(r => r.Name.Chinese == "北京" && r.Name.English == "Beijing")
var c = ctx.Regions.First(ExpressionHelper.MakeEqual((Region r) => r.Name, new MultilingualString("北京", "Beijing")));
Console.WriteLine(c);
*/
#endregion
