using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseCenter.Controllers
{
    public class StudentWorkController : Controller
    {
        //
        // GET: /StudentWork/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult WorksIndex() {

            return View();
        }
    }
}
