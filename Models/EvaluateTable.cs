using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class EvaluateTable
    {
        public int Id { get; set; }
        public string Title { get; set; }//题目
        public string Level1 { get; set; }//选项
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }
        public string Level5 { get; set; }
        public string Level6 { get; set; }
        public double Level1Score { get; set; }//每个选项的分数
        public double Level2Score { get; set; }
        public double Level3Score { get; set; }
        public double Level4Score { get; set; }
        public double Level5Score { get; set; }
        public double Level6Score { get; set; }
        public int IsNotconsentTag { get; set; }//是否是默认分值
        public int TableId { get; set; }
    }
}
