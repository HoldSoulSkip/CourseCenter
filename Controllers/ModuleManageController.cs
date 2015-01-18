using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CourseCenter.Models;

namespace CourseCenter.Controllers
{
    public class ModuleManageController : Controller
    {

        CourseCenter.Models.DBEntities db = new Models.DBEntities();
        Guid teacherId = new Guid(Common.TakeCookie.GetCookie("userid"));

        #region 显示莫个课程的 每一个模块+ModuleView
        /// <summary>
        /// 
        /// 代码要求为
        /// 通过Flag的不同，获取到不同的页面
        /// 同时获取到Module，显示在不同页面中，修改的时候，显示内容
        /// </summary>
        /// <returns></returns>
        public ActionResult ModuleView()
        {
            string CId = Request.QueryString["CId"];

            ViewBag.CId = CId;
            int CourseId = Convert.ToInt32(CId);
            Course course = db.Course.Where(c => c.Id == CourseId).FirstOrDefault();
            ViewBag.Pagecourse = course;

            string id = Request.QueryString["id"];
            if (id != null)
            {
                int ModuleId = Convert.ToInt32(id);//获得模块的id
                Module module = db.Module.Where(m => m.Id == ModuleId).FirstOrDefault();
                ViewBag.Pagemodule = module;
                ViewBag.ModuleId = id;
            }
            string moduleTag = Request.QueryString["flag"];
            if (moduleTag.Equals("1"))
            {
                return View("ModuleView1");
            }
            else
                if (moduleTag.Equals("2"))
                {
                    return View("ModuleView2");
                }
                else
                    if (moduleTag.Equals("3"))
                    {
                        return View("ModuleView3");
                    }
                    else
                        if (moduleTag.Equals("4"))
                        {
                            return View("ModuleView4");
                        }
                        else
                        {
                            return View("ModuleView5");
                        }
        }
        #endregion

        #region 教师添加课程模块内容+AddModule
        /// <summary>
        /// 增加模块的方法的复用，显示成功以后 加一个标识符显示为OK
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult AddModule(FormCollection form)
        {
            int moduleTag = Convert.ToInt32(Request.QueryString["moduleTag"]);
            int CId = Convert.ToInt32(form["CourseId"]);
            //int ModuleId = Convert.ToInt32(form["ModuleId"]); ///完成修改
            HttpPostedFileBase hpfb = Request.Files["teacherUpLoad"];
            string filePath = "";
            try
            {
                if (hpfb.ContentLength > 0)
                {
                    Common.UpLoad upLoad = new Common.UpLoad();
                    filePath = upLoad.TeacherSaveFile(hpfb);
                }

                Module module = db.Module.Where(m => m.ModuleTag == moduleTag && m.CourseId == CId).FirstOrDefault();
                //如果可以查询到module 说明模块已存在，即是修改，否则是建立新的模块
                if (module == null)
                {
                    module = new Module()
                    {
                        ModuleTag = moduleTag,
                        DeadlineTime = form["DeadlineTime"],
                        ModuleContent = form["ModuleContent"],
                        ModuleTitle = form["ModuleTitle"],
                        CourseId = CId,
                        ModuleFilePath = filePath
                    };
                    module = db.Module.Add(module);
                    int moduleId = module.Id;
                    string[] keys = form.AllKeys;
                    if (form["SelectQ1"] != "" && form["SelectQ1"] != null)
                    {
                        for (int i = 1; i <= 10; i++)//默认只能有十个选择
                        {
                            if (keys.Contains<string>("SelectQ" + i))
                            {
                                PaperQuestion question = new PaperQuestion()
                                {
                                    QTitle = form["SelectQ" + i],
                                    QuestionType = int.Parse(form["type" + i]),
                                    A = form["SelectQ" + i + "A"],
                                    B = form["SelectQ" + i + "B"],
                                    C = form["SelectQ" + i + "C"],
                                    D = form["SelectQ" + i + "D"],
                                    Answer = form["SelectAnswer" + i],
                                    MouduleId = moduleId,
                                    CourseId = CId,
                                    ModuleTag = moduleTag
                                };
                                db.PaperQuestion.Add(question);
                            }
                            else
                                break;
                        }
                    }
                    if (form["BlankQ1"] != "" && form["BlankQ1"] != null)
                    {
                        for (int i = 1; i <= 5; i++)//默认只能有五个填空
                        {
                            if (keys.Contains<string>("BlankQ" + i))
                            {
                                PaperQuestion question = new PaperQuestion()
                                {
                                    QTitle = form["BlankQ" + i],
                                    Answer = form["BlankAnswer" + i],
                                    QuestionType=2,
                                    MouduleId = moduleId,
                                    CourseId = CId,
                                    ModuleTag = moduleTag
                                };
                                db.PaperQuestion.Add(question);
                            }
                            else
                                break;
                        }
                    }
                }
                //修改模块信息
                else
                {
                    //todo ---------修改试题!!!!
                    module.DeadlineTime = form["DeadlineTime"];
                    module.ModuleContent = form["ModuleContent"];
                    module.ModuleTitle = form["ModuleTitle"];
                    if (!string.IsNullOrEmpty(filePath))
                    { module.ModuleFilePath = filePath; }
                }

                db.SaveChanges();

            }
            catch (Exception)
            {
                ViewBag.TagMSG = "Error";
                throw;
            }

            ViewBag.TagMSG = "OK";


            return RedirectToAction("CoursesDetail", "CourseManage", new { id = CId });
        }
        #endregion

    }
}
