using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StudentManagementProject.Models.Domain;
using System.Reflection.Emit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StudentManagementProject.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedUsers(builder);
            SeedCourses(builder);
            SeedRoles(builder);
            SeedUserRoles(builder);
            builder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.Id });
            builder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);
            builder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);
            builder.Entity<StudentCourse>().Property(p => p.Id).HasColumnType("int").UseIdentityColumn().IsRequired();
        }

        private void SeedUsers(ModelBuilder builder)
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            User user = new User() { Id = "A1", Name = "Tuğba", Surname = "Aydın", Email = "tugba.aydinn.94@gmail.com", Role = "Admin", PasswordHash = passwordHasher.HashPassword(null, "Admin*123"), NormalizedEmail = "tugba.aydinn.94@gmail.com", UserName = "tugba.aydinn.94@gmail.com", NormalizedUserName = "tugba.aydinn.94@gmail.com" };
            builder.Entity<User>().HasData(user);
        }
        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "1", Name = "Admin", NormalizedName = "Admin", ConcurrencyStamp = "1" },
                        new IdentityRole() { Id = "2", Name = "User", NormalizedName = "User", ConcurrencyStamp = "2" });
        }
        private void SeedCourses(ModelBuilder builder)
        {
            builder.Entity<Course>().HasData(
                new Course() { Id = "CSI101", Name = "Introduction to Computer Science" },
                         new Course() { Id = "CSI102", Name = "Algorithms" },
                         new Course() { Id = "MAT101", Name = "Calculus" },
                         new Course() { Id = "PHY101", Name = "Physics" }
                );
        }
        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    UserId = "A1",
                    RoleId = "1"
                }
                );
        }
    }
}
