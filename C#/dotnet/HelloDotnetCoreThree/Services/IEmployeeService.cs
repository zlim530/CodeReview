using HelloDotnetCoreThree.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloDotnetCoreThree.Services {
    public interface IEmployeeService {

        // 添加员工
        Task Add(Employee employee);

        // 根据部门获得所有员工
        Task<IEnumerable<Employee>> GetByDepartmentId(int departmentId);

        // 开除员工
        Task<Employee> Fire(int id);
    }
}
