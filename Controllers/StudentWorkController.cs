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
    public class StudentWorkController : Controller
    {
        DBEntities db = new DBEntities();
        ModelHelpers mHelp = new ModelHelpers();
        Guid studentId = new Guid(TakeCookie.GetCookie("userid"));
        /// <summary>
        /// 获取学生所有的课程
        /// </summary>
        /// <returns></returns>
        public ActionResult WorksIndex()
        {
            List<Course> listCourse = db.Course.SqlQuery("select * from Courses cr join Stu_Course sc on cr.Id=sc.CourseId and sc.StudentId=@Id", new SqlParameter("@Id",studentId)).ToList();
            ViewBag.listCourse = listCourse;
            return View();
        }


        /// <summary>
        /// 返回学生电子学档
        /// </summary>
        /// <param name="courseid"></param>
        /// <param name="moduleTag"></param>
        /// <returns></returns>
        public ActionResult WorksDetail(int courseid, string moduleTag)
        {
            ViewBag.courseid = courseid;
            //-------------------------------------------返回学生的最后得分-------------------------------------------------
            int mTag = 1;//如果是第一次其他页面请求过来，mTag的值为1，则默认显示该课程的第一个模块
            CouScore cScore = null;
            if (!string.IsNullOrEmpty(moduleTag))
            {
                mTag = Convert.ToInt32(moduleTag);

            }
            cScore = db.CouScore.Where(cs => cs.CourseId == courseid && cs.ModuleTag == mTag).FirstOrDefault();
            ViewBag.cScore = cScore;
            //--------------------------------------返回课程某一模块的详细----------------------------------------------------------
            Module module = db.Module.Where(m => m.CourseId == courseid && m.ModuleTag == mTag).FirstOrDefault();
            ViewBag.module = module;

            //----------------------------------------------返回学生对某一模块的作业----------------------------------------------------
            StudentWork sWork = db.StudentWork.Where(sw => sw.CourseId == courseid && sw.StudentId == studentId && sw.ModuleTag == mTag).FirstOrDefault();
            ViewBag.sWork = sWork;
            //---------------------------------------返回某一模块的考试题目 学生的答案 及评分-------------------------------------
            if (module != null)
            {

                List<Erecord> listErecord = mHelp.SqlQuery<Erecord>("select pq.QTitle,psa.Answer,psa.AnswerScore from PaperStudentAnswers as psa join PaperQuestions as pq on psa.QuestionId=pq.Id where psa.MouduleTag=@mTag and StudentId=@studentId", new SqlParameter[] { new SqlParameter("@mTag", mTag), new SqlParameter("@studentId", studentId) }).ToList();
                ViewBag.listErecord = listErecord;
            }
            //--------------------------------------------------------------------------------------------------------------------
            return View();
        }

    }

    /// <summary>
    /// 这是一个viewmodel ，整合的时候可以单独放到一个文件夹。
    /// </summary>
    public class Erecord
    {
        public string QTitle { get; set; }
        public string Answer { get; set; }
        public double AnswerScore { get; set; }
    }
}
