using StudentManagementProject.Models.Domain;

namespace StudentManagementProject.Models
{
    public class StudentCourseViewModel
    {
        public List<Course> Course { get; set; }  
        public List<StudentCourse> StudentCourse { get; set; }
        public int StudentId { get; set; }    

    }
}
