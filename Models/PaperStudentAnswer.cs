using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class PaperStudentAnswer
    {
        public int Id { get; set; }
        public Guid StudentId { get; set; }
        public int CourseId { get; set; }
        public int MouduleId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}