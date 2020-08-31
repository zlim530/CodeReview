using System;
using System.Collections.Generic;

/**
 * @author zlim
 * @create 2020/8/27 20:42:23
 */
namespace Demo.Domian {
    public class Player {

        public Player() {
            GamePlayers = new List<GamePlayer>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public List<GamePlayer> GamePlayers { get; set; }

        // 此时 Player 与 Resume 就是一对一的关系，EFCore 会选择一个类作为主体，但有时 EFCore 会选错，因此最好是手动指定一下比较好
        // 在 Demo.Data 项目 DBContext 类中的 OnModelCreating 方法进行指定
        public int ResumeId { get; set; }

        public Resume Resume { get; set; }
    }
}
