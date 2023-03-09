using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET: Courses
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel()
            {
                Categories = _dbContext.Category.ToList()
            };

            return View(viewModel);
        }

        //POST: Courses
        [Authorize]
        [HttpPost]
        public ActionResult Create(CourseViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Categories = _dbContext.Category.ToList();
                return View("Create", model);
            }

            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = model.GetDateTime(),
                CategoryId = model.Category,
                Place = model.Place
            };
            _dbContext.Course.Add(course);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}