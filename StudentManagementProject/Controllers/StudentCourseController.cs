using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementProject.Models;
using StudentManagementProject.Models.Domain;
using StudentManagementProject.Repositories;
using System.Security.Claims;

namespace StudentManagementProject.Controllers
{
    [Authorize]
    public class StudentCourseController : Controller
    {
        private readonly IBaseRepository<StudentCourse> baseRepository;
        private readonly IBaseRepository<Student> studentRepository;
        private readonly IBaseRepository<Course> courseRepository;  
        public StudentCourseController(IBaseRepository<StudentCourse> _baseRepository, IBaseRepository<Student> _studentRepository, IBaseRepository<Course> _courseRepository)
        {
            baseRepository= _baseRepository;   
            studentRepository=_studentRepository;
            courseRepository= _courseRepository;
        }
        //Method written to show courses taken and not taken by the student
        public IActionResult Index(int id)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != id.ToString()&&!User.IsInRole("Admin"))
            {
                return RedirectToAction("AccessDenied","Login");
            }
            var courseList = courseRepository.GetAll();
            StudentCourseViewModel studentCourseViewModel = new StudentCourseViewModel();
            studentCourseViewModel.Course = courseList;
            studentCourseViewModel.StudentCourse = baseRepository.GetAll().Where(x=>x.StudentId==id).ToList();
            studentCourseViewModel.StudentId=id;    
            return View(studentCourseViewModel);
        }
        //Method written List all Student & Course matchings 
        public IActionResult Students()
        {
            if (!User.IsInRole("Admin"))
            {
                string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var studentList = studentRepository.GetEntity().Include(s => s.StudentCourses).ThenInclude(c => c.Course).Where(v=>v.Id.ToString()==id).ToList();
                return View(studentList);
            }
            else
            {
                var studentList = studentRepository.GetEntity().Include(s => s.StudentCourses).ThenInclude(c => c.Course).ToList();
                return View(studentList);
            }
        }
        //Add course method
        [HttpGet]
        public IActionResult Add(int id, string courseId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != id.ToString() && !User.IsInRole("Admin"))
            {
                return RedirectToAction("AccessDenied", "Login");
            }
            StudentCourse studentCourse = new StudentCourse();
            studentCourse.StudentId = id;
            studentCourse.CourseId = courseId;
            baseRepository.Add(studentCourse);
            return RedirectToAction("Index","StudentCourse",new {@id=id });
        }
        //Delete course method
        [HttpGet]
        public IActionResult Delete(int id,int studentCourseId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != id.ToString() && !User.IsInRole("Admin"))
            {
                return RedirectToAction("AccessDenied", "Login");
            }
            baseRepository.Delete(studentCourseId);
            return RedirectToAction("Index", "StudentCourse", new { @id = id });
        }
    }
}
