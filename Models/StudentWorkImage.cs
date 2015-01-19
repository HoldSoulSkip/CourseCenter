using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class StudentWorkImage
    {
        public int Id { get; set; }
        public int CourseId { set; get; } //课程Id
        public int ModuleTag { set; get; }//模块标示符代表是某一类型的模块：例如1, 模块一，2,模块二，3,模块三
        public string FilePath { get; set; }
        public Guid StudentId { get; set; }
        public double WorkImgScore { get; set; }
    }
}
