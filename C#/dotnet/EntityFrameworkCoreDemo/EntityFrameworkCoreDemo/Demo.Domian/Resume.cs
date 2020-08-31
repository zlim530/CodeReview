using System;
using System.Collections.Generic;
using System.Text;

/**
 * @author zlim
 * @create 2020/8/31 14:30:53
 */
namespace Demo.Domian {
    /// <summary>
    /// 与 Player 是一对一关系
    /// </summary>
    public class Resume {
        public int Id { get; set; }

        public string Description { get; set; }

        public int PlayerId { get; set; }

        // 导航属性
        public Player Player { get; set; }
    }
}
