using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class ScoreProportion
    {
        public int Id { get; set; }
        public int Tag { get; set; }//标示，默认为0，使用默认成绩比例，否则使用教师比例
        public Guid TeacherId { get; set; }
        public int CourseId { get; set; }
        //五个模块的分数比例
        public double Moudule1Percent { get; set; }
        public double Moudule2Percent { get; set; }
        public double Moudule3Percent { get; set; }
        public double Moudule4Percent { get; set; }
        public double Moudule5Percent { get; set; }
        //教师给分比例
        public double TeacherPercent { get; set; }
    }
}