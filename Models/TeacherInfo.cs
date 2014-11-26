using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class TeacherInfo : User
    {
        public int TeacherCode { set; get; } //教师工号牌
        public int CourseId { set; get; } //课程id


    }
}