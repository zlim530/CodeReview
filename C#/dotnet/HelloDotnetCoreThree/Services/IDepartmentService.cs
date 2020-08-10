using HelloDotnetCoreThree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloDotnetCoreThree.Services {
    public interface IDepartmentService {
        
        // 获得全部的部门
        Task<IEnumerable<Department>> GetAll();

        // 通过 Id 获取部门
        Task<Department> GetById(int id);

        // 获得公司的总体情况
        Task<CompanySummary> GetCompanySummary();

        // 添加一个部门
        Task Add(Department department);
    }
}
