using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CourseCenter.Common;
using CourseCenter.Models;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace CourseCenter.Controllers
{
    public class StudentGroupController : Controller
    {
        Guid studentId = new Guid(TakeCookie.GetCookie("userid"));
        DBEntities db = new DBEntities();
        ModelHelpers mHelp = new ModelHelpers();

        #region 学生小助首页+GroupIndex
        /// <summary>
        /// 学生进入小组后看到的页面，默认显示最近加入课程对应的小组的所有Title
        /// </summary>
        /// <returns></returns>
        public ActionResult GroupIndex()
        {
            List<Course> listCourse = db.Course.SqlQuery("select * from Courses cr  join Stu_Course sc on sc.CourseId=cr.Id and sc.StudentId=@id ", new SqlParameter("@id", studentId)).OrderByDescending(s => s.BeginTime).ToList();
            ViewBag.listCourse = listCourse;
            if (listCourse.Count>0)
            {
                int courseId = (listCourse[0] as Course).Id;
                List<CourseGroupTitle> listCgt = db.CourseGroupTitle.Where(cg => cg.CourseId == courseId).ToList();
                ViewBag.courseGroupTitle = listCgt;
            }
            return View();
        }
        #endregion

        #region 给前台Ajax返回json数据， 显示其他某个小组/课程 讨论的Title+ChangeCourseGroup
        public string ChangeCourseGroup(int id)
        {
            int courseId = id;
            List<CourseGroupTitle> listCgt = db.CourseGroupTitle.Where(cg => cg.CourseId == courseId).ToList();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonStr = jss.Serialize(listCgt);
            return jsonStr;
        }
        #endregion

        #region 显示某个小组，某个title，的具体内容 和所有回复+GroupDetail
        public ActionResult GroupDetail(int id)
        {
            CourseGroupTitle Ctg = db.CourseGroupTitle.Where(c => c.Id == id).FirstOrDefault();
            ViewBag.ctg = Ctg;
            List<TitleContent> listTitleContent = db.TitleContent.Where(tc => tc.CourseGroupTitleId == id).OrderBy(t => t.FromDate).ToList();
            return View(listTitleContent);
        }
        #endregion

        #region 学生针对某个课程 新建讨论title+AddGroup
        [ValidateInput(false)]
        public ActionResult AddGroup(FormCollection form)
        {
            StudentInfo sInfo = db.StudentInfo.Where(s => s.Id == studentId).FirstOrDefault();
            CourseGroupTitle Cgt = new CourseGroupTitle();
            Cgt.CourseGroupCreatId = sInfo.Id;
            Cgt.CourseGroupCreatAcount = sInfo.Account;
            Cgt.CourseGroupCreatName = form["Title"];
            Cgt.Content = form["Content"];
            Cgt.CourseId = Convert.ToInt32(form["teacher"]);
            Cgt.CreatTime = DateTime.Now.ToLongDateString() + DateTime.Now.ToShortTimeString();
            mHelp.Add<CourseGroupTitle>(Cgt);
            return RedirectToAction("GroupIndex");
        }
        #endregion

        #region 回复帖子+AddReply
        [ValidateInput(false)]
        public ActionResult  AddReply(FormCollection form)
        {
            StudentInfo sInfo = db.StudentInfo.Where(s => s.Id == studentId).FirstOrDefault();
            int id=Convert.ToInt32(form["GroupID"]);
            TitleContent tContent = new TitleContent()
            {
                Content = form["Content"],
                CourseGroupTitleId =id ,
                FromId = sInfo.Id,
                FromName = sInfo.UserName,
                FromAccount = sInfo.Account,
                FromDate = DateTime.Now.ToLongDateString() + DateTime.Now.ToShortTimeString()
            };

            try
            {
                mHelp.Add<TitleContent>(tContent);
            }
            catch (Exception)
            {
                TempData["msg"] = "回复失败";
                return RedirectToAction("GroupDetail", new { id = id });
            }

            return RedirectToAction("GroupDetail", new { id = id });
        } 
        #endregion

    }
}
