using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace YSR.MES.Routine
{
    [Table("Companies")]
    public class Companies : Entity<Guid>
    {
        public string Name { get; set; }

        public string Introduction { get; set; }
    }
}
