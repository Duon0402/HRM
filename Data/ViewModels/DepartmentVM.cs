using HRM.Models;

namespace HRM.Data.ViewModels
{
    public class DepartmentVM
    {
        public DepartmentVM()
        {
            Department = new DepartmentPosition();
            Departments = new List<DepartmentPosition>();
        }

        public DepartmentPosition Department { get; set; }
        public IEnumerable<DepartmentPosition> Departments { get; set; }
    }
}
