using LoongEgg.LoongLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

/**
 * @author zlim
 * @create 2020/6/29 23:04:10
 */
namespace Lesson.UdpCore {
    public class UdpSender {

        private IPEndPoint EndPoint;

        private UdpClient UdpClient;

        public int Port { get; set; } = 5566;

        public bool IsBroadCast { get; set; } = true;

        public UdpSender() {
            try {
                EndPoint = new IPEndPoint(IPAddress.Broadcast,Port);
                UdpClient = new UdpClient {
                    EnableBroadcast = IsBroadCast
                };
                Logger.Info("Udp sender initialized");
                Logger.Info(this.ToString());
            } catch (Exception ex) {

                throw ex;
            }
        }


        public async void SendAsync(string message) {
            if (EndPoint == null || UdpClient == null) {
                throw new InvalidOperationException("Init() before first sending.");
            }
            try {
                Logger.Info($"Send > {message}");
                byte[] datagram = Encoding.UTF8.GetBytes(message);
                await UdpClient.SendAsync(datagram, datagram.Length,EndPoint);
                
            } catch (Exception ex) {

                throw ex;
            }
        }


    }
}
