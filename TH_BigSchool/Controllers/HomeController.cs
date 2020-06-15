using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TH_BigSchool.Models;
using System.Data.Entity;
using TH_BigSchool.ViewModels;

namespace TH_BigSchool.Controllers
{
    public class HomeController : Controller
    {
        readonly private ApplicationDbContext _dbContext;
        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upComingCourses = _dbContext.Courses
                .Where(x => x.IsCanceled == false
                )
                .Include(c => c.Lecturer)
                 .Include(c => c.Category)
                 .Where(c => c.DateTime > DateTime.Now);
            var viewModel = new CourseViewModel
            {
                UpCommingCourses = upComingCourses,
                ShowAction = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}