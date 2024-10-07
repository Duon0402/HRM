using HRM.Models;

namespace HRM.Data.ViewModels
{
    public class EmployeeVM
    {
        public EmployeeVM()
        {
            Employee = new Employee();
            Employees = new List<Employee>();
            Departments = new List<Department>();
            Positions = new List<Position>();
        }
        public Employee Employee { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Position> Positions { get; set; }
    }
}
