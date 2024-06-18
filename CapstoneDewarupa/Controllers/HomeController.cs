using CapstoneDewarupa.Data;
using CapstoneDewarupa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CapstoneDewarupa.Controllers
{
    public class HomeController : Controller
    {
        private readonly CourseContext context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(CourseContext context, ILogger<HomeController> logger)
        {
            this.context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var tutorials = await context.Courses.Where(course => course.Category == "Tutorial").OrderByDescending(date => date.CreateDate).Take(3).ToListAsync();
            var webinars = await context.Courses.Where(course => course.Category == "Webinar").OrderByDescending(date => date.CreateDate).Take(3).ToListAsync();
            var blogs = await context.Courses.Where(course => course.Category == "Blog").OrderByDescending(date => date.CreateDate).Take(3).ToListAsync();

            var tutorialsCount = await context.Courses.CountAsync(course => course.Category == "Tutorial");
            var webinarsCount = await context.Courses.CountAsync(course => course.Category == "Webinar");
            var blogsCount = await context.Courses.CountAsync(course => course.Category == "Blog");

            ViewData["Tutorials"] = tutorials.Any() ? (object)tutorials : "tutorial tidak ada";
            ViewData["Webinars"] = webinars.Any() ? (object)webinars : "webinar tidak ada";
            ViewData["Blogs"] = blogs.Any() ? (object)blogs : "blog tidak ada";
            ViewData["TutorialsCount"] = tutorialsCount;
            ViewData["WebinarsCount"] = webinarsCount;
            ViewData["BlogsCount"] = blogsCount;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
