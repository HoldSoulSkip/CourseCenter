using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class Admin :User
    {
        public string Tb_Info { set; get; } //冗余字段
    }
}