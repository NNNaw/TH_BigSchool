using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TH_BigSchool.Models;
using TH_BigSchool.ViewModels;

namespace TH_BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList(),
                Heading = "Add Course"
            };
            return View(viewModel);
        }
        // GET: Courses
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var Course = new Course
            {
                //Categories = _dbContext.Categories.ToList()
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place
            };
            _dbContext.Courses.Add(Course);
            _dbContext.SaveChanges();
            //  return View(viewModel);
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Attendances
                .Where(x => x.AttendeeId == userId)
                .Select(x => x.Course)
                .Include(l => l.Lecturer)
                .Include(l => l.Category).ToList();
            var viewModel = new CourseViewModel
            {
                UpCommingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var lectureFollowing = _dbContext.Followings
                .Where(x => x.FollowerId == userId)
                .Select(x => x.Followee)
                .ToList();


            var viewModel = new CourseViewModel
            {
                LectureFollowing = lectureFollowing,
                ShowAction = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

        public ActionResult DeleteFollowing(string id)
        {
            var userId = User.Identity.GetUserId();
            var obj = _dbContext.Followings.Single(x => x.FolloweeId == id && x.FollowerId == userId);
            //Puts an entity from this table into a pending delete state and parameter is the entity which to be deleted.  
            _dbContext.Followings.Remove(obj);
            // executes the appropriate commands to implement the changes to the database  
            _dbContext.SaveChanges();
            return RedirectToAction("Following");
        }

        public ActionResult DeleteGoing(int id)
        {
            var userId = User.Identity.GetUserId();
            var obj = _dbContext.Attendances.Single(x => x.CourseId == id && x.AttendeeId == userId);
            //Puts an entity from this table into a pending delete state and parameter is the entity which to be deleted.  
            _dbContext.Attendances.Remove(obj);
            // executes the appropriate commands to implement the changes to the database  
            _dbContext.SaveChanges();
            return RedirectToAction("Attending");
        }
        [Authorize]
        public ActionResult Mine()
        {
            var userID = User.Identity.GetUserId();
            var courses = _dbContext.Courses
                .Where(x => x.LecturerId == userID && x.DateTime > DateTime.Now)
                .Include(l => l.Lecturer)
                .Include(l => l.Category).ToList();
            return View(courses);
        }
        public ActionResult EditCourse(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Courses.Single(c => c.Id == id && c.LecturerId == userId);
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList(),
                Date = course.DateTime.ToString("dd/MM/yyyy"),
                Time = course.DateTime.ToString("HH:mm"),
                Category = course.CategoryId,
                Place = course.Place,
                Heading = "Update Course",
                Id = course.Id
            };
            return View("Create", viewModel);

        }
       [Authorize]
       [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult UpdateCourse(CourseViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Courses.Single(c => c.Id == viewModel.Id && c.LecturerId == userId);
            course.Place = viewModel.Place;
            course.DateTime = viewModel.GetDateTime();
            course.CategoryId = viewModel.Category;
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
   
        public ActionResult DetailList (int id)
        {

            return View();

        }
    }
 }