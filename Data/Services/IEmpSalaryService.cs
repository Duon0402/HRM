using HRM.Data.Base;
using HRM.Models;

namespace HRM.Data.Services
{
    public interface IEmpSalaryService : IEntityBaseRepository<EmpSalary>
    {
        Task<bool> ValidateDate(EmpSalary empSalary);
    }
}
