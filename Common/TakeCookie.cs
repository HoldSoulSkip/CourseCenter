using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Common
{
    public class TakeCookie
    {
        //把用户账户写入cookie,有效期一个月
        public static void SetCookie(string info,string cookieValue)
        { 
            HttpCookie cookie = new HttpCookie(info);
            cookie.Value = cookieValue;
            cookie.Expires = DateTime.Now.AddDays(30);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        //获取用户cookie 
        public static string  GetCookie(string info)
        {
            if(HttpContext.Current.Request.Cookies[info]==null){
                return null;
            }
           string cookieValue= HttpContext.Current.Request.Cookies[info].Value;
           return cookieValue;
        }

        //删除用户cookie
        public static void DelCookie(string info)
        {
            HttpCookie cookie = new HttpCookie(info);
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        
    }
}