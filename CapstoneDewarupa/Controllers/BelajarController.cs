using CapstoneDewarupa.Data;
using CapstoneDewarupa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CapstoneDewarupa.Controllers
{
    public class BelajarController : Controller
    {
        private readonly CourseContext context;
        private readonly ILogger<BelajarController> _logger;
        public BelajarController(CourseContext context, ILogger<BelajarController> logger)
        {
            this.context = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var courses = await context.Courses.OrderByDescending(date => date.CreateDate).ToListAsync();

            var tutorialsCount = await context.Courses.CountAsync(course => course.Category == "Tutorial");
            var webinarsCount = await context.Courses.CountAsync(course => course.Category == "Webinar");
            var blogsCount = await context.Courses.CountAsync(course => course.Category == "Blog");

            ViewData["TutorialsCount"] = tutorialsCount;
            ViewData["WebinarsCount"] = webinarsCount;
            ViewData["BlogsCount"] = blogsCount;
            return View(courses);
        }

        public async Task<IActionResult> Tutorial(string param)
        {
            // param is slug
            bool checkSlug = string.IsNullOrEmpty(param);
            if (checkSlug)
            {
                // Jika slug kosong, tampilkan view utama yaitu tutorial.cshtml
                var tutorials = await context.Courses
                                              .Where(course => course.Category == "Tutorial")
                                              .OrderByDescending(course => course.CreateDate)
                                              .ToListAsync();
                return View("Tutorial", tutorials);
            }
            else
            {
                // Jika slug tidak kosong, tampilkan view detail.cshtml
                var tutorial = await context.Courses
                                             .Where(course => course.Category == "Tutorial" && course.Slug == param)
                                             .FirstOrDefaultAsync();
                if (tutorial == null)
                {
                    // Tutorial tidak ditemukan, tampilkan halaman 404 atau tindakan lainnya
                    return NotFound();
                }
                return View("Detail", tutorial);
            }
        }

        public async Task<IActionResult> Webinar(string param)
        {
            // param is slug
            bool checkSlug = string.IsNullOrEmpty(param);
            if (checkSlug)
            {
                var webinars = await context.Courses
                                             .Where(course => course.Category == "Webinar")
                                             .OrderByDescending(course => course.CreateDate)
                                             .ToListAsync();
                return View("Webinar", webinars);
            }
            else
            {
                var webinar = await context.Courses
                                            .Where(course => course.Category == "Webinar" && course.Slug == param)
                                            .FirstOrDefaultAsync();
                if (webinar == null)
                {
                    return NotFound();
                }
                return View("Detail", webinar);
            }
        }

        public async Task<IActionResult> Blog(string param)
        {
            // param is slug
            bool checkSlug = string.IsNullOrEmpty(param);
            if (checkSlug)
            {
                var blogs = await context.Courses
                                          .Where(course => course.Category == "Blog")
                                          .OrderByDescending(course => course.CreateDate)
                                          .ToListAsync();
                return View("Blog", blogs);
            }
            else
            {
                var blog = await context.Courses
                                         .Where(course => course.Category == "Blog" && course.Slug == param)
                                         .FirstOrDefaultAsync();
                if (blog == null)
                {
                    return NotFound();
                }
                return View("Detail", blog);
            }
        }

        public async Task<List<Course>> GetRandomCoursesAsync(int param)
        {
            // param is random
            var randomCrouses = await context.Courses
                .OrderBy(course => Guid.NewGuid()) // Mengacak secara acak menggunakan GUID
                .Take(param)
                .ToListAsync();

            System.Console.WriteLine(randomCrouses.Count);
            return randomCrouses;
        }
    }
}
