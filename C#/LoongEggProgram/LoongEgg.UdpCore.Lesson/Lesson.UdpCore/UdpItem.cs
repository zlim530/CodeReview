using System;
using System.Collections.Generic;
using System.Text;

/**
 * @author zlim
 * @create 2020/6/29 23:39:31
 */
namespace Lesson.UdpCore {
    public class UdpItem {

        public string Name { get; set; }

        public string Unit { get; set; }

        public int Length { get; set; }

        public double Resolution { get; set; }

        public bool Signed { get; set; }

        public double Maximum { get; set; }

        public double Minimum { get; set; }


    }

    public class UdpPack {
        public string PackName { get; set; }

        public int PackID { get; set; }

        public UdpItem[] Items { get; set; }
    }
}
