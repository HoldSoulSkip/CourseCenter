using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseCenter.Models;

namespace CourseCenter.Controllers
{
    public class HomeController : Controller
    {
        //创建数据库实体对象
        private DBEntities dbEntity = new DBEntities();

        #region 登陆首页 httpget模式
        //登陆首页action 
        [HttpGet]
        [Common.Skip]
        public ActionResult Index()
        {
            return View();
        } 
        #endregion

        #region 登陆首页 处理登陆收据+httppost
        /// <summary>
        /// 登陆首页
        /// </summary>
        /// <param name="userModel">接受页面的值</param>
        /// <returns></returns>
        [HttpPost]
        [Common.Skip]
        public ActionResult Index(User userModel)
        {
            //根据页面传来的值，调用GetUser方法
            User user = GetUser(userModel);
            if (user == null)
            {
                return View();
            }
            //保存user 到cookie 
            CourseCenter.Common.TakeCookie.SetCookie("userId", user.Id.ToString());
            
            if (user.Authority == "1")
            {
                //跳转到TeacherIndex方法
                return RedirectToAction("TeacherIndex");
            }

            if (user.Authority == "2")
            {
                //跳转到AdminIndex方法
                return RedirectToAction("AdminIndex");
            }
            if (user.Authority == "0")
            {
                //跳转到AdminIndex方法
                CourseCenter.Common.TakeCookie.SetCookie("EnterTime", DateTime.Today.ToString());
                return RedirectToAction("StudentIndex");
            }
            return View();
        } 
        #endregion

        #region 教师登陆后的首页+TeacherIndex
        /// <summary>
        /// 登陆后的教师首页
        /// </summary>
        /// <returns></returns>
        public ActionResult TeacherIndex()
        {
            Guid teacherId =new Guid( Common.TakeCookie.GetCookie("userId"));
            //查询出教师个人信息 在首页显示
            TeacherInfo teacherInfo = dbEntity.TeacherInfo.Where(t => t.Id == teacherId).FirstOrDefault(); 
            ViewBag.teacherInfo = teacherInfo;
            //首次进入个人主页，显示互动 的未读 答疑私信
            List<Question> listQue = dbEntity.Question.Where(q => q.ToId == teacherId).OrderByDescending(q => q.CreateTime).ToList();
            int newStamp = listQue.Where(q => q.Flag == "未读").Count();
            ViewBag.listQue = listQue.Take(3).ToList();
            ViewBag.newStamp = newStamp;
            //显示热门博文，根据查看次数排序
            List<BlogTitle> listBlogTitle = dbEntity.BlogTitle.OrderByDescending(b => b.ReadTimes).Take(3).ToList();
            ViewBag.listBlogTitle = listBlogTitle;
            //显示热点的帖子在首页
            List<CourseGroupTitle> listCgt = dbEntity.CourseGroupTitle.OrderBy(g => dbEntity.TitleContent.Where(t => t.CourseGroupTitleId == g.Id).Count()).ToList();
            ViewBag.listCgt = listCgt.Take(3).ToList();
            return View();
        } 
        #endregion

        #region 管理员登陆后的首页+AdminIndex
        /// <summary>
        /// 管理员登陆后的首页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminIndex()
        {
            Guid adminId = new Guid(Common.TakeCookie.GetCookie("userId"));
            //查询出教师个人信息 在首页显示
            Admin adminInfo = dbEntity. Admin.Where(a => a.Id ==adminId).FirstOrDefault(); ;
            ViewBag.adminInfo = adminInfo;
            //显示热门博文，根据查看次数排序
            List<BlogTitle> listBlogTitle = dbEntity.BlogTitle.OrderByDescending(b => b.ReadTimes).Take(3).ToList();
            ViewBag.listBlogTitle = listBlogTitle;
            //显示热点的帖子在首页
            List<CourseGroupTitle> listCgt = dbEntity.CourseGroupTitle.OrderBy(g => dbEntity.TitleContent.Where(t => t.CourseGroupTitleId == g.Id).Count()).ToList();
            ViewBag.listCgt = listCgt.Take(3).ToList();
            return View();
        } 
        #endregion

        #region 学生登陆后的首页+StudentIndex
        /// <summary>
        /// 学生登陆后的首页
        /// </summary>
        /// <returns></returns>
        public ActionResult StudentIndex()
        {
            Guid StudentId =new Guid( Common.TakeCookie.GetCookie("userId"));
            //查询出学生个人信息 在首页显示
            StudentInfo studentInfo = dbEntity.StudentInfo.Where(s => s.Id ==StudentId).FirstOrDefault(); 
            ViewBag.studentInfo = studentInfo;
            //学生首次进入个人主页，显示互动 的未读 答疑私信
            List<Question> listQue = dbEntity.Question.Where(q => q.ToId == StudentId).OrderByDescending(q=>q.CreateTime).ToList();

            int newStamp = listQue.Where(q => q.Flag == "未读").Count();
            ViewBag.listQue = listQue.Take(3).ToList();
            ViewBag.newStamp = newStamp;
            //显示热门博文，根据查看次数排序
            List<BlogTitle> listBlogTitle = dbEntity.BlogTitle.OrderByDescending(b => b.ReadTimes).Take(3).ToList();
            ViewBag.listBlogTitle = listBlogTitle;
            //显示热点的帖子在首页
            List<CourseGroupTitle> listCgt = dbEntity.CourseGroupTitle.OrderBy(g => dbEntity.TitleContent.Where(t => t.CourseGroupTitleId == g.Id).Count()).ToList();
            ViewBag.listCgt = listCgt.Take(3).ToList();

            return View();
        }
        #endregion

        #region 获取USER的通用方法+GetUser
        /// <summary>
        /// 获取user的通用方法
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public User GetUser(User userModel)
        {
            User user = dbEntity.Admin.Where(a => a.Account == userModel.Account && a.Pwd == userModel.Pwd).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            user = dbEntity.TeacherInfo.Where(t => t.Account == userModel.Account && t.Pwd == userModel.Pwd ).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            user = dbEntity.StudentInfo.Where(s => s.Account == userModel.Account && s.Pwd == userModel.Pwd ).FirstOrDefault();
            return user;
        } 
        #endregion

        #region 退出登录+loginOut
        public ActionResult loginOut()
        {
            Common.TakeCookie.DelCookie("userId");
            return View("Index");
        } 
        #endregion
    }
}
