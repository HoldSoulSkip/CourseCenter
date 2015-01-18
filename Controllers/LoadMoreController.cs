using CourseCenter.Common;
using CourseCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseCenter.Controllers
{
    public class LoadMoreController : Controller
    {
        DBEntities db = new DBEntities();
        //数据库帮助类
        ModelHelpers mHelp = new ModelHelpers();
        Guid userId = new Guid(TakeCookie.GetCookie("userId"));


        #region 学生查看所有博客+StudentGetMoreHotBlog
        public ActionResult StudentGetMoreHotBlog()
        {
            List<BlogTitle> listBlog = db.BlogTitle.OrderByDescending(b => b.ReadTimes).ToList();
            return View(listBlog);
        }


        #endregion

        #region 学生查看博客内容+StudentGetBlogDetail
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult StudentGetBlogDetail(int id)
        {

            BlogTitle bt = db.BlogTitle.Where(b => b.Id == id).FirstOrDefault();

            List<BlogReply> listBlogReply = db.BlogReply.Where(br => br.BlogId == id).OrderBy(b => b.CreatTime).ToList();
            ViewBag.listBlogReply = listBlogReply;
            if (userId != bt.CreatId)
            {
                AddReadTimes(id);
            }
            return View(bt);
        } 
        #endregion

        #region 学生首页回复热门博客+AddComment
        [ValidateInput(false)]
        public ActionResult AddComment(FormCollection form)
        {
            try
            {
                db.StudentInfo.Where(s => s.Id == userId).FirstOrDefault();
                BlogReply br = new BlogReply
                {
                    BlogId = Convert.ToInt32(form["BlogID"]),
                    ReplyId = userId,
                    ReplyAccount = db.StudentInfo.Where(s => s.Id == userId).FirstOrDefault().Account,
                    ReplyContent = form["Content"],
                    CreatTime = DateTime.Now.ToLongDateString() + DateTime.Now.ToShortTimeString()
                };
                mHelp.Add<BlogReply>(br);
                return RedirectToAction("StudentGetBlogDetail", new { id = br.BlogId });
            }
            catch (Exception)
            {
                return RedirectToAction("StudentGetBlogDetail", new { id = Convert.ToInt32(form["BlogID"]) });
            }

        } 
        #endregion

        #region 学生在首页查看小组详细+StudentGetGroupDetail
        /// <summary>
        /// 显示热门帖子的详细内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult StudentGetGroupDetail(int id)
        {
            CourseGroupTitle Ctg = db.CourseGroupTitle.Where(c => c.Id == id).FirstOrDefault();
            ViewBag.ctg = Ctg;
            List<TitleContent> listTitleContent = db.TitleContent.Where(tc => tc.CourseGroupTitleId == id).OrderBy(t => t.FromDate).ToList();
            return View(listTitleContent);
        } 
        #endregion

        #region 学生首页查看所有的小组+StudentGetMoreGroup
        /// <summary>
        /// 显示更多的热门的消息
        /// </summary>
        /// <returns></returns>
        public ActionResult StudentGetMoreGroup()
        {
            List<CourseGroupTitle> listCgt = db.CourseGroupTitle.OrderBy(g => db.TitleContent.Where(t => t.CourseGroupTitleId == g.Id).Count()).ToList();
            ViewBag.listCgt = listCgt;
            return View();
        } 
        #endregion

        #region 学生首页回复小组讨论+StudentIndexAddReply
        [ValidateInput(false)]
        public ActionResult StudentIndexAddReply(FormCollection form)
        {
            StudentInfo sInfo = db.StudentInfo.Where(s => s.Id == userId).FirstOrDefault();
            int id = Convert.ToInt32(form["GroupID"]);
            TitleContent tContent = new TitleContent()
            {
                Content = form["Content"],
                CourseGroupTitleId = id,
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
                return RedirectToAction("StudentGetGroupDetail", new { id = id });
            }

            return RedirectToAction("StudentGetGroupDetail", new { id = id });
        } 
        #endregion

        #region 教师查看所有博客+TeacherGetMoreBlog
        /// <summary>
        /// 教师显示更多热点的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult TeacherGetMoreBlog()
        {
            List<BlogTitle> listBlog = db.BlogTitle.OrderByDescending(b => b.ReadTimes).ToList();
            return View(listBlog);
        }
        #endregion

        #region 教师查看博客内容+TeacherGetBlogDetail
        /// <summary>
        /// 教师获得详细的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult TeacherGetBlogDetail(int id)
        {
            BlogTitle bt = db.BlogTitle.Where(b => b.Id == id).FirstOrDefault();

            List<BlogReply> listBlogReply = db.BlogReply.Where(br => br.BlogId == id).OrderBy(b => b.CreatTime).ToList();
            ViewBag.listBlogReply = listBlogReply;

            AddReadTimes(id);

            return View(bt);
        }
        #endregion

        #region 教师首页查看所有小组+TeacherGetMoreGroup
        public ActionResult TeacherGetMoreGroup()
        {

            List<CourseGroupTitle> listCgt = db.CourseGroupTitle.OrderBy(g => db.TitleContent.Where(t => t.CourseGroupTitleId == g.Id).Count()).ToList();
            ViewBag.listCgt = listCgt;
            return View();
        } 
        #endregion

        #region 教师首页查看小组讨论详细+TeacherGetGroupDetail
        public ActionResult TeacherGetGroupDetail(int id)
        {

            CourseGroupTitle Ctg = db.CourseGroupTitle.Where(c => c.Id == id).FirstOrDefault();
            ViewBag.ctg = Ctg;
            List<TitleContent> listTitleContent = db.TitleContent.Where(tc => tc.CourseGroupTitleId == id).OrderBy(t => t.FromDate).ToList();
            return View(listTitleContent);
        } 
        #endregion

        #region 教师回复小组讨论+TeacherIndexAddReply
        /// <summary>
        /// 教师回复信息
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult TeacherIndexAddReply(FormCollection form)
        {
            TeacherInfo sInfo = db.TeacherInfo.Where(t => t.Id == userId).FirstOrDefault();
            int id = Convert.ToInt32(form["GroupID"]);
            TitleContent tContent = new TitleContent()
            {
                Content = form["Content"],
                CourseGroupTitleId = id,
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
                return RedirectToAction("TeacherGetGroupDetail", new { id = id });
            }

            return RedirectToAction("TeacherGetGroupDetail", new { id = id });

        } 
        #endregion


        #region 管理员查看所有博客+AdminGetMoreBlog
        /// <summary>
        /// 管理员显示更多热点的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminGetMoreBlog()
        {
            List<BlogTitle> listBlog = db.BlogTitle.OrderByDescending(b => b.ReadTimes).ToList();
            return View(listBlog);
        }
        #endregion

        #region 管理员查看博客内容+AdminGetBlogDetail
        /// <summary>
        /// 管理员获得详细的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminGetBlogDetail(int id)
        {
            BlogTitle bt = db.BlogTitle.Where(b => b.Id == id).FirstOrDefault();

            List<BlogReply> listBlogReply = db.BlogReply.Where(br => br.BlogId == id).OrderBy(b => b.CreatTime).ToList();
            ViewBag.listBlogReply = listBlogReply;

            AddReadTimes(id);

            return View(bt);
        }
        #endregion

        #region 管理员首页查看所有小组+AdminGetMoreGroup
        public ActionResult AdminGetMoreGroup()
        {

            List<CourseGroupTitle> listCgt = db.CourseGroupTitle.OrderBy(g => db.TitleContent.Where(t => t.CourseGroupTitleId == g.Id).Count()).ToList();
            ViewBag.listCgt = listCgt;
            return View();
        }
        #endregion

        #region 管理员首页查看小组讨论详细+AdminGetGroupDetail
        public ActionResult AdminGetGroupDetail(int id)
        {

            CourseGroupTitle Ctg = db.CourseGroupTitle.Where(c => c.Id == id).FirstOrDefault();
            ViewBag.ctg = Ctg;
            List<TitleContent> listTitleContent = db.TitleContent.Where(tc => tc.CourseGroupTitleId == id).OrderBy(t => t.FromDate).ToList();
            return View(listTitleContent);
        }
        #endregion

        #region 管理员回复小组讨论+AdminIndexAddReply
        /// <summary>
        /// 管理员回复信息
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult AdminIndexAddReply(FormCollection form)
        {
            Admin  aInfo = db.Admin.Where(t => t.Id == userId).FirstOrDefault();
            int id = Convert.ToInt32(form["GroupID"]);
            TitleContent tContent = new TitleContent()
            {
                Content = form["Content"],
                CourseGroupTitleId = id,
                FromId = aInfo.Id,
                FromName = aInfo.UserName,
                FromAccount = aInfo.Account,
                FromDate = DateTime.Now.ToLongDateString() + DateTime.Now.ToShortTimeString()
            };

            try
            {
                mHelp.Add<TitleContent>(tContent);
            }
            catch (Exception)
            {
                TempData["msg"] = "回复失败";
                return RedirectToAction("AdminGetGroupDetail", new { id = id });
            }

            return RedirectToAction("AdminGetGroupDetail", new { id = id });

        }
        #endregion

        #region 管理员首页回复热门博客+AddComment
        [ValidateInput(false)]
        public ActionResult AdminAddComment(FormCollection form)
        {
            Guid adminId = new Guid(TakeCookie.GetCookie("userId"));
            try
            {
                db.Admin.Where(s => s.Id == adminId).FirstOrDefault();
                BlogReply br = new BlogReply
                {
                    BlogId = Convert.ToInt32(form["BlogID"]),
                    ReplyId = adminId,
                    ReplyAccount = db.Admin.Where(s => s.Id == adminId).FirstOrDefault().Account,
                    ReplyContent = form["Content"],
                    CreatTime = DateTime.Now.ToLongDateString() + DateTime.Now.ToShortTimeString()
                };

                mHelp.Add<BlogReply>(br);
                return RedirectToAction("AdminGetBlogDetail", "LoadMore", new { id = br.BlogId });
            }
            catch (Exception)
            {
                return RedirectToAction("AdminGetBlogDetail", "LoadMore", new { id = Convert.ToInt32(form["BlogID"]) });
            }

        }
        #endregion



        #region 修改博客的阅读次数+AddReadTimes
        public void AddReadTimes(int blogId)
        {
            BlogTitle bt = db.BlogTitle.Where(b => b.Id == blogId).FirstOrDefault();
            bt.ReadTimes++;
            db.SaveChanges();
        }
        #endregion

    }
}
