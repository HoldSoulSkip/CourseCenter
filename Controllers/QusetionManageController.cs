using CourseCenter.Common;
using CourseCenter.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseCenter.Controllers
{
    public class QusetionManageController : Controller
    {
        DBEntities db = new DBEntities();
        string teacherId = TakeCookie.GetCookie("userId");
        ModelHelpers mHelp = new ModelHelpers();

        #region 教师查看和教师往来的私信+QusetionCenter
        /// <summary>
        /// 答疑中心
        /// </summary>
        /// <returns></returns>
        public ActionResult QusetionCenter()
        {
            List<Question> listAllQue = db.Question.Where(q => q.ToId == new Guid(teacherId) || q.FromId == new Guid(teacherId)).ToList();
            //查看自己收到的私信
            List<Question> ToMelistQue = listAllQue.Where(l => l.ToId == new Guid(teacherId)).ToList();
            ViewBag.ToMelistQue = ToMelistQue;
            //查看自己发送的私信
            List<Question> FromMelistQue = listAllQue.Where(l => l.FromId == new Guid(teacherId)).ToList();
            ViewBag.FromMelistQue = FromMelistQue;
            return View();
        }
        #endregion

        #region 查看或回复私信+ReplyQusetion
        [ValidateInput (false)]
        public ActionResult ReplyQusetion(FormCollection  form)
        {
            Question que=new Question ();
           Guid  id=new Guid(  TakeCookie.GetCookie("userid"));
        
            que.Title = form["Title"];
            que.Content = form["Content"];
            string[] accountAndId = form["hiddenIA"].Split('#');
           que.ToId =new Guid( accountAndId[1]);
           que.ToAcconut = accountAndId[0];
           que.FromId = id;
           que.FromAccount = accountAndId[2];
           que.Flag = "未读";
           que.CreateTime = DateTime.Now.ToShortDateString();

           mHelp.Add<Question>(que);
           return RedirectToAction("QusetionCenter");
        }
        #endregion

        #region 删除私信+DelectQusetion
        public ActionResult DelectQusetion(int id)
        {
            Question que = new Question() { Id = id };
            mHelp.Del<Question>(que);
            return RedirectToAction("QusetionCenter");
        } 
        #endregion

    }
}
