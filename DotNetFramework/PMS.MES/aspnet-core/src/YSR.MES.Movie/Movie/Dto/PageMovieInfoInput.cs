using YSR.MES.Common.CommonModel;

namespace YSR.MES.Movie.Movie.Dto
{
    /// <summary>
    /// 获取电影信息输入 Dto
    /// </summary>
    public class PageMovieInfoInput : InputPageBase
    {
        /// <summary>
        /// 片名
        /// </summary>
        public string t { get; set; }
    }
}
