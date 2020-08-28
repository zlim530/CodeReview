using Abp.Application.Services.Dto;

namespace LearningAbpDemo.MyTest.DTO
{
    public class PagedMovieResultRequestDTO : PagedResultRequestDto
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Keyword { get; set; }
    }
}
