using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CourseCenter.Models;
using CourseCenter.Common;


namespace CourseCenter.Controllers
{
    public class PersonalManageController : Controller
    {
        DBEntities db = new DBEntities();
        //数据库帮助类
        ModelHelpers modelHelp = new ModelHelpers();

        #region 修改个人信息 页面+GetPersonalInfo
        public ActionResult GetPersonalInfo()
        {
            //获取用户(教师)cookie，cookie存储是ID
            string teacherId = TakeCookie.GetCookie("userId");
            TeacherInfo teacher = db.TeacherInfo.Where(t => t.Id ==new Guid(teacherId)).FirstOrDefault();
            return View(teacher);
        } 
        #endregion

        #region 方法重载。接受页面数据，修改数据库信息，并返回添加返回结果到修改页面+GetPersonalInfo
        [HttpPost]
        public ActionResult GetPersonalInfo(TeacherInfo teacherInfo)
        {
            try
            {
                //获取用户(教师)cookie，cookie存储是ID
                string teacherId = TakeCookie.GetCookie("userId");
                teacherInfo.Id =new Guid( teacherId);
                teacherInfo.Pwd = teacherInfo.Pwd.Trim();
                if (string.IsNullOrEmpty(teacherInfo.Pwd) || teacherInfo.Pwd == "不修改就不需要输入")
                {
                    modelHelp.Modify<TeacherInfo>(teacherInfo, new string[] { "Id", "Account", "UserName", "Sex" });
                    TempData["res"] = "修改成功";
                    return RedirectToAction("GetPersonalInfo");
                }
                modelHelp.Modify<TeacherInfo>(teacherInfo, new string[] { "Id", "Account", "UserName", "Pwd", "Sex" });
                TempData["res"] = "修改成功";
            }
            catch (Exception)
            {
                TempData["res"] = "<font color='red'>修改失败，请点解cancel或刷新 后 重新输入！<font/>";
            }

            return RedirectToAction("GetPersonalInfo");

        }
        
        #endregion

        #region 教师首页回复热门博客+AddComment
        [ValidateInput(false)]
        public ActionResult AddComment(FormCollection form)
        {
            Guid teacherId = new Guid(TakeCookie.GetCookie("userId"));
            try
            {
                db.TeacherInfo.Where(s => s.Id == teacherId).FirstOrDefault();
                BlogReply br = new BlogReply
                {
                    BlogId = Convert.ToInt32(form["BlogID"]),
                    ReplyId = teacherId,
                    ReplyAccount = db.TeacherInfo.Where(s => s.Id == teacherId).FirstOrDefault().Account,
                    ReplyContent = form["Content"],
                    CreatTime = DateTime.Now.ToLongDateString() + DateTime.Now.ToShortTimeString()
                };

                modelHelp.Add<BlogReply>(br);
                return RedirectToAction("TeacherGetBlogDetail", "LoadMore", new { id = br.BlogId });
            }
            catch (Exception)
            {
                return RedirectToAction("TeacherGetBlogDetail", "LoadMore", new { id = Convert.ToInt32(form["BlogID"]) });
            }

        } 
        #endregion

    }
}
