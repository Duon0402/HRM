using System.ComponentModel.DataAnnotations;
using HRM.Data.Base;

namespace HRM.Models
{
    public class Employee : EntityBase
    {
        [Required(ErrorMessage = "DateOfBirth is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Identifier is required")]
        public string Identifier { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public int PositionId { get; set; }
        public Position? Position { get; set; }
    }
}
