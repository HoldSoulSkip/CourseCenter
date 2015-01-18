using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CourseCenter.Models;
using CourseCenter.Common;
using System.Data.SqlClient;

namespace CourseCenter.Controllers
{
    public class AdminCourseController : Controller
    {

        DBEntities db = new DBEntities();

        #region 查看所有课程信息+AdminCoureseView
        /// <summary>
        /// 管理员 管理教师页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminCoureseView()
        {
            //获取用户(教师)cookie，cookie存储是ID
            Guid adminId =new Guid(TakeCookie.GetCookie("userId"));
            List<Course> listCourse = db.Course.ToList();
            ViewBag.Course = listCourse;
            return View();
        } 
        #endregion

        #region 显示课程详细信息+AdminCoursesDetail
        /// <summary>
        /// id，为课程的id，这个是管理员的部分
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AdminCoursesDetail(string[] id)
        {
            int courseId = Convert.ToInt32(id[0]); //从用户请求中取出课程的id
            //完成课程的信息显示
            List<Module> listModule = db.Module.Where(m => m.CourseId == courseId).ToList();
            if (listModule != null)
            {
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
            }
            return View();
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
            return RedirectToAction("AdminCoureseView");
        } 
        #endregion

        #region  管理员查看所有教师信息+AdminTeacherView
        /// <summary>
        /// 管理员查看所有教师信心
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminTeacherView()
        {
            List<TeacherInfo> listTeacher = db.TeacherInfo.ToList();
            ViewBag.TeacherInfo = listTeacher;
            return View();
        } 
        #endregion

        #region 管理员根据教师Id ，查看该教师的课程信息+AdminTeacherCoursesView
        public ActionResult AdminTeacherCoursesView( Guid id)
        {
            List<Course> listCourse = db.Course.Where(c => c.TeacherId == id).ToList();
            ViewBag.Course = listCourse;
            return View();
        } 
        #endregion

        #region 根据点击下拉框中的值 返回该课程的学生情况+GetCourse
        public ActionResult GetCourse(int id)
        {
            //查询所有的课程，根据实际参加人数排序
            List<Course> listCourse = db.Course.OrderBy(c => c.CourseAttend).ToList();
            ViewBag.listCourse = listCourse;
            List<StudentInfo> listStudentInfoOfIndex = db.StudentInfo.SqlQuery("select * from StudentInfoes where Id in (select StudentId from Stu_Course where Stu_Course.CourseId=@id )", new SqlParameter("@Id", id)).ToList();
            ViewBag.listStudentInfoOfIndex = listStudentInfoOfIndex;
            return View("AdminLearnesView");
        } 
        #endregion

        #region 查询出所有的课程放在下列框中，表格中默认显示第一门课程的学生情况+AdminLearnesView
        public ActionResult AdminLearnesView()
        {
            //查询出所有的课程，根据实际参加人数排序
            List<Course> listCourse = db.Course.OrderBy(c => c.CourseAttend).ToList();
            ViewBag.listCourse = listCourse;
            if (listCourse.Count > 0)
            {
                //页面默认显示第一门课程 的学生情况
                Course firsrCourse = listCourse[0] as Course;
                List<StudentInfo> listStudentInfoOfIndex = db.StudentInfo.SqlQuery("select * from StudentInfoes where Id in (select StudentId from Stu_Course where Stu_Course.CourseId=@Id )", new SqlParameter("@Id", firsrCourse.Id)).ToList();
                ViewBag.listStudentInfoOfIndex = listStudentInfoOfIndex;
                ViewBag.CourseId = firsrCourse.Id;
            }
            return View();
        } 
        #endregion

        #region 修改个人信息 页面+GetPersonalInfo
        public ActionResult GetPersonalInfo()
        {
            //获取用户(教师)cookie，cookie存储是ID
            string adminId = TakeCookie.GetCookie("userId");
            Admin adminInfo = db.Admin.Where(a => a.Id ==new Guid(adminId)).FirstOrDefault();
            return View(adminInfo);
        }
        #endregion

        #region 方法重载。接受页面数据，修改数据库信息，并返回添加返回结果到修改页面+GetPersonalInfo
        [HttpPost]
        public ActionResult GetPersonalInfo(Admin adminInfo)
        {
            //数据库帮助类
           ModelHelpers modelHelp = new ModelHelpers();
            //获取用户(教师)cookie，cookie存储是ID
            try
            {
                string adminId = TakeCookie.GetCookie("userId");
                adminInfo.Id =new Guid( adminId);
                adminInfo.Pwd = adminInfo.Pwd.Trim();
                if (string.IsNullOrEmpty(adminInfo.Pwd) || adminInfo.Pwd == "不修改就不需要输入")
                {
                    modelHelp.Modify<Admin>(adminInfo, new string[] { "Id", "Account", "UserName", "Sex" });
                    TempData["res"] = "修改成功";
                    return RedirectToAction("GetPersonalInfo");
                }
                modelHelp.Modify<Admin>(adminInfo, new string[] { "Id", "Account", "UserName", "Pwd", "Sex" });
                TempData["res"] = "修改成功";
            }
            catch (Exception)
            {
                TempData["res"] = "<font color='red'>修改失败，请点解cancel或刷新 后 重新输入！<font/>";
            }

            return RedirectToAction("GetPersonalInfo");

        }

        #endregion
    }
}
