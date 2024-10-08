using HRM.Models;

namespace HRM.Data.ViewModels
{
    public class EmployeeVM
    {
        public EmployeeVM()
        {
            Employee = new Employee();
            Employees = new List<Employee>();
            Departments = new List<DepartmentPosition>();
            Positions = new List<DepartmentPosition>();
        }
        public Employee Employee { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<DepartmentPosition> Departments { get; set; }
        public IEnumerable<DepartmentPosition> Positions { get; set; }
    }
}
