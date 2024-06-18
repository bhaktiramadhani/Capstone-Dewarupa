using CapstoneDewarupa.Core;
using CapstoneDewarupa.Data;
using CapstoneDewarupa.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapstoneDewarupa.Controllers
{
    public class AdminController : Controller
    {
        private readonly CourseContext context;
        private readonly IWebHostEnvironment environment;
        public AdminController(CourseContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var courses = await context.Courses.OrderByDescending(date => date.CreateDate).ToListAsync();
            return View(courses);
        }
        [Authorize]
        public IActionResult Tambah()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Tambah(TambahCourse tambahCourse)
        {
            if (tambahCourse.Image == null)
            {
                ModelState.AddModelError("Image", "Image is required");
            }

            /*if (!ModelState.IsValid)
            {
                return View(tambahCourse);
            }*/

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(tambahCourse.Image!.FileName);

            string imageFullPath = environment.WebRootPath + "/assets/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                tambahCourse.Image.CopyTo(stream);
            }

            Course course = new Course()
            {
                Name = tambahCourse.Name,
                Description = tambahCourse.Description,
                Image = newFileName,
                Slug = StringUtilities.GenerateSlug(tambahCourse.Name),
                CreateDate = DateTime.Now,
                Category = tambahCourse.Category
            };

            context.Courses.Add(course);
            context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }
        public IActionResult Edit(int param)
        {
            // param is id
            var course = context.Courses.Find(param);

            if (course == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            var editCourse = new TambahCourse()
            {
                Name = course.Name,
                Description = course.Description,
                Category = course.Category,
                Slug = StringUtilities.GenerateSlug(course.Name),
                CreateDate = course.CreateDate
            };

            ViewData["CourseId"] = course.CourseId;
            ViewData["Image"] = course.Image;
            ViewData["CreateDate"] = course.CreateDate.ToString("YYYY/MM/DD HH:mm");
            return View(editCourse);
        }

        [HttpPost]
        public IActionResult Edit(int param, TambahCourse tambahCourse)
        {
            // param is id
            var course = context.Courses.Find(param);

            if (course == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            string newFileName = course.Image;
            if (tambahCourse.Image != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(tambahCourse.Image!.FileName);

                string imageFullPath = environment.WebRootPath + "/assets/images/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    tambahCourse.Image.CopyTo(stream);
                }

                string oldImagePath = environment.WebRootPath + "/assets/images/" + course.Image;
                System.IO.File.Delete(oldImagePath);
            }

            course.Name = tambahCourse.Name;
            course.Description = tambahCourse.Description;
            course.Category = tambahCourse.Category;
            course.Slug = tambahCourse.Name.Replace(" ", "-").ToLower();
            course.Image = newFileName;

            context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Hapus(int param)
        {
            // param is id
            var course = context.Courses.Find(param);

            if (course == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            context.Courses.Remove(course);
            context.SaveChanges();

            string oldImagePath = environment.WebRootPath + "/assets/images/" + course.Image;
            System.IO.File.Delete(oldImagePath);

            return RedirectToAction("Index", "Admin");
        }
    }
}
