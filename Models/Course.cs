using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class Course
    {
        public int Id { set; get; } //id
        public string CourseName { set; get; }//课程名称
        public int CourseAttend { set; get; } //应该参加人数
        public Guid TeacherId { set; get; } //开课教师id
        public string BeginTime { set; get; } //课程开始时间
        public string EndTime { set; get; } //课程结束时间
        public int RealAttend { set; get; } //实际参加人数
        public int CourseStatus { set; get; } //课程状态 ，已开通，已关闭
        public string KeyValue { set; get; } //课程关键字
        public string RelativeSub { set; get; } //关联学科
        public int CourseLevel { set; get; } //课程 层级，一般分为 小中高三层


    }
}
