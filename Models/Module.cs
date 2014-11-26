using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class Module
    {
        public int Id { set; get; } //模块id
        public string ModuleTag { set; get; }//模块标示符代表是某一类型的模块：例如 模块一，模块二，模块三
        public string ModuleTagId { set; get; }//模块标示唯一的id。以便链接课程
        public string ModuleTitle { set; get; }//模块题目
        public string ModuleContent { set; get; }//模块内容
        public string ModuleFileName { set; get; }//模块附件名称
        public string ModuleFilePath { set; get; }//模块附件地址 存在服务器的位置
    }
}