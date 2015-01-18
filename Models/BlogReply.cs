using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class BlogReply
    {
        public int Id { get; set; }
        public int BlogId{get;set;} //评论的博客Id
        public Guid ReplyId { get; set; }//评论人Id
        public string ReplyAccount { set; get; }//评论人account
        public string ReplyContent { get; set; }//评论内容
        public string CreatTime { get; set; }//评论时间
    }
}