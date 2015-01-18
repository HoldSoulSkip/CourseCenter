using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CourseCenter.Models;
using CourseCenter.Common;
using System.Web.Script.Serialization;

namespace CourseCenter.Controllers
{
    public class AdminEidtController : Controller
    {
        DBEntities db = new DBEntities();
        Guid adminId = new Guid(TakeCookie.GetCookie("userid"));

        #region 管理员增加教师+AddTeacher
        public ActionResult AddTeacher(TeacherInfo teacherInfo)
        {
            teacherInfo.Authority = "1";
            db.Set<TeacherInfo>().Add(teacherInfo);
            db.SaveChanges();
            return RedirectToAction("AdminTeacherView", "AdminCourse");
        }
        #endregion

        #region 显示所有的小组信息+ShowAllGroups
        public ActionResult ShowAllGroups()
        {
            List<CourseGroupTitle> listCgt = db.CourseGroupTitle.OrderBy(g => g.CreatTime).ToList();
            ViewBag.listCgt = listCgt;
            return View();
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

        #region 管理员删除整个帖子+DeleteTitle
        public string DeleteTitle(int id)
        {
            CourseGroupTitle Cgt = new CourseGroupTitle() { Id = id };
            ModelHelpers Mhelp = new ModelHelpers();
            Mhelp.Del<CourseGroupTitle>(Cgt);
            List<CourseGroupTitle> listCgt = db.CourseGroupTitle.OrderBy(g => g.CreatTime).ToList();
            ViewBag.listCgt = listCgt;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonStr = jss.Serialize(listCgt);
            return jsonStr;
           
        } 
        #endregion

        #region 管理员回复帖子+AddReply
        [ValidateInput(false)]
        public ActionResult AddReply(FormCollection form)
        {
            Admin aInfo = db.Admin.Where(s => s.Id == adminId).FirstOrDefault();
            int id = Convert.ToInt32(form["GroupID"]);
            TitleContent tContent = new TitleContent()
            {
                Content = form["Content"],
                CourseGroupTitleId = id,
                FromId = aInfo.Id,
                FromName = aInfo.UserName,
                FromAccount = aInfo.Account,
                FromDate = DateTime.Now.ToLongDateString()+DateTime.Now.ToShortTimeString()
            };

            ModelHelpers mHelp = new ModelHelpers();

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
