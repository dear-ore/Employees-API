using System.ComponentModel.DataAnnotations;

namespace Employees.Models.DTOs
{
    public class EmployeeResponseDto
    {
        public int Id { get; set; } 
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string JobTitle { get; set; }
    }
}
