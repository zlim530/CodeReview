using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChatDemo
{
    public class MsgHub : Hub
    {
        // 在 hub 中编写的方法，都是要被客户端调用的方法
        public void SendMsg(string txt)
        {
            // 服务器主动调用客户端的方法
            Clients.All.getMsg(txt);
        }
    }
}