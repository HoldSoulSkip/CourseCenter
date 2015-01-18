using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class StudentWork
    {
        public int Id { set; get; } //模块id
        public  Guid StudentId { set; get; } //学生Id
        public string StudentAccount { set; get; } //学生Account
        public int CourseId { set; get; } //课程Id
        public int ModuleTag { set; get; }//模块标示符代表是某一类型的模块：例如1, 模块一，2,模块二，3,模块三
        public string WorkContent { set; get; }//作业内容
        public string WorkFilePath { set; get; }//模块附件地址 存在服务器的位置
        public string WorkTime { set; get; } //模块截止时间
        //public double TeacherScore
    }
}