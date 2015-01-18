using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Models
{
    public class SysScore //系统评价学生成绩表
    {
        public int Id { get; set; }//表Id
        public Guid StudentId { get; set; }//用户Id
        public int CourseId { get; set; }//这张成绩表关联的课程Id

        public DateTime EnterCourseTime { get; set; }//学生进入某个课程时间
        public DateTime ExitCourseTime { get; set; }//学生退出某个课程时间

        public int CreatMsgCount { get; set; }//自己创建私信的数量
        public int RelativeMsgCount { get; set; }//别人回复私信我的数量
        public int ReplyMsgCount { get; set; }//自己回复私信的数量

        public int CreatGroupCount { get; set; }//自己创建小组的数量
        public int RelativeGroupCount { get; set; }//别人回复小组我的数量
        public int ReplyGroupCount { get; set; }//自己回复小组的数量

        public int CreatBlogCount { get; set; }//自己创建博客的数量
        public int RelativeBlogCount { get; set; }//别人回复博客我的数量
        public int ReplyBlogCount { get; set; }//自己回复博客的数量
        public int ContentOfCourseKeyValue { get; set; } //发表的博客 和 课程关键字的关联度

        

    }
}