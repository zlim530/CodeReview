using aspnetcoreCancellationToken.Filter;

namespace aspnetcoreCancellationToken
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<MarkdownMiddleware>();
            // Ҫ�������Զ���� Markdown �����м���� StaticFiles �м��ǰ��ע�ᣬ��Ϊ�ں���ע�����ֱ�ӱ� UseStaticFiles �м�������޷��ﵽר�Ŵ��� Markdown �����ļ���Ч�� 
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}