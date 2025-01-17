using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Routine.Api.Data;
using Routine.Api.Entities;

namespace Routine.Api.Services {
    public class CompanyRepository : ICompanyRepository {
        private readonly RoutineDbContext _routineDbContext;

        public CompanyRepository(RoutineDbContext context) {
            _routineDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId) {
            if (companyId == Guid.Empty) {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await _routineDbContext.Employees.Where(x => x.CompanyId == companyId).OrderBy(x => x.EmployeeNo).ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId) {
            if (companyId == Guid.Empty) {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employeeId == Guid.Empty) {
                throw new ArgumentNullException(nameof(employeeId));
            }
            return await _routineDbContext.Employees.Where(x => x.CompanyId == companyId && x.Id == employeeId).FirstOrDefaultAsync();
        }

        public void AddEmployee(Guid companyId, Employee employee) {
            if (companyId == Guid.Empty) {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employee == null) {
                throw new ArgumentNullException(nameof(employee));
            }
            employee.CompanyId = companyId;
            _routineDbContext.Employees.Add(employee);
        }

        public void UpdateEmployee(Employee employee) {
            _routineDbContext.Employees.Update(employee);
        }
        public void DeleteEmployee(Employee employee) {
            _routineDbContext.Remove(employee);
        }


        public async Task<IEnumerable<Company>> GetCompaniesAsync() {
            return await _routineDbContext.Companies.ToListAsync();
        }


        public async Task<Company> GetCompanyAsync(Guid companyId) {
            if (companyId == Guid.Empty) {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await _routineDbContext.Companies.FirstOrDefaultAsync(a => a.Id== companyId);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds) {
            if (companyIds == null) {
                throw new ArgumentNullException(nameof(companyIds));
            }

            return await _routineDbContext.Companies.Where(x => companyIds.Contains(x.Id)).OrderBy(a => a.Name).ToListAsync();
        }


        public void AddCompany(Company company) {
            if (company == null) {
                throw new ArgumentNullException(nameof(company));
            }

            company.Id = Guid.NewGuid();

            foreach (var employee in company.Employees) {
                employee.Id = Guid.NewGuid();
            }

            _routineDbContext.Companies.Add(company);
        }

        public void DeleteCompany(Company company) {
            if (company == null) {
                throw new ArgumentNullException(nameof(company));
            }

            _routineDbContext.Companies.Remove(company);
        }

        public void UpdateCompany(Company company) {
            _routineDbContext.Companies.Update(company);
        }


        public async Task<bool> CompanyExistsAsync(Guid companyId) {
            if (companyId == null) {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await _routineDbContext.Companies.AnyAsync(x => x.Id == companyId);
        }

        public async Task<bool> SaveAsync() {
            return (await _routineDbContext.SaveChangesAsync()) >= 0;
        }


    }
}
