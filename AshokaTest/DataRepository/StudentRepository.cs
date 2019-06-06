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

        internal void SaveStudent(StudentViewModel studentViewModel)
        {
            try
            {
                //Get Course Reserve seats....
                CoursesRepository coursesRepository = new CoursesRepository();

                foreach (var item in studentViewModel.CoursesID)
                {
                    int Capicity = coursesRepository.GetCourseCapicity(item);
                }

                Student student = new Student();
                student.StudentName = studentViewModel.StudentName;
                student.StudentPassword = studentViewModel.StudentPassword;
                student.StudentEmail = studentViewModel.StudentEmail;
               /// student.Status = studentViewModel.Status; Need to get status based on capicity of course.
                student.Status = "Confirmed";
                student.IsActive = studentViewModel.IsActive;
                entities.Students.Add(student);
                entities.SaveChanges();
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

        }

        internal bool IsEmailExist(string email)
        {
            bool record = (from student in entities.Students.Where(x => x.StudentEmail == email) select student).ToList().Count>0;
            return record;
        }

        internal StudentViewModel GetStudentData(int? id)
        {
            StudentViewModel studentViewModel = new StudentViewModel();
            Student student = entities.Students.Find(id);
            //Get Course ID
            var rec = (from map in entities.StudentCourseMappings.Where(x => x.StudentID == student.StudentID) select map).FirstOrDefault();
            //find category ID
            var category = (from map in entities.Courses.Where(x => x.CousreCode == rec.CourseCode) select map).FirstOrDefault();
            studentViewModel.StudentID = student.StudentID;
            studentViewModel.StudentName = student.StudentName;
            studentViewModel.StudentEmail = student.StudentEmail;
            studentViewModel.StudentPassword = student.StudentPassword;
            studentViewModel.Rule = category.Category;
            return studentViewModel;
        }
    }
}