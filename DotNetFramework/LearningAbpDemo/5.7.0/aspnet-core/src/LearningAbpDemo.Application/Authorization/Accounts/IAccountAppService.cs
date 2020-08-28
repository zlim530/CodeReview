using System.Threading.Tasks;
using Abp.Application.Services;
using LearningAbpDemo.Authorization.Accounts.Dto;

namespace LearningAbpDemo.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
