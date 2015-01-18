using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseCenter.Common
{
    public class Config
    {

        #region 用于给客观成绩各个部分比例设置静态值

        public static double BlogScorePercent = 0.2;//博客比例
        public static double HomeWorkScorePercent = 0.2;//作业比例
        public static double LearnTimeScorePercent = 0.2;//学习时长比例
        public static double GroupNumScorePercent = 0.2;//小组比例
        public static double MsgNumScorePercent = 0.2; //消息比例

        #endregion
    }
}