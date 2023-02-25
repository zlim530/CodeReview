using Microsoft.Extensions.FileProviders;

namespace MiddleWareDemo;

public class ContentTypeHelper
{
    private static readonly Dictionary<string, string> data = new(StringComparer.OrdinalIgnoreCase);

    static ContentTypeHelper()
    {
        data[".html"] = "text/html; charset=utf-8";
        data[".htm"] = "text/html; charset=utf-8";
        data[".txt"] = "text/plain; charset=utf-8";
        data[".jpg"] = "image/jpeg";
        data[".jpeg"] = "image/jpeg";
        data[".png"] = "image/png";
        data[".js"] = "application/x-javascript; charset=utf-8";
        data[".css"] = "text/css";
    }

    /// <summary>
    /// �ж�һ���ļ��Ƿ��ǺϷ��ľ�̬�ļ�
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static bool IsValid(IFileInfo file)
    {
        if (file.IsDirectory)
        {
            return false;
        }
        var extension = Path.GetExtension(file.Name);
        return data.ContainsKey(extension);
    }

    /// <summary>
    /// ��ȡһ���ļ���Ӧ�� ContentType
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string GetContentType(IFileInfo file)
    {
        var extension = Path.GetExtension(file.Name);
        return data[extension];
    }

}