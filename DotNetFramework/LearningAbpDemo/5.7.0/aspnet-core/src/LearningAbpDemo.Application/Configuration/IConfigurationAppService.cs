using System.Threading.Tasks;
using LearningAbpDemo.Configuration.Dto;

namespace LearningAbpDemo.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
