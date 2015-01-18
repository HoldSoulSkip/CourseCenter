using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{    
    //分组显示内容。主要显示的是小组的讨论标题
    public class CourseGroupTitle
    {
        public int Id { set; get; }//小组某个标题的Id
        public int CourseId { set; get; }//小组某个标题的对应的课程Id
        public string Content { get; set; }//创建小组时的初始内容
        public string CourseGroupCreatName { set; get; }//创建小组时的初始题目
        public Guid CourseGroupCreatId { set; get; } //小组某个标题的创建人的Id
        public string CourseGroupCreatAcount { set; get; }//小组某个标题的创建人的Acount
        public string CreatTime { get; set; }
      
    }
}