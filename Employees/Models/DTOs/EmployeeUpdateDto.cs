using System.ComponentModel.DataAnnotations;

namespace Employees.Models.DTOs
{
    public class EmployeeUpdateDto
    {
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string JobTitle { get; set; }
        public int Salary { get; set; }
    }
}
