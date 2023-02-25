namespace MiddleWareDemo;

/// <summary>
/// ����վ�еľ�̬�ļ����д�����м��
/// </summary>
public class MyStaticFileMiddleware
{
    private readonly RequestDelegate next;
    private readonly IWebHostEnvironment hostEnv;

    public MyStaticFileMiddleware(RequestDelegate next, IWebHostEnvironment hostEnv)
    {
        this.next = next;
        this.hostEnv = hostEnv;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value ?? "";
        //����·����ȡwwwroot�ļ����µľ�̬�ļ�
        var file = hostEnv.WebRootFileProvider.GetFileInfo(path);
        if (!file.Exists || !ContentTypeHelper.IsValid(file))
        {
            await next(context);
            return;
        }
        context.Response.ContentType = ContentTypeHelper.GetContentType(file);
        context.Response.StatusCode = 200;
        using var stream = file.CreateReadStream();
        byte[] bytes = await ToArrayAsync(stream);
        await context.Response.Body.WriteAsync(bytes);
    }

    private static async Task<byte[]> ToArrayAsync(Stream stream)
    {
        using MemoryStream memStream = new MemoryStream();
        await stream.CopyToAsync(memStream);
        memStream.Position = 0;
        byte[] bytes = memStream.ToArray();
        return bytes;
    }
}