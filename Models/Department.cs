using HRM.Data.Base;

namespace HRM.Models
{
    public class Department : EntityBase
    {
        public ICollection<Employee>? Employees { get; set; }

        public ICollection<DepartmentPosition>? DepartmentPositions { get; set; }
    }
}
