using Dynamic.Json;
using Microsoft.AspNetCore.Http;
using System;
using System.Reflection;
using System.Text.Json;

namespace MiddleWareDemo;

/// <summary>
/// ��������ĵ��������������İ�
/// </summary>
public class BindingHelper
{
    /// <summary>
    /// httpContext����Ϊ�����HttpContext����actionMethod����Ϊ
    /// ����������MethodInfo���󣬷����ķ���ֵ�ǽ��������Ĳ��������Ĳ���ֵ��
    /// ����Լ����������ֻ�������һ���������������ΪHttpContext���ͣ�
    /// ��ֱ�ӽ�GetParameterValues������httpContext����ֵ��Ϊ���������Ĳ�����
    /// �����������HttpContext���ͣ���������ĵ�Json�ַ��������л�Ϊ���������͡�
    /// </summary>
    /// <param name="context"></param>
    /// <param name="methodInfo"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static object?[] GetParameterValues(HttpContext context, MethodInfo methodInfo)
    {
        var parameters = methodInfo.GetParameters();
        if (parameters.Length <= 0)
        {
            return new object?[0];
        }
        else if (parameters.Length > 1)
        {
            throw new Exception("Action����ֻ��Ϊ0��1��");
        }
        //������parameters.Length==1��ֻ��һ�����������
        //�������ΪHttpContext���ͣ���ֱ�Ӵ���
        if (parameters.Single().ParameterType == typeof(HttpContext))
        {
            return new object?[] { context };
        }
        //�������͵Ĳ������������������json�����л�
        if (!context.Request.HasJsonContentType())
        {
            throw new Exception("Action ���ֻ��һ���������� contentType ������ application/json ����");
        }
        // ���������Ϊ�գ���󶨲���ֵΪ null
        if (context.Request.ContentLength == 0)
        {
            return new object?[1] { null };
        }
        var reqStream = context.Request.BodyReader.AsStream();
        // ��������
        Type paramType = parameters.Single().ParameterType;
        object? paramValue = JsonSerializer.Deserialize(reqStream, paramType);
        return new object?[1] { paramValue };
    }
}