using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{   
    //答疑的表，显示的是学生与教师的交互
    public class Question
    {
        public int Id { set; get; } //Id
        public Guid FromId { set; get; } //发件人
        public string FromAccount { set; get; } //发件人账号,如果是老师的话就是指开的课称名称，如果是学生就是学生姓名
        public Guid ToId { set; get; } //收件人Id
        public string ToAcconut { set; get; } //收件人账号 如果是老师的话就是指开的课称名称，如果是学生就是学生姓名
        public string Title { set; get; } //答疑的题目
        public string Content { set; get; } //答疑的内容，
        public string CreateTime { set; get; } //答疑时间
        public string Flag { set; get; } //状态：默认是未读。
        public string Tb_Info { set; get; } //冗余字段
        ///显示的时候，表现为50字，多的地方用弹窗显示

    }
}