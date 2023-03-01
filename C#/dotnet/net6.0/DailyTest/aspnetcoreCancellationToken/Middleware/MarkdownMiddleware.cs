using MarkdownSharp;
using System.IO;
using System.Text;

namespace aspnetcoreCancellationToken.Filter;

public class MarkdownMiddleware
{
    private readonly RequestDelegate next;
    private readonly IWebHostEnvironment hostEnv;

    public MarkdownMiddleware(RequestDelegate requestDelegate, IWebHostEnvironment hostEnv)
    {
        this.next = requestDelegate;
        this.hostEnv = hostEnv;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string path = context.Request.Path.ToString();
        if(!path.EndsWith(".md",true,null))// 只处理 .md 文件请求
        {
            await next.Invoke(context);
            return;
        }
        var file = hostEnv.WebRootFileProvider.GetFileInfo(path);
        if (!file.Exists)// 判断文件是否存在
        {
            await next.Invoke(context);
            return;
        }
        using var stream = file.CreateReadStream();
        Ude.CharsetDetector detector = new Ude.CharsetDetector();
        detector.Feed(stream);
        detector.DataEnd();
        string charset = detector.Charset??"UTF-8";// 探测文本的编码类型
        stream.Position = 0;// 将文件流复位，因为 detector 在探测文件编码类型时将 stream 的指针往后挪了
        // 后面需要以正确的编码方式重新读取这个文件流于是要将文件指针复位
        using StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(charset));
        string mdText = await reader.ReadToEndAsync();// 读取 markdown 源文件
        Markdown md = new Markdown();
        string html = md.Transform(mdText);// 将 Markdown 文件转换为 html 格式
        context.Response.ContentType = "text/html;charset=UTF-8";
        await context.Response.WriteAsync(html);

    }
}