using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CourseCenter.Models;
using CourseCenter.Common;
// 所有的首页显示的东西都在这个controller,里面
namespace CourseCenter.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/


        /// <summary>
        /// 显示首页 --图片还有其他内容
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Common.Skip]
        public ActionResult ShowIndex()
        {
            return View();
        }

        /// <summary>
        /// 显示注册的页面内容
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Common.Skip]
        public ActionResult Register()
        {
            return View();

        }

        [HttpPost]
        [Common.Skip]
        public ActionResult Register(StudentInfo userModel)
        {
            userModel.Authority = "0";
            ModelHelpers mHelp = new ModelHelpers();
            mHelp.Add<StudentInfo>(userModel);
            CourseCenter.Common.TakeCookie.SetCookie("userId", userModel.Id.ToString());
            return RedirectToAction("StudentIndex", "Home");

        }


        /// <summary>
        /// 显示首页的优秀的作品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Common.Skip]
        public ActionResult GoodStuWork() {
            return View();
        }

        /// <summary>
        /// 显示优秀作品的内页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Common.Skip]
        public ActionResult GoodWorkDetail() {
            
            return View();
        }

    }
}
