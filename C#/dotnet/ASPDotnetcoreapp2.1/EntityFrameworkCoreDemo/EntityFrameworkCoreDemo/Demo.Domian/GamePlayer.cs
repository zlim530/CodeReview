/**
 * @author zlim
 * @create 2020/8/31 14:09:51
 */
namespace Demo.Domian {
    public class GamePlayer {

        /*
        GamePlayer 是 Player 与 Game 表的中间表，因此 PlayerId 和 GameId 是它的联合主键
        */
        public int PlayerId { get; set; }

        public int GameId { get; set; }

        public Player Player { get; set; }

        public Game Game { get; set; }
    }
}
