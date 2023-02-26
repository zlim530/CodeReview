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
        if(!path.EndsWith(".md",true,null))// ֻ���� .md �ļ�����
        {
            await next.Invoke(context);
            return;
        }
        var file = hostEnv.WebRootFileProvider.GetFileInfo(path);
        if (!file.Exists)// �ж��ļ��Ƿ����
        {
            await next.Invoke(context);
            return;
        }
        using var stream = file.CreateReadStream();
        Ude.CharsetDetector detector = new Ude.CharsetDetector();
        detector.Feed(stream);
        detector.DataEnd();
        string charset = detector.Charset??"UTF-8";// ̽���ı��ı�������
        stream.Position = 0;// ���ļ�����λ����Ϊ detector ��̽���ļ���������ʱ�� stream ��ָ������Ų��
        // ������Ҫ����ȷ�ı��뷽ʽ���¶�ȡ����ļ�������Ҫ���ļ�ָ�븴λ
        using StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(charset));
        string mdText = await reader.ReadToEndAsync();// ��ȡ markdown Դ�ļ�
        Markdown md = new Markdown();
        string html = md.Transform(mdText);// �� Markdown �ļ�ת��Ϊ html ��ʽ
        context.Response.ContentType = "text/html;charset=UTF-8";
        await context.Response.WriteAsync(html);

    }
}