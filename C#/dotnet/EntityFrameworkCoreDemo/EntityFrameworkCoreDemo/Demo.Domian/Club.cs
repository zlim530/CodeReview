using System;
using System.Collections.Generic;

/**
 * @author zlim
 * @create 2020/8/27 20:40:08
 */
namespace Demo.Domian {
    public class Club {

        public Club() {
            Players = new List<Player>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public DateTime DateOfEstableishment { get; set; }

        public string History { get; set; }

        public League League { get; set; }

        public List<Player> Players { get; set; }

    }
}
