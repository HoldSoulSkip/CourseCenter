using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class BlogTitle
    {
        public int Id { get; set; }                                     //博客Id
        public string TitleName { get; set; }                     //博客名称
        public string BlogContent { get; set; }                 //博客内容
        public int CourseId { get; set; }                 //博客关联的课程Id
        public Guid CreatId { get; set; }                              //博客创建人Id
        public string CreatAccount { get; set; }                //博客创建人账号
        public string CreatTime { get; set; }                         //博客创建时间
        public int ReadTimes { get; set; }                     //阅读次数
        public string Other { get; set; }                          //冗余字段，以后可作为转发，收藏 等扩展


    }
}