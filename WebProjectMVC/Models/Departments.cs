using System.ComponentModel.DataAnnotations;

namespace WebProjectMVC.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        public string Name { get; set; } = string.Empty;
    }
}