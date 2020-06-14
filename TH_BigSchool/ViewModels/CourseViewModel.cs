using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TH_BigSchool.Models;

namespace TH_BigSchool.ViewModels
{
    public class CourseViewModel
        
    {
        public int Id { get; set; }
        [Required]
        public string Place { get; set; }
        [Required]
        [FutureDate]
        public string Date { get; set; }
        [Required]
        [ValidTime]
        public string Time { get; set; }
        [Required]
        public byte Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
        public IEnumerable<Course> UpCommingCourses { get; set; }
        public IEnumerable<ApplicationUser> LectureFollowing { get; set; }
        public bool ShowAction { get; set; }
        public string Heading { get; set; }
        public string Action
        {
            get { return (Id != 0) ? "Update" : "Create"; }
        }

    }
}