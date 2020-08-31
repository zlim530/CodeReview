using System;
using System.Collections.Generic;
using System.Text;

/**
 * @author zlim
 * @create 2020/8/31 14:08:49
 */
namespace Demo.Domian {
    public class Game {

        public Game() {
            GamePlayers = new List<GamePlayer>();
        }

        // 默认取名为 Id 的属性就是数据表中的主键
        public int Id { get; set; }

        public int Round { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        public List<GamePlayer> GamePlayers { get; set; }
    }
}
