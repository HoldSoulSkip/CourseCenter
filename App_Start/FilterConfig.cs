﻿using System.Web;
using System.Web.Mvc;

namespace CourseCenter
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new Common.AuthorityAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}