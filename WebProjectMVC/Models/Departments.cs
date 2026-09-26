using System.ComponentModel.DataAnnotations;

namespace WebProjectMVC.Models
{
    public class Department
    {
        [Key]
        public int slno { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        public string Deptname { get; set; } = string.Empty;
    }
}