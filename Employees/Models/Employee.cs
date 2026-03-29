using System.ComponentModel.DataAnnotations;

namespace Employees.Models

{
    public class Employee
    {
        public int Id { get; set; }
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
