using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class User
    {
        public int Id { set; get; } //id
        public string Account { set; get; } //账号
        public string Pwd { set; get; }//密码
        public string Authority { set; get; }//权限
        public string Sex { set; get; }//性别
        public string UserName { set; get; } //姓名
    }
}