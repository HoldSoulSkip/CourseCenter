using CourseCenter.Common;
using CourseCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseCenter.Controllers
{
    public class AdminScoreController : Controller
    {
        DBEntities db = new DBEntities();

        public ActionResult GetStudentScore()
        {
            Guid studentId = new Guid(Request.QueryString["SId"]);
            int courseId = Convert.ToInt32(Request.QueryString["CId"]);

            List<CouScore> listCs = db.CouScore.Where(cs => cs.CourseId == courseId && cs.StudentId == studentId).OrderBy(cs => cs.ModuleTag).ToList();
            ViewBag.listCs = listCs;
            string courseName = db.Course.Where(c => c.Id == courseId).FirstOrDefault().CourseName;
            ViewBag.courseName = courseName;
            return View();
        }

    }
}
