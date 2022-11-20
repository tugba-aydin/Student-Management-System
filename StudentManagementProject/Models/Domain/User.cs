
using Microsoft.AspNetCore.Identity;

namespace StudentManagementProject.Models.Domain
{
    public class User:IdentityUser
    {
        public string Role { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
