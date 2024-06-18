using CapstoneDewarupa.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CapstoneDewarupa.Data
{
    public class CourseContext : IdentityDbContext
    {
        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    CourseId = 1,
                    Name = "Introduction to Programming",
                    Description = "Learn the basics of programming in this introductory course.",
                    Image = "intro-programming.jpg",
                    Category = "Tutorial",
                    Slug = "introduction-to-programming",
                    CreateDate = new DateTime(2024, 6, 13, 10, 30, 0)
        },
                new Course
                {
                    CourseId = 2,
                    Name = "Web Development",
                    Description = "Learn how to build websites using HTML, CSS, and JavaScript.",
                    Image = "web-development.jpg",
                    Category = "Tutorial",
                    Slug = "web-development",
                    CreateDate = new DateTime(2024, 6, 13, 10, 30, 0)
        },
                new Course
                {
                    CourseId = 3,
                    Name = "Data Science",
                    Description = "Learn how to analyze and visualize data using Python and R.",
                    Image = "data-science.jpg",
                    Category = "Webinar",
                    Slug = "data-science",
                    CreateDate = new DateTime(2024, 6, 13, 10, 30, 0)
        },
                new Course
                {
                    CourseId = 4,
                    Name = "Mobile App Development",
                    Description = "Learn how to build mobile apps for iOS and Android devices.",
                    Image = "mobile-app-development.jpg",
                    Category = "Blog",
                    Slug = "mobile-app-development",
                    CreateDate = new DateTime(2024, 6, 13, 10, 30, 0)
                },
                new Course
                {
                    CourseId = 5,
                    Name = "Cybersecurity",
                    Description = "Learn how to protect computer systems and networks from cyber attacks.",
                    Image = "cybersecurity.jpg",
                    Category = "Blog",
                    Slug = "cybersecurity",
                    CreateDate = new DateTime(2024, 6, 13, 10, 30, 0)
                }
                                                                                                      );
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }

    }
}
