using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseCenter.Controllers
{
    public class StudentWorkController : Controller
    {
      /// <summary>
        /// 获取学生所有的课程
        /// </summary>
        /// <returns></returns>
        public ActionResult WorksIndex()
        {
            List<Course> listCourse = db.Course.SqlQuery("select * from Courses cr join Stu_Course sc on cr.Id=sc.CourseId and sc.StudentId=@Id", new SqlParameter("@Id", studentId)).ToList();
            ViewBag.listCourse = listCourse;
            return View();
        }


        public ActionResult WorksDetail() {
            return View();
        }
    }
}
