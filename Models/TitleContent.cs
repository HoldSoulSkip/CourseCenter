using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{     
    //小组某个标题的相应的回复
    public class TitleContent
    {
        public int Id { set; get; } //小组讨论内容的ID
        public int CourseGroupTitleId { set; get; }//小组讨论的所对应的标题Id
        public string Content { set; get; }//小组讨论回复的内容
        public string FromAccount { set; get; } //回帖人
        public Guid FromId { set; get; } //回帖人Id
        public string FromName{ set; get; } //回帖人名称
        public string FromDate { set; get; } //回帖时间
 

    }
}