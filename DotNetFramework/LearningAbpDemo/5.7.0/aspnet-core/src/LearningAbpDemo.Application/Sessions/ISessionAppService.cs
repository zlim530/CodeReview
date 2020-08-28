using System.Threading.Tasks;
using Abp.Application.Services;
using LearningAbpDemo.Sessions.Dto;

namespace LearningAbpDemo.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
