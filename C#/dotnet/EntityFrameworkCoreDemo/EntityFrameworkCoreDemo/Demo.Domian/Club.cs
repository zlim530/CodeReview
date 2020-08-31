using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Column(TypeName ="date")]// 表示在数据库中对应列的类型为日期类型
        public DateTime DateOfEstableishment { get; set; }

        public string History { get; set; }

        // 导航属性
        public League League { get; set; }
        
        // 导航属性：此时导航属性是一个集合，相当于 Club 是主表，Player 是子表
        public List<Player> Players { get; set; }

    }
}
