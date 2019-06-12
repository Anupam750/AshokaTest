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

        internal int GetStudentCourseOccupiedCapicity(string rule)
        {
            List<Cours> courseList = (from cap in entities.Courses where (cap.Category == rule) select cap).ToList();
            int occupiedStudent = 0;
            foreach (var Cours in courseList)
            {
                int countOfRegistredMapStudent = (from map in entities.StudentCourseMappings where(map.CourseCode == Cours.CousreCode) select map.CourseCode).ToList().Count();
                occupiedStudent += countOfRegistredMapStudent;
            }
            return occupiedStudent;
        }

        internal int GetCourseCategoryCapicity(string category)
        {
            int? Capicity = (from cap in entities.Rules.Where(x => x.Category == category) select cap.Capicity).FirstOrDefault();
            return Convert.ToInt32( Capicity);
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

        internal CourseRuleViewModel GetCourseByCategory(string category, int StudentID)
        {
            Student student = entities.Students.Find(StudentID);
            if (student == null)
                return new CourseRuleViewModel();


            //student those record selected...
            string[] studentCourseSelected = (from selectedCourse in entities.StudentCourseMappings where (selectedCourse.StudentID == student.StudentID) select selectedCourse.CourseCode).ToArray();
            var categoryRecord = (from map in entities.Courses where (studentCourseSelected.Contains(map.CousreCode)) select map).FirstOrDefault();
            // List<Cours> allCoursesWithCategory = (from map in entities.Courses where (map.Category == category.Category) select map).ToList();

            List<CourseViewModel> allCoursesWithCategory = (from map in entities.Courses
                                                            where (map.Category == category)
                                                            select new CourseViewModel
                                                            {
                                                                CourseID = map.CourseID,
                                                                Capicity = map.Capicity,
                                                                Category = map.Category,
                                                                CousreCode = map.CousreCode,
                                                                CourseName = map.CourseName,
                                                            }).ToList();



            List<CourseViewModel> selectedCourses = (from map in entities.Courses
                                                     where (studentCourseSelected.Contains(map.CousreCode))
                                                     select new CourseViewModel
                                                     {
                                                         CourseID = map.CourseID,
                                                         Capicity = map.Capicity,
                                                         Category = map.Category,
                                                         CousreCode = map.CousreCode,
                                                         CourseName = map.CourseName,
                                                     }).ToList();


            foreach (CourseViewModel item in allCoursesWithCategory)
            {
                var rec = (from ss in selectedCourses where ss.CousreCode == item.CousreCode select ss.CourseID).ToList();
                if (rec.Count > 0)
                {
                    item.IsSelected = true;
                }
            }


            RulesRepository rulesRepository = new RulesRepository();
            RuleViewModel ruleViewModel = rulesRepository.GetRuleDetailsByCategory(category);
            CourseRuleViewModel courseRuleViewModel = new CourseRuleViewModel();
            courseRuleViewModel.CourseList = allCoursesWithCategory;
            courseRuleViewModel.Rule = ruleViewModel;
            return courseRuleViewModel;



        }
    }
}