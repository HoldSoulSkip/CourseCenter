using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseCenter.Common
{
    public class AuthorityAttribute : AuthorizeAttribute
	{
		/// <summary>
        /// 在执行action之前，执行OnAuthorization，检查用户身份信息。在登陆 和注册时 ，要使用skip跳过此过滤器
		/// </summary>
		/// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
          string userId=  Common.TakeCookie.GetCookie("userId");
          List<object> attr = filterContext.ActionDescriptor.GetCustomAttributes(true).ToList();
          List<object> skip = attr.Where(a => a.ToString().Contains("Skip")).ToList();

          if (string.IsNullOrEmpty(userId) && skip.Count < 1)
          {
              HttpContext.Current.Response.Redirect("/Home/Index");
          }
        }
	}
}