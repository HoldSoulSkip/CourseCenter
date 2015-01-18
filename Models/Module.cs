using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    /// <summary>
    /// 这个课程模块表，具体内容不分比较复杂，可能还需要添加许多字段。
    /// </summary>
    public class Module
    {
        public int Id { set; get; } //模块id
        public int CourseId { set; get; } //课程Id
        public int ModuleTag { set; get; }//模块标示符代表是某一类型的模块：例如1, 模块一，2,模块二，3,模块三
        public string ModuleTitle { set; get; }//模块题目
        public string ModuleContent { set; get; }//模块内容
        public string ModuleFilePath { set; get; }//模块附件地址 存在服务器的位置
        public string DeadlineTime { set; get; } //模块截止时间
    }
}