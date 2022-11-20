using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagementProject.Models;
using StudentManagementProject.Models.Domain;
using StudentManagementProject.Repositories;
using System.Data;

namespace StudentManagementProject.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IBaseRepository<Student> baseRepository;
        private readonly IBaseRepository<User> userRepository;
        private readonly IBaseRepository<IdentityUserRole<string>> roleRepository;
        private readonly IMapper mapper;
        public StudentController(IBaseRepository<Student> _baseRepository, IBaseRepository<User> _userRepository, IBaseRepository<IdentityUserRole<string>> _roleRepository,
            IMapper _mapper)
        {
            baseRepository = _baseRepository;
            userRepository = _userRepository;
            roleRepository= _roleRepository;    
            mapper= _mapper;
        }
        // The method by which all students are taken 
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var studentList=baseRepository.GetAll();    
            return View(studentList);
        }
        //Method that brings student details
        [Authorize]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var student = baseRepository.GetById(id);
            return View(student);
        }
        // Student update method (view)
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = baseRepository.GetById(id);
            return View(student);
        }
        // Student update method (db proccess)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update([FromForm]Student student)
        {
            baseRepository.Update(student);
            return RedirectToAction("Index", "Student");
        }
        // Student delete method (db process)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id) {
            baseRepository.Delete(id);
            return RedirectToAction("Index","Student");
        }
        // Student add method (view)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add() {
            Random rnd= new Random();
            StudentViewModel studentViewModel = new StudentViewModel();
            studentViewModel.Password = rnd.Next(1000, 9999).ToString();
            return View(studentViewModel);
        }
        // Student add method (db process)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create([FromForm]StudentViewModel studentViewModel) {
            Student student = mapper.Map<Student>(studentViewModel);
            User user= mapper.Map<User>(studentViewModel);
            baseRepository.Add(student);
            user.Id =student.Id.ToString();
            userRepository.Add(user);
            roleRepository.Add(new IdentityUserRole<string>() { UserId=user.Id.ToString(),RoleId="2"});
            return RedirectToAction("Index","Student");
        }
    }
}
