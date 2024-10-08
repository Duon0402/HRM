using HRM.Models;

namespace HRM.Data.ViewModels
{
    public class PositionVM
    {
        public PositionVM()
        {
            Position = new DepartmentPosition();
            Positions = new List<DepartmentPosition>();
        }
        public DepartmentPosition Position { get; set; }
        public IEnumerable<DepartmentPosition> Positions { get; set; }
    }
}
