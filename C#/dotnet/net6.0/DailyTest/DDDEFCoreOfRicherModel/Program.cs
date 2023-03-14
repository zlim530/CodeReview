// See https://aka.ms/new-console-template for more information
using DDDEFCoreOfRicherModel.DbContexts;
using DDDEFCoreOfRicherModel.Models;
using DDDEFCoreOfRicherModel.ValueObject;
using Zack.Infrastructure.EFCore;

using var ctx = new MyDbContext();

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