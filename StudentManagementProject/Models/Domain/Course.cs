namespace StudentManagementProject.Models.Domain
{
    public class Course
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
