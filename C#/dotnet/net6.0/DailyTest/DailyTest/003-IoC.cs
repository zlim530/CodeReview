using System;
using Microsoft.Extensions.DependencyInjection;

namespace DailyTest 
{
	public class IoC
	{
        /// <summary>
        /// ����ע�᣺�������ڵĻ�������
        /// </summary>
        /// <param name="args"></param>
		static void Main1(string[] args)
		{
            ServiceCollection services = new ServiceCollection();
            //services.AddTransient<TestServiceImp1>();
            //services.AddSingleton<TestServiceImp1>();
            // ���ע����� Singleton ����ģʽ����ֻ������һ������
            services.AddScoped<TestServiceImp1>();// ����� TestServiceImp1 �Ƿ������ͣ���һ���࣬�������ʹ�ýӿ�����Ϊ�������ͣ���ʵ���������
            // Scope ͬһ�� scope ��Χ�ڵķ����õ�����ͬһ�������ڿ���̨���������ǿ����������� scope ���� asp .net core ����� scope �ķ�Χ�ɿ�ܾ���
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())// ����� ServiceProvider ���Ƿ�������λ��
            {
                // ServiceLocator ��������λ��ʵ�� ���Ʒ�ת����
                var testService = serviceProvider.GetService<TestServiceImp1>();
                testService.Name = "tom";
                testService.SayHi();

                var t2 = serviceProvider.GetService<TestServiceImp1>();
                Console.WriteLine(object.ReferenceEquals(testService,t2));// False������ͬһ������
                // ��Ϊע�� TestServiceImp1 ����ʱѡ���ǵ� AddTransient ��Ϊ�������������
                // ���ÿ�ε��� GetService ���᷵��һ���µĶ���
                t2.Name = "Tim";
                t2.SayHi();

                testService.SayHi();

                using (IServiceScope scope = serviceProvider.CreateScope())
                {
                    // ���´����� scope �л�ȡ Scope ��صĶ���Ҫʹ���´����� IServiceScope ���� .ServiceProvider ��ȡ������Ҫ���� scope ��Χ��� serviceProvider ����
                    var t3 = scope.ServiceProvider.GetService<TestServiceImp1>();
                    t3.Name = "Jack";
                    t3.SayHi();

                    var t4 = scope.ServiceProvider.GetService<TestServiceImp1>();
                    t3.Name = "Rose";
                    t3.SayHi();

                    Console.WriteLine(object.ReferenceEquals(t4, t3));// True��ͬһ�� scope ��Χ�лᴴ����ͬ�Ķ���
                    Console.WriteLine(object.ReferenceEquals(t2, t3));// False����ͬ scope ��Χ�лᴴ����ͬ�Ķ���
                }
            }
        }


        /// <summary>
        /// ����ע�룺����λ��
        /// </summary>
        /// <param name="args"></param>
        static void Main2(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScoped<ITestService,TestServiceImp1>();
            services.AddScoped<ITestService,TestServiceImp2>();
            //services.AddScoped(typeof(ITestService), typeof(TestServiceImp1));// �����ʹ�÷��ͷ���Ҳ����ʹ�� typeof ��ע��
            //services.AddScoped<ITestService>(s => new TestServiceImp1());// Add* �����кܶ����أ������Լ�������ѡ����Ӧ�����ط�������
            //services.AddSingleton(typeof(ITestService), new TestServiceImp1());// ����ʹ�� new ����ķ���ʵ�֣�����ʹ���ڴ�����������Ҫ����ĳЩ�ض���ֵ
            using (var sp = services.BuildServiceProvider())
            {
                // GetService ����Ҳ������񣬾ͻ᷵�� null�����������쳣
                ITestService t1 = sp.GetService<ITestService>();// ͨ���ӿڵķ�������õ�ע���ʵ�������ע��ע��ʲô������ʲô����
                // ���ʹ�� GetRequiredService ���������Ҳ�������ͻ����쳣
                // �ñ���ʽ����ת���� as ��������ʽ����ת��ʧ��ʱ��ֱ�����쳣������ as ת��ʧ�ܻ᷵�� null
                //t1 = sp.GetRequiredService<TestServiceImp1>();
                t1.Name = "New Jack";
                t1.SayHi();

                Console.WriteLine(t1.GetType());

                IEnumerable<ITestService> tests = sp.GetServices<ITestService>();
                foreach (ITestService t in tests)
                {
                    Console.WriteLine(t.GetType());
                }

                // ���ע���˶����������õ����һ��ע��ʱ��ʵ����
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
            Console.WriteLine($"��ã�����{Name}");
        }
    }

}