using HRM.Models;

namespace HRM.Data.ViewModels
{
    public class PositionVM
    {
        public PositionVM()
        {
            Position = new Position();
            Positions = new List<Position>();
        }
        public Position Position { get; set; }
        public IEnumerable<Position> Positions { get; set; }
    }
}
