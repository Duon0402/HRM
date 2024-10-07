using HRM.Models;

namespace HRM.Data.ViewModels
{
    public class EmpSalaryVM
    {
        public EmpSalaryVM()
        {
            Employees = new List<Employee>();
            EmpSalaries = new List<EmpSalary>();
            EmpSalary = new EmpSalary();
        }

        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<EmpSalary> EmpSalaries { get; set; }
        public EmpSalary EmpSalary { get; set; }
    }
}
