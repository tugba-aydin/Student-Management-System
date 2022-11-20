using System.ComponentModel.DataAnnotations;

namespace StudentManagementProject.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
