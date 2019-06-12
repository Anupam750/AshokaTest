using AshokaTest.DataRepository;
using AshokaTest.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AshokaTest.Controllers
{
    public class ReviewStudentController : Controller
    {
        StudentRepository studentRepository = null;
        public ReviewStudentController()
        {
            studentRepository = new StudentRepository();
        }

        // GET: ReviewDetails
        public ActionResult ReviewDetails(int ? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentViewModel student = new StudentViewModel();
            StudentViewModel studentRecord = studentRepository.GetStudentData(ID);
            studentRecord.RulesList = student.RulesList;
            studentRecord.StatusItems = student.StatusItems;
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(studentRecord);
        }
    }
}