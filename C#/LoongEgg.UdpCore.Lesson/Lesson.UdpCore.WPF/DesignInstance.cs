using System;
using System.Collections.Generic;
using System.Text;

/**
 * @author zlim
 * @create 2020/7/3 14:46:58
 */
namespace Lesson.UdpCore.WPF {
    public static class DesignInstance {

        public static AltViewModel AltDesignModel { get; set; } = new AltViewModel { Name = "CoolName",Value = 2233,Unit="m/s"};

    }
}
