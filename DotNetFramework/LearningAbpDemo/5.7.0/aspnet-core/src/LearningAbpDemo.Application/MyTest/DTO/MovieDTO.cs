using Abp.Application.Services.Dto;

namespace LearningAbpDemo.MyTest.DTO
{
    public class MovieDTO:EntityDto<int>
    {
        public string Name { get; set; }

        public string Stars { get; set; }
    }
}
