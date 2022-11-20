namespace StudentManagementProject.Models.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }

    }
}
