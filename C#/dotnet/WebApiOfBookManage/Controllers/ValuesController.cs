using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiOfBoolManage.Models;

namespace WebApiOfBookManage.Controllers
{
    [Route("book/add")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static Dictionary<string,AddRequest> DB = new Dictionary<string, AddRequest>();

        // POST book/add
        [HttpPost]
        public AddRespose Post([FromBody] AddRequest req)
        {
            AddRespose resp = new AddRespose();
            try
            {
                DB.Add(req.ISBN,req);
                resp.ISBN = req.ISBN;
                resp.message = "交易成功";
                resp.result = "S";
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                resp.ISBN = "";
                resp.message = "交易失败";
                resp.result = "F";
            }
            return resp;
        }

    }
}
