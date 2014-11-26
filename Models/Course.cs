using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class Course
    {
        public int Id { set; get; } //id
        public int CourseAttend { set; get; } //应该参加人数
        public int RealAttend { set; get; } //实际参加人数
        public int TeacherId { set; get; } //开课教师id
        public int Flag{ set; get; } //是否为优秀课程 默认为0.表示否
        public string CourseStatus { set; get; } //课程状态 ，建设中，正在上课，已关闭
        public string CourseModule { set; get; } //模块组成 #modelID#modeleID#
        public DateTime BeginTime { set; get; } //课程开始时间
        public DateTime EndTime { set; get; } //课程关闭时间
          public DateTime EndTimes { set; get; } //课程关闭时间
    }
}
