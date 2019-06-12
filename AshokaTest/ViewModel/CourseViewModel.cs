using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AshokaTest.ViewModel
{
    public class CourseViewModel
    {
        public int CourseID { get; set; }
        public string Category { get; set; }
        public string CousreCode { get; set; }
        public string CourseName { get; set; }
        public int Capicity { get; set; }
        public bool IsSelected { get; set; } 
        public RuleViewModel Rules { get; set; }
    }
}