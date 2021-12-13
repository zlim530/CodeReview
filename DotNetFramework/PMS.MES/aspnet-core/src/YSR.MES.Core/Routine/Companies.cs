using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using YSR.MES.Common.Attributes;
using YSR.MES.Common.CommonEnum;

namespace YSR.MES.Routine
{
    [Table("Companies")]
    [TableName("公司信息表", "Companies", ContextTypeEnum.RoutineDbContext)]
    public class Companies : Entity<Guid>
    {
        public string Name { get; set; }

        public string Introduction { get; set; }
    }
}
