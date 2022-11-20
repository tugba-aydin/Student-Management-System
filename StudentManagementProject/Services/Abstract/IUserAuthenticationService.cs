using StudentManagementProject.Models;
using StudentManagementProject.Models.Domain;

namespace StudentManagementProject.Services.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        //Task<Status> RegisterAsync(RegistrationModel model);
        //Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);
    }
}
