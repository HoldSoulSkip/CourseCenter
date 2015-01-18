using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class PaperQuestion//模块习题表
    {
        public int Id { get; set; }
        public int CourseId { set; get; } //课程Id
        public int ModuleTag { set; get; }//模块标示符代表是某一类型的模块：例如1, 模块一，2,模块二，3,模块三
        public int MouduleId { get; set; }//模块Id
        public int QuestionType { get; set; }//问题类型暂定为三种0单选，1多选，2填空
        public string QTitle { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string E { get; set; }
        public string F { get; set; }
        public string Answer { get; set; }
    }
}