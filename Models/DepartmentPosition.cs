using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM.Models
{
    public class DepartmentPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        [Required]
        public int PositionId { get; set; }
        public Position? Position { get; set; }

        [Required]
        public string CreateBy { get; set; }
        public DateTime CreateAt { get; set; }  = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeleteAt { get; set; }

        public Employee? Employee { get; set; }
    }
}
