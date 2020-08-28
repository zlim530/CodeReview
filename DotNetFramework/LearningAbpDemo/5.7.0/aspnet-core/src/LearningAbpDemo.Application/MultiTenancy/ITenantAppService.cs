using Abp.Application.Services;
using LearningAbpDemo.MultiTenancy.Dto;

namespace LearningAbpDemo.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

