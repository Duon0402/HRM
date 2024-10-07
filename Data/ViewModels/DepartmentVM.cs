using HRM.Models;

namespace HRM.Data.ViewModels
{
    public class DepartmentVM
    {
        public DepartmentVM()
        {
            Department = new Department();
            Departments = new List<Department>();
        }

        public Department Department { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}
