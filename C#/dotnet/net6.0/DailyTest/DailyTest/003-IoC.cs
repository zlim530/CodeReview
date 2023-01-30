using System;
using Microsoft.Extensions.DependencyInjection;

namespace DailyTest 
{
	public class IoC
	{
        /// <summary>
        /// 服务注册：生命周期的基本解释
        /// </summary>
        /// <param name="args"></param>
		static void Main1(string[] args)
		{
            ServiceCollection services = new ServiceCollection();
            //services.AddTransient<TestServiceImp1>();
            //services.AddSingleton<TestServiceImp1>();
            // 如果注册的是 Singleton 单例模式，则只会生成一个对象
            services.AddScoped<TestServiceImp1>();// 这里的 TestServiceImp1 是服务类型，是一个类，但最好是使用接口来作为服务类型，而实现类就用类
            // Scope 同一个 scope 范围内的服务拿到的是同一个对象，在控制台程序中我们可以自主创建 scope 但是 asp .net core 框架中 scope 的范围由框架决定
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())// 这里的 ServiceProvider 就是服务器定位器
            {
                // ServiceLocator 服务器定位器实现 控制反转功能
                var testService = serviceProvider.GetService<TestServiceImp1>();
                testService.Name = "tom";
                testService.SayHi();

                var t2 = serviceProvider.GetService<TestServiceImp1>();
                Console.WriteLine(object.ReferenceEquals(testService,t2));// False：不是同一个对象
                // 因为注册 TestServiceImp1 服务时选择是的 AddTransient 作为服务的生命周期
                // 因此每次调用 GetService 都会返回一个新的对象
                t2.Name = "Tim";
                t2.SayHi();

                testService.SayHi();

                using (IServiceScope scope = serviceProvider.CreateScope())
                {
                    // 在新创建的 scope 中获取 Scope 相关的对象，要使用新创建的 IServiceScope 对象 .ServiceProvider 获取，而不要用在 scope 范围外的 serviceProvider 对象
                    var t3 = scope.ServiceProvider.GetService<TestServiceImp1>();
                    t3.Name = "Jack";
                    t3.SayHi();

                    var t4 = scope.ServiceProvider.GetService<TestServiceImp1>();
                    t3.Name = "Rose";
                    t3.SayHi();

                    Console.WriteLine(object.ReferenceEquals(t4, t3));// True：同一个 scope 范围中会创建相同的对象
                    Console.WriteLine(object.ReferenceEquals(t2, t3));// False：不同 scope 范围中会创建不同的对象
                }
            }
        }


        /// <summary>
        /// 依赖注入：服务定位器
        /// </summary>
        /// <param name="args"></param>
        static void Main2(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScoped<ITestService,TestServiceImp1>();
            services.AddScoped<ITestService,TestServiceImp2>();
            //services.AddScoped(typeof(ITestService), typeof(TestServiceImp1));// 如果不使用泛型方法也可以使用 typeof 来注册
            //services.AddScoped<ITestService>(s => new TestServiceImp1());// Add* 方法有很多重载，按照自己的需求选择相应的重载方法即可
            //services.AddSingleton(typeof(ITestService), new TestServiceImp1());// 或者使用 new 对象的方法实现，这种使用于创建对象是需要传入某些特定的值
            using (var sp = services.BuildServiceProvider())
            {
                // GetService 如果找不到服务，就会返回 null，但不会抛异常
                ITestService t1 = sp.GetService<ITestService>();// 通过接口的服务对象，拿到注册的实现类对象：注意注册什么类型拿什么类型
                // 如果使用 GetRequiredService 方法，则找不到服务就会抛异常
                // 好比显式类型转换与 as 的区别：显式类型转换失败时会直接抛异常报错，而 as 转换失败会返回 null
                //t1 = sp.GetRequiredService<TestServiceImp1>();
                t1.Name = "New Jack";
                t1.SayHi();

                Console.WriteLine(t1.GetType());

                IEnumerable<ITestService> tests = sp.GetServices<ITestService>();
                foreach (ITestService t in tests)
                {
                    Console.WriteLine(t.GetType());
                }

                // 如果注册了多个服务，则会拿到最后一次注册时的实现类
                var tt = sp.GetService<ITestService>();
                Console.WriteLine(tt.GetType());
            }

        }
    }

	public interface ITestService
	{
		public string Name { get; set; }
		public void SayHi();
	}

    public class TestServiceImp1 : ITestService, IDisposable
    {
		public string Name { get; set; }

        public void Dispose()
        {
            Console.WriteLine("Dispose ... ");
        }

        public void SayHi()
        {
			Console.WriteLine($"Hi I'm {Name}");
        }
    }

    public class TestServiceImp2 : ITestService
    {
        public string Name { get ; set ; }

        public void SayHi()
        {
            Console.WriteLine($"你好，我是{Name}");
        }
    }

}