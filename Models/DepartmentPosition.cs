using HRM.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRM.Models
{
    public class DepartmentPosition : EntityBase
    {
        [Required]
        public string Type { get; set; }
    }
}
