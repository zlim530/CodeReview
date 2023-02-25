using System.Text.RegularExpressions;

namespace MiddleWareDemo;


public class PathParser
{
    /// <summary>
    /// ����������������Action�����֡�
    /// ������·���з������������������ֺͲ������������֣�
    /// ���·������ʧ�ܣ�����ֵ�е�ok��ֵΪfalse��
    /// �������·��Ϊ��/Test1/Save����
    /// ��Parse�����ķ���ֵΪ(true,��Test1��,��Save��)��
    /// </summary>
    /// <param name="pathString"></param>
    /// <returns></returns>
    public static (bool ok, string? controllerName, string? actionName) Parse(PathString pathString)
    { 
        string? path = pathString.Value;
        if (path == null)
        {
            return (false, null, null);
        }
        // ���������������ֺ� Action ������
        var match = Regex.Match(path,
            "/([a-zA-Z0-9]+)/([a-zA-Z0-9]+)");
        if (!match.Success) 
        {
            return (false, null, null);
        }
        string controllerName = match.Groups[1].Value;
        string actionName = match.Groups[2].Value;
        return (true, controllerName, actionName);
    }
}