using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class Admin :User
    {
        public int NewsId { set; get; } //新闻表ID
    }
}