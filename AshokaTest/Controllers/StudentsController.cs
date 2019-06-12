using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AshokaTest.Database;
using AshokaTest.DataRepository;
using AshokaTest.ViewModel;

namespace AshokaTest.Controllers
{
    public class StudentsController : Controller
    {
        StudentRepository studentRepository = null;
        private AshokaTestEntities db = new AshokaTestEntities();
        public StudentsController()
        {
            studentRepository = new StudentRepository();
        }
        // GET: Students
        public ActionResult Index()
        {
            //Get 
            return View(db.Students.ToList());
        }
        
        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        [HttpGet]
        public ActionResult Create()
        {
            StudentViewModel student = new StudentViewModel();
            return View(student);
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form, StudentViewModel student)
        {

            if (!string.IsNullOrEmpty(student.StudentEmail))
            {
                var isEmailExist = studentRepository.IsEmailExist(student.StudentEmail,0);
                if (isEmailExist)
                {
                    ModelState.AddModelError("StudentEmail", "Email already exist, Please choose another one.");
                }
            }

            if (string.IsNullOrEmpty(Convert.ToString(form["CourseIDs"])))
            {
                ModelState.AddModelError("CourseIDs", "Please select courses....");
            }


            if (ModelState.IsValid)
            {
                string CourseIDs = Convert.ToString(form["CourseIDs"]).TrimEnd(',');
                student.CoursesID = CourseIDs.Split(',');
                StudentRepository studentRepository = new StudentRepository();
               int StudentID= studentRepository.SaveStudent(student);
                return RedirectToAction("ReviewDetails", "ReviewStudent", new { ID = StudentID });
            }
            return View(student);
        }

        // GET: Students/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentViewModel student = new StudentViewModel();
            StudentViewModel studentRecord = studentRepository.GetStudentData(id);
            studentRecord.RulesList = student.RulesList;
            studentRecord.StatusItems = student.StatusItems;
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(studentRecord);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection form, StudentViewModel student)
        {
            StudentViewModel studentRecord = new StudentViewModel();
            if (!string.IsNullOrEmpty(student.StudentEmail))
            {
                var isEmailExist = studentRepository.IsEmailExist(student.StudentEmail, student.StudentID);
                if (isEmailExist)
                {
                    ModelState.AddModelError("StudentEmail", "Email already exist, Please choose another one.");
                }
            }

            if (string.IsNullOrEmpty(Convert.ToString(form["CourseIDs"])))
            {
                if (string.IsNullOrEmpty(Convert.ToString(form["CoursesID"])))
                {
                    ModelState.AddModelError("CoursesID", "Please select courses....");
                }
            }

            if (ModelState.IsValid)
            {
                string CourseIDs = "";
                if (string.IsNullOrEmpty(Convert.ToString(form["CourseIDs"])))
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(form["CoursesID"])))
                    {
                        CourseIDs = Convert.ToString(form["CoursesID"]).TrimEnd(',');
                    }
                }
                else
                {
                     CourseIDs = Convert.ToString(form["CourseIDs"]).TrimEnd(',');
                }
              
                student.CoursesID = CourseIDs.Split(',');
                StudentRepository studentRepository = new StudentRepository();
                int StudentID = studentRepository.SaveStudent(student);
                return RedirectToAction("ReviewDetails", "ReviewStudent", new { ID = StudentID });
            }
            else
            {
                StudentViewModel _student = new StudentViewModel();
                studentRecord = studentRepository.GetStudentData(student.StudentID);
                studentRecord.RulesList = _student.RulesList;
                studentRecord.StatusItems = _student.StatusItems;
                if (student == null)
                {
                    return HttpNotFound();
                }
            }
            return View(studentRecord);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();

            List<StudentCourseMapping> StudentCourseMappings = (from co in db.StudentCourseMappings where co.StudentID == id select co).ToList();
            db.StudentCourseMappings.RemoveRange(StudentCourseMappings);
            db.SaveChanges();
          
            return RedirectToAction("Index");
        }


        public ActionResult GetCourses(string Category, int ? StudentID)
        {
            if (string.IsNullOrEmpty(Category))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursesRepository coursesRepository = new CoursesRepository();
            CourseRuleViewModel Courses = new CourseRuleViewModel();
            if (StudentID==null)
            {
                //Create Request
                //get Courses list
                 Courses = coursesRepository.GetCourseByCategory(Category);
            }
            else
            {
                Courses = coursesRepository.GetCourseByCategory(Category, Convert.ToInt32(StudentID));
            }
           
            if (Request.IsAjaxRequest())
            {
                return PartialView("/Views/Students/_PartialCourseList.cshtml", Courses);
            }
            else
            {
                return Json(Courses, JsonRequestBehavior.AllowGet);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
