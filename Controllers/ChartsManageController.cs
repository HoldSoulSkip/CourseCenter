using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseCenter.Controllers
{
    public class ChartsManageController : Controller
    {
        //
        // GET: /ChartsManage/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 显示所有的课程的各种信息、
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ChartsIndex() {
            return View();
        }

    }
}
