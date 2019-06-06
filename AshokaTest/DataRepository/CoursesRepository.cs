using AshokaTest.Database;
using AshokaTest.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AshokaTest.DataRepository
{
    public class CoursesRepository
    {
        AshokaTestEntities entities = null;
        public CoursesRepository()
        {
            entities = new AshokaTestEntities();
        }

        public List<CourseViewModel> GetAllCourses()
        {
            List<CourseViewModel> courseViewModels = (from course in entities.Courses
                                                      select new CourseViewModel
                                                      {
                                                          CourseID = course.CourseID,
                                                          CousreCode = course.CousreCode,
                                                          CourseName = course.CourseName,
                                                          Category = course.Category,
                                                          Capicity = course.Capicity
                                                      }).ToList();
            return courseViewModels;
        }

        internal int GetCourseCapicity(string CousreCode)
        {
            int Capicity = (from cap in entities.Courses.Where(x => x.CousreCode == CousreCode) select cap.Capicity).FirstOrDefault();
            return Capicity;
        }

        public CourseRuleViewModel GetCourseByCategory(string Category)
        {
            RulesRepository rulesRepository = new RulesRepository();
            RuleViewModel ruleViewModel = rulesRepository.GetRuleDetailsByCategory(Category);
            List<CourseViewModel> courseViewModels = (from course in entities.Courses
                                                      select new CourseViewModel
                                                      {
                                                          CourseID = course.CourseID,
                                                          CousreCode = course.CousreCode,
                                                          CourseName = course.CourseName,
                                                          Category = course.Category,
                                                          Capicity = course.Capicity,
                                                      }).Where(x => x.Category == Category).ToList();

            CourseRuleViewModel courseRuleViewModel = new CourseRuleViewModel();
            courseRuleViewModel.CourseList = courseViewModels;
            courseRuleViewModel.Rule = ruleViewModel;
            return courseRuleViewModel;
        }

    }
}