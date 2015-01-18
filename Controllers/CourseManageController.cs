using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CourseCenter.Models;
using CourseCenter.Common;

namespace CourseCenter.Controllers
{
    public class CourseManageController : Controller
    {

        DBEntities db = new DBEntities();
        ModelHelpers mHelper = new ModelHelpers();

        #region 根据教师ID 获取他所有的课程信息+Courses
        public ActionResult Courses()
        {
            //获取用户(教师)cookie，cookie存储是ID
            string teacherId = TakeCookie.GetCookie("userId");
            List<Course> listCourse = db.Course.Where(c => c.TeacherId == new Guid(teacherId)).ToList();
            ViewBag.Course = listCourse;
            return View();
        }
        #endregion
        // 发布课程 --内容  D大调
        public ActionResult CoursesPublish()
        {
            try
            {
                int courseId = int.Parse(Request.QueryString["id"]);
                Course course = db.Course.Where(c => c.Id == courseId).FirstOrDefault();
                Module module =db.Module.Where(c => c.CourseId == courseId && c.ModuleTag == 5).FirstOrDefault();
                if (module != null)
                {
                    course.CourseStatus = 1;
                    mHelper.Modify<Course>(course);
                }
                else
                {
                    ViewBag.ModifyError = "保存失败!因为没有添加完所有模块!";
                }
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("Courses");
        }

        #region 根据课程Id 进入课程的每一个模块。如果是新建课程 则 每个模块 都是未完成状态+CoursesDetail
        public ActionResult CoursesDetail(string[] id)
        {
            int courseId = Convert.ToInt32(id[0]); //从用户请求中取出课程的id
            ViewBag.courseId = courseId;
            List<Module> listModule = db.Module.Where(m => m.CourseId == courseId).ToList();
            //完成课程的信息显示
            foreach (var lm in listModule)
            {
                if (lm.ModuleTag == 1)
                    ViewBag.lm1 = lm;
                if (lm.ModuleTag == 2)
                    ViewBag.lm2 = lm;
                if (lm.ModuleTag == 3)
                    ViewBag.lm3 = lm;
                if (lm.ModuleTag == 4)
                    ViewBag.lm4 = lm;
                if (lm.ModuleTag == 5)
                    ViewBag.lm5 = lm;
            }

            return View();
        }
        #endregion

        #region 新建课程+CourseNew ---增加课程简介
        /// <summary>
        /// 新建课程
        /// </summary>
        /// <returns></returns>
        public ActionResult CourseNew()
        {
            return View();
        }

        public ActionResult AddNewCourse(Course course)
        {
            string teacherId = TakeCookie.GetCookie("userId");
            course.CourseStatus = 0;
            course.TeacherId = new Guid(teacherId);
            ModelHelpers mHelp = new ModelHelpers();
            mHelp.Add<Course>(course);
            return RedirectToAction("CoursesDetail", new { id = course.Id });
        }
        #endregion

        #region 删除课程+CoursesDelete
        public ActionResult CoursesDelete(string[] id)
        {
            int courseId = Convert.ToInt32(id[0]); //从用户请求中取出课程的id
            //完成课程的删除
            Course course = db.Course.Where(c => c.Id == courseId).FirstOrDefault();
            db.Set<Course>().Remove(course);
            db.SaveChanges();
            //返回Courses视图
            return RedirectToAction("Courses");
        }
        #endregion

    }
}
