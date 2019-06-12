using AshokaTest.Database;
using AshokaTest.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace AshokaTest.DataRepository
{
    public class StudentRepository
    {
        AshokaTestEntities entities = null;
        public StudentRepository()
        {
            entities = new AshokaTestEntities();
        }

        internal int SaveStudent(StudentViewModel studentViewModel)
        {
            int StudentID = 0;
            try
            {
                //Get Course Reserve seats....
                CoursesRepository coursesRepository = new CoursesRepository();
                int Capicity = coursesRepository.GetCourseCategoryCapicity(studentViewModel.Rule);

                //Delete old records
                List<StudentCourseMapping> studentCourseMappings = (from coursemap in entities.StudentCourseMappings where coursemap.StudentID == studentViewModel.StudentID select coursemap).ToList();
                entities.StudentCourseMappings.RemoveRange(studentCourseMappings);
                entities.SaveChanges();

                int StudentCountWithCategory = coursesRepository.GetStudentCourseOccupiedCapicity(studentViewModel.Rule);
                string status = "";
                if(StudentCountWithCategory> Capicity)
                {
                    status = "Waiting";
                }
                else
                {
                    status = "Confirmed";
                }

                if(studentViewModel.StudentID==0)
                {
                    Student student = new Student();
                    student.StudentName = studentViewModel.StudentName;
                    student.StudentPassword = studentViewModel.StudentPassword;
                    student.StudentEmail = studentViewModel.StudentEmail;
                    student.Status = status;
                    student.IsActive = studentViewModel.IsActive;
                    entities.Students.Add(student);
                    entities.SaveChanges();
                    StudentID = student.StudentID;
                    //Enter Course Mapping
                    foreach (var item in studentViewModel.CoursesID)
                    {
                        StudentCourseMapping studentMapping = new StudentCourseMapping();
                        studentMapping.StudentID = student.StudentID;
                        studentMapping.CourseCode = item;
                        entities.StudentCourseMappings.Add(studentMapping);
                        entities.SaveChanges();
                    }
                }
                else
                {
                    //Update case....
                    Student student = entities.Students.Find(studentViewModel.StudentID);

                    if (student != null)
                    {
                        student.StudentName = studentViewModel.StudentName;
                        student.StudentPassword = studentViewModel.StudentPassword;
                        student.StudentEmail = studentViewModel.StudentEmail;
                        student.Status = status;
                        student.IsActive = studentViewModel.IsActive;
                        entities.SaveChanges();
                    }

                  

                    foreach (var item in studentViewModel.CoursesID)
                    {
                        StudentCourseMapping studentMapping = new StudentCourseMapping();
                        studentMapping.StudentID = student.StudentID;
                        studentMapping.CourseCode = item;
                        entities.StudentCourseMappings.Add(studentMapping);
                        entities.SaveChanges();
                    }

                    StudentID = studentViewModel.StudentID;

                }

                
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return StudentID;
        }

        internal bool IsEmailExist(string email, int studentID)
        {
            bool record = false;

            Student _student = (from student in entities.Students.Where(x => x.StudentEmail == email) select student).FirstOrDefault();
            if (_student != null)
            {
                if(studentID==0)
                {
                    record = true;
                }
                else
                {
                    if (_student.StudentID != studentID)
                    {
                        record = true;
                    }
                }
            }
            return record;
        }

        internal StudentViewModel GetStudentData(int? id)
        {
            StudentViewModel studentViewModel = new StudentViewModel();
            Student student = entities.Students.Find(id);
            //Get Course ID
            //Find all the course which you have selceted at the time of signup
            if(student==null)
            {
                return new StudentViewModel();
            }

            List<string> studentCourseSelectedList = (from selectedCourse in entities.StudentCourseMappings where (selectedCourse.StudentID == student.StudentID) select selectedCourse.CourseCode).ToList();

            if (studentCourseSelectedList.Count > 0)
            {
                string[] studentCourseSelected = (from selectedCourse in entities.StudentCourseMappings where (selectedCourse.StudentID == student.StudentID) select selectedCourse.CourseCode).ToArray();
                var category = (from map in entities.Courses where (studentCourseSelected.Contains(map.CousreCode)) select map).FirstOrDefault();

                string CousreCode = (from selectedC in entities.StudentCourseMappings where (selectedC.StudentID == student.StudentID) select selectedC.CourseCode).FirstOrDefault();
                var _category = (from map in entities.Courses where (map.CousreCode== CousreCode) select map.Category).FirstOrDefault();


                RuleViewModel ruleViewModel = (from selectedC in entities.Rules
                                               where (selectedC.Category == _category)
                                               select new RuleViewModel
                                               {
                                                   Category = selectedC.Category,
                                                   Min = selectedC.Min,
                                                   Max = selectedC.Max
                                               }).FirstOrDefault();

                studentViewModel.RuleDetails = ruleViewModel;

                List<CourseViewModel> allCoursesWithCategory = (from map in entities.Courses
                                                                where (map.Category == category.Category)
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
              
                studentViewModel.StudentID = student.StudentID;
                studentViewModel.StudentName = student.StudentName;
                studentViewModel.StudentEmail = student.StudentEmail;
                studentViewModel.StudentPassword = student.StudentPassword;
                studentViewModel.Rule = category.Category;
                studentViewModel.CoursesList = allCoursesWithCategory;
            }
          
            return studentViewModel;
        }
    }
}