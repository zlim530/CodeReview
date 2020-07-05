using System;
using System.Collections.Generic;
using System.Text;
using LoongEgg.MvvmCore;

/**
 * @author zlim
 * @create 2020/7/3 14:43:31
 */
namespace Lesson.UdpCore.WPF {
    public class AltViewModel:ViewModel{

        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name;



        public double Value {
            get { return _Value; }
            set {
                if (SetProperty(ref _Value,value)) {
                    string msg = $"{nameof(Value)} set to: {Value}";
                    UdpSender.SendAsync(msg);
                }
            }
        }
        private double _Value;


        public string Unit {
            get { return _Unit; }
            set { _Unit = value; }
        }
        private string _Unit;

        public UdpSender UdpSender { get; set; }
    }
}
