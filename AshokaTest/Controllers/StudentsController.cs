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
                var isEmailExist = studentRepository.IsEmailExist(student.StudentEmail);
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
                studentRepository.SaveStudent(student);

                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentViewModel student = new StudentViewModel();
            StudentViewModel studentRecord = studentRepository.GetStudentData(id);
            studentRecord.RulesList = student.RulesList;
            studentRecord.CoursesList = student.CoursesList;
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
        public ActionResult Edit([Bind(Include = "StudentID,StudentName,StudentEmail,StudentPassword,Status,IsActive")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
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
            return RedirectToAction("Index");
        }


        public ActionResult GetCourses(string Category)
        {
            if (string.IsNullOrEmpty(Category))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //get Courses list
            CoursesRepository coursesRepository = new CoursesRepository();
            CourseRuleViewModel Courses = coursesRepository.GetCourseByCategory(Category);
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
