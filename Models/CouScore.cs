using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class CouScore  //学生课程任务成绩表
    {
        public int Id { get; set; }
        public Guid StudentId { get; set; }////这张成绩表关联的学生Id
        public int CourseId{get;set;}//这张成绩表关联的课程Id
        public int ModuleTag { get; set; }//表示属于具体的模块
        public string ModuleScore{ get; set; }//每个模块完成的成绩
    }
}