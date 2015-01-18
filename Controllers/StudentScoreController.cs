using CourseCenter.Common;
using CourseCenter.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseCenter.Controllers
{
    public class StudentScoreController : Controller
    {

        DBEntities db = new DBEntities();
        Guid studentId = new Guid(TakeCookie.GetCookie("userId"));
        ModelHelpers mHelp = new ModelHelpers();


        #region 学生查看不同课程是的成绩+ScoreIndex
        /// <summary>
        /// 显示学生的选中课程的成绩
        /// 根据下拉框修改
        /// </summary>
        public ActionResult ScoreIndex()
        {

            List<Course> listCourse = db.Course.SqlQuery("select * from Courses cr join Stu_Course sc on cr.Id=sc.CourseId and sc.StudentId=@Id", new SqlParameter("@Id", studentId)).ToList();
            ViewBag.listCourse = listCourse;

            string cid = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(cid))//通过点解下拉框
            {
                int courseId = Convert.ToInt32(cid);
                List<CouScore> listCs = db.CouScore.Where(cs => cs.CourseId == courseId && cs.StudentId == studentId).OrderBy(cs => cs.ModuleTag).ToList();
                ViewBag.listCs = listCs;

                string courseName = db.Course.Where(c => c.Id == courseId).FirstOrDefault().CourseName;
                ViewBag.courseName = courseName;
            }
            else//第一次进入
            {
                if (listCourse.Count > 0)
                {
                    Course course = listCourse[0] as Course;
                    int courseId = course.Id;
                    List<CouScore> listCs = db.CouScore.Where(cs => cs.CourseId == courseId && cs.StudentId == studentId).OrderBy(cs => cs.ModuleTag).ToList();
                    ViewBag.listCs = listCs;

                    string courseName = db.Course.Where(c => c.Id == courseId).FirstOrDefault().CourseName;
                    ViewBag.courseName = courseName;
                }
            }
            return View();
        } 
        #endregion




    }
}
