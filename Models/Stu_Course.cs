using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class Stu_Course
    {
        public int Id { get; set; }
        public Guid StudentId { get; set; }
        public int CourseId { get; set; }
        public string Tb_Info { get; set; }//冗余字段
    }
}