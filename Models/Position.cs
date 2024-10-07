using HRM.Data.Base;
using HRM.Models;

namespace HRM.Models
{
    public class Position : EntityBase
    {
        public ICollection<Employee>? Employees { get; set; }
    }
}
