using System.Threading.Tasks;
using Abp.Application.Services;
using YSR.MES.Sessions.Dto;

namespace YSR.MES.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
