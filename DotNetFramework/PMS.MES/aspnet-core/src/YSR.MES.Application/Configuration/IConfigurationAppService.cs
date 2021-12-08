using System.Threading.Tasks;
using YSR.MES.Configuration.Dto;

namespace YSR.MES.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
