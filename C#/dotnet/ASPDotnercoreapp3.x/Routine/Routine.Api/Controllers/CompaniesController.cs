using Microsoft.AspNetCore.Mvc;
using Routine.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Controllers
{
    /*
     2.这个属性是应用于Controller的，它其实并不是强制的，但是它提供了一些帮助，使得Web API的开发体验更好。
     * 它会启动以下行为：
     * 要求使用属性路由（Attribute Routing）
     * 自动HTTP 400响应
     * 推断参数的绑定源
     * Multipart/form-data 请求推断
     * 错误状态代码的问题详细信息
     */
    [ApiController]
    [Route("api/companies")] // 还可用 [Route("api/[controller]")]
    public class CompaniesController:ControllerBase
    /*
     1.RESTful API 或者其它Web API的Controller都应该继承于 ControllerBase 这个类，而不是Controller这个类。 

     Controller类继承于ControllerBase，Controller添加了对视图的支持，因此它更适合用于处理 MVC Web 页面，而不是 Web API。但是如果你的Controller需要同时支持MVC Web页面和Web API，那么这时候就应该继承于Controller这个类。 
     */
    {
        private readonly ICompanyRepository _companyRepository;

        //3.我们需要通过构造函数注入ICompanyRepository，并把它存放在一个只读的字段里面。 
        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = 
                companyRepository ??
                //4.如果注入的ICompanyRepository的实例为null，那么就抛出一个ArgumentNullException。
                throw new ArgumentNullException(nameof(companyRepository));
        }

        /*
         5.想要返回数据结果，我们需要在Controller里面添加一个Action方法。我暂时把它的返回类型写为IActionResult。IActionResult里面定义了一些合约，它们可以代表Action方法返回的结果。 
         */
        [HttpGet]// Identifies an action that supports the HTTP GET method.
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepository.GetCompaniesAsync();
            //Creates a new Microsoft.AspNetCore.Mvc.JsonResult with the given value.
            return new JsonResult(companies);
            //6.我暂时只想把结果序列化为JSON格式并返回，这里我new了一个JsonResult（参考文档），它可以做这项工作。 
            

        }

    }
}
