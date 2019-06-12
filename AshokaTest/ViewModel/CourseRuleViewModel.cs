using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AshokaTest.ViewModel
{
    public class CourseRuleViewModel
    {
        public RuleViewModel Rule { get; set; }
        public List<CourseViewModel> CourseList { get; set; }
        public List<CourseViewModel> SelectedCourseList { get; set; }
    }
}