using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id  {set;  get;    }//主键Id
        public string Account { set; get; } //账号，学号，工号，或者登录邮箱
        public string Pwd { set; get; }//密码
        public string Authority { set; get; }//权限
        public string Sex { set; get; }//性别
        public string UserName { set; get; } //姓名
    }
}