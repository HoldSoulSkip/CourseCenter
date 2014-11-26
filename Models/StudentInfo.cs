using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class StudentInfo :User
    {

        public int CourseId { set; get; } //课程id
        public int ScoreId { set; get; } //成绩表Id
        public int PerformanceId { set; get; } //行为表Id



    }
}