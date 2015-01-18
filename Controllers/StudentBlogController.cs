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
    public class StudentBlogController : Controller
    {
        DBEntities db = new DBEntities();
        //获取用户(学生)cookie，cookie存储是ID
        Guid studentId = new Guid(TakeCookie.GetCookie("userId"));
        //数据库帮助类
        ModelHelpers mHelp = new ModelHelpers();


        #region 显示所有自己的博客+ShowAllBolgList
        public ActionResult ShowAllBolgList()
        {
            List<BlogTitle> listBlog = db.BlogTitle.Where(b => b.CreatId == studentId).ToList();
            return View(listBlog);
        }
        #endregion

        #region 添加博客页面+AddNewBlog
        /// <summary>
        /// 知识一个跳转的效果
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddNewBlog()
        {
            List<Course> listCourse = db.Course.SqlQuery("select * from Courses cr join Stu_Course sc on cr.Id=sc.CourseId and sc.StudentId=@Id", new SqlParameter("@Id", studentId)).ToList();
            ViewBag.Course = listCourse;
            return View();
        }
        #endregion

        #region  添加博客操作+AddNewBlog
        /// <summary>
        /// 发表博客，实际上应该是显示详细的
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNewBlog(FormCollection form)
        {
            StudentInfo sInfo = db.StudentInfo.Where(s => s.Id == studentId).FirstOrDefault();
            BlogTitle bt = new BlogTitle
            {
                CreatId = studentId,
                CreatAccount = sInfo.Account,
                TitleName = form["TitleName"],
                CourseId =Convert.ToInt32( form["teacher"]),
                BlogContent = form["BlogContent"],
                CreatTime = DateTime.Now.ToLongDateString() + DateTime.Now.ToShortTimeString(),
                ReadTimes = 0
            };

            mHelp.Add<BlogTitle>(bt);
            return RedirectToAction("ShowAllBolgList");
        }
        #endregion

        #region 根据Id查看博客的具体内容+BlogDetail
        /// <summary>
        /// 显示详细的页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BlogDetail(int id)
        {

            BlogTitle bt = db.BlogTitle.Where(b=>b.Id == id).FirstOrDefault();

            List<BlogReply> listBlogReply = db.BlogReply.Where(br => br.BlogId== id).OrderBy(b=>b.CreatTime).ToList();
            ViewBag.listBlogReply = listBlogReply;
            if (studentId != bt.CreatId)
            {
                AddReadTimes(id);
            }
            return View(bt);
        }
        #endregion

        #region 修改博客阅读次数+AddReadTimes
        /// <summary>
        /// 当点击非自己创建的博文后，在该博文里增加阅读数量ReadTime 
        /// </summary>
        /// <param name="blogId">博客的Id</param>
        public void AddReadTimes(int blogId)
        {
            BlogTitle bt = db.BlogTitle.Where(b => b.Id == blogId).FirstOrDefault();
            bt.ReadTimes++;
            db.SaveChanges();
        } 
        #endregion

        #region 学生在 我的博客 回复博客+AddComment
        [ValidateInput(false)]
        public ActionResult AddComment(FormCollection form)
        {
            try
            {
                db.StudentInfo.Where(s => s.Id == studentId).FirstOrDefault();
                BlogReply br = new BlogReply
                {
                    BlogId = Convert.ToInt32(form["BlogID"]),
                    ReplyId = studentId,
                    ReplyAccount = db.StudentInfo.Where(s => s.Id == studentId).FirstOrDefault().Account,
                    ReplyContent = form["Content"],
                    CreatTime = DateTime.Now.ToLongDateString() + DateTime.Now.ToShortTimeString()
                };
                mHelp.Add<BlogReply>(br);
                return RedirectToAction("BlogDetail", new { id = br.BlogId });
            }
            catch (Exception)
            {
                return RedirectToAction("BlogDetail", new { id = Convert.ToInt32(form["BlogID"]) });
            }

        } 
        #endregion

    }
}
