using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NETCoreWebAPIDemo;

public class MyExceptionFilter : IAsyncExceptionFilter
{
    private readonly IWebHostEnvironment webHostEnvironment;

    public MyExceptionFilter(IWebHostEnvironment webHostEnvironment)
    {
        this.webHostEnvironment = webHostEnvironment;
    }

    public Task OnExceptionAsync(ExceptionContext context)
    {
        // ExceptionContext �����������Ҫ���ԣ�
        // context.Exception �����쳣��Ϣ����
        // ����� context.ExceptionHandled ��ֵΪ true,������ ExceptionFilter ������ִ��
        // context.Result ��ֵ��������ͻ���
        string msg;
        // �ڿ��������´�ӡ�쳣����ϸ��Ϣ���������������н���ʾ�����������쳣����������ӡ�쳣��ջ��Ϣ����ȫ���ǣ�
        if (webHostEnvironment.IsDevelopment())
        {
            msg = context.Exception.ToString();
        }
        else 
        {
            msg = "�������˷���δ������쳣";
        }
        var objectResult = new ObjectResult(new { code = 500, message = msg});
        context.Result = objectResult;
        context.ExceptionHandled = true;
        return Task.CompletedTask;
    }
}