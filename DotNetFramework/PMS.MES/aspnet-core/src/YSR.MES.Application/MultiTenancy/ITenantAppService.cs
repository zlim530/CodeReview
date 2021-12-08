using Abp.Application.Services;
using YSR.MES.MultiTenancy.Dto;

namespace YSR.MES.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

