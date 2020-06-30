using LoongEgg.LoongLog;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/**
 * @author zlim
 * @create 2020/6/29 21:11:23
 */
namespace Lesson.UdpCore {
    public class UdpReceiver {

        public event UdpReceivedEvent MessageReceived;

        public int Port { get; set; }

        public string GroupAddress { get; set; }


        public UdpReceiver() {
            Port = 5566;
        }

        public async Task ReceiveAsync() {
            using (var client =  new UdpClient(Port)) {
                bool stop = false;
                do {
                    UdpReceiveResult result = await client.ReceiveAsync();
                    byte[] buff = result.Buffer;
                    MessageReceived?.Invoke(this,new UdpReceivedEventArgs(buff));
                    string rec = Encoding.UTF8.GetString(buff);
                    Logger.Info(rec);
                    stop = rec == "s";
                } while (!stop);
            }
        }

    }
}
