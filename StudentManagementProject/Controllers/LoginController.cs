using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagementProject.Models;
using StudentManagementProject.Services.Abstract;
using StudentManagementProject.Services.Concrete;

namespace StudentManagementProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserAuthenticationService authenticationService;
        public LoginController(IUserAuthenticationService _authenticationService)
        {
            authenticationService = _authenticationService;  
        }
        // GET: LoginController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            var result = await authenticationService.LoginAsync(loginViewModel);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Student");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpGet]
        public ActionResult AccessDenied() {
            return View();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await authenticationService.LogoutAsync();
            return RedirectToAction("Index","Home");
        }

    }
}
