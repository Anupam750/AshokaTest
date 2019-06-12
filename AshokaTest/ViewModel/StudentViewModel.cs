using AshokaTest.DataRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AshokaTest.ViewModel
{
    public class StudentViewModel
    {
        public int StudentID { get; set; }
        [Required(ErrorMessage = "Please entet name")]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Please enter  email")]
        [Display(Name = "Student Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Remote("isStudentEmailExist", "Student", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        public string StudentEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, ErrorMessage = "Must be between 5 and 20 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string StudentPassword { get; set; }


        //[Display(Name = "Confirm Password")]
        //[Required(ErrorMessage = "Confirm Password is required")]
        //[StringLength(20, ErrorMessage = "Must be between 5 and 20 characters", MinimumLength = 5)]
        //[DataType(DataType.Password)]
        //[Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }

        public List<SelectListItem> StatusItems { get; set; } = new List<SelectListItem>();
        public string Status { get; set; }

        public List<SelectListItem> RulesList { get; set; } = new List<SelectListItem>();
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public string Rule { get; set; }

        public RuleViewModel RuleDetails { get; set; } = new RuleViewModel();

        public bool IsActive { get; set; }

        [Display(Name = "Course")]
        //[Required(ErrorMessage = "Course is required")]
        public string[] CoursesID { get; set; }
        public List<CourseViewModel> CoursesList { get; set; } = new List<CourseViewModel>();


        public string[] SelectedCoursesID { get; set; }
        public List<CourseViewModel> SelectedCoursesList { get; set; } = new List<CourseViewModel>();


        public StudentViewModel()
        {
            StatusItems.Add(new SelectListItem
            {
                Text = "Confirmed",
                Value = "Confirmed"
            });
            StatusItems.Add(new SelectListItem
            {
                Text = "Waiting",
                Value = "Waiting"
            });

            //get Courses list
            CoursesRepository coursesRepository = new CoursesRepository();
            CoursesList = coursesRepository.GetAllCourses();

            RulesRepository rulesRepository = new RulesRepository();
            RulesList = rulesRepository.GetAllRules();

        }
    }
}