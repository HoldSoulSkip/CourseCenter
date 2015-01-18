using CourseCenter.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CourseCenter.Common
{
    public class ScoreHelper
    {

        public double objectiveScore;//客观成绩
        public double terminateScore;//最终成绩

        DBEntities dbEntity = new DBEntities();
        #region 计算最终成绩+TerminateScore(double Moudule1Score)
        /// <summary>
        /// 计算最终成绩
        /// </summary>
        /// <param name="MouduleScore">模块分数集合</param>
        /// <param name="teacherId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public double TerminateScore(List<double> MouduleScore, string teacherId, int courseId)
        {
            //获取百分比从百分比表中
            ScoreProportion scorePercernt = dbEntity.ScoreProportion.Where(a => a.TeacherId == new Guid(teacherId) && a.CourseId == courseId).FirstOrDefault();
            double score = 0.0;
            score = MouduleScore.First() * scorePercernt.Moudule1Percent + MouduleScore[1] * scorePercernt.Moudule2Percent + MouduleScore[2] * scorePercernt.Moudule3Percent + MouduleScore[3] * scorePercernt.Moudule4Percent + MouduleScore[4] * scorePercernt.Moudule5Percent;
            this.terminateScore = score * scorePercernt.TeacherPercent + this.objectiveScore * (1 - scorePercernt.TeacherPercent);
            return this.terminateScore;
        }
        #endregion

        #region 计算客观成绩+ObjectiveScore(SysScore sysScore, double HomeWorkScore)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysScore"></param>
        /// <param name="HomeWorkScore"></param>
        /// <returns></returns>
        public double ObjectiveScore(SysScore sysScore, double HomeWorkScore)
        {
            double BlogScore, LearnTimeScore, GroupScore, MsgScore;
            //博客帖子数量计算出博客成绩
            if (sysScore.CreatBlogCount > 10)
            {
                BlogScore = 100.0;
            }
            else if (sysScore.CreatBlogCount <= 0)
            {
                BlogScore = 0;
            }
            else
            {
                BlogScore = sysScore.CreatBlogCount * 10;
            }

            //学习时长计算成绩----------- todo
            //LearnTime.TotalMinutes
            TimeSpan LearnTime = sysScore.ExitCourseTime - sysScore.EnterCourseTime;
            LearnTimeScore = GetLearnTimeScore(LearnTime);

            //计算小组分数-----------todo
            GroupScore = GetGroupScore(sysScore.CreatGroupCount);

            //消息分数------------todo
            MsgScore = GetMsgScore(sysScore.CreatMsgCount);

            this.objectiveScore = BlogScore * Config.BlogScorePercent + HomeWorkScore * Config.HomeWorkScorePercent + LearnTimeScore * Config.LearnTimeScorePercent + GroupScore * Config.GroupNumScorePercent + MsgScore * Config.MsgNumScorePercent;

            return this.objectiveScore;
        }
        #endregion


        #region 获得根据小组数量的分数+GetGroupScore(int GroupNum)
        /// <summary>
        /// 获得根据小组数量的分数
        /// </summary>
        /// <param name="GroupNum"></param>
        /// <returns></returns>
        public double GetGroupScore(int GroupNum)
        {
            if (GroupNum > 3)
            {
                return 100.0;
            }
            else if (GroupNum <= 0)
            {
                return 0.0;
            }
            else
            {
                return GroupNum / 3 * 100;
            }
        }
        #endregion



        #region 获得根据学习时长的分数+GetLearnTimeScore(LearnTime)
        /// <summary>
        /// 获得根据学习时长的分数,不乘百分比
        /// </summary>
        /// <param name="LearnTime"></param>
        /// <returns></returns>
        public double GetLearnTimeScore(TimeSpan LearnTime)
        {
            if (LearnTime.TotalMinutes >= 60)
            {
                return 100.0;
            }
            else if (LearnTime.TotalMinutes <= 0)
            {
                return 0.0;
            }
            else
            {
                return LearnTime.TotalMinutes / 6 * 10;
            }
        }
        #endregion



        #region 获得消息的分数+ GetMsgScore(int MsgNum)
        /// <summary>
        /// 获得消息的分数
        /// </summary>
        /// <param name="MsgNum"></param>
        /// <returns></returns>
        public double GetMsgScore(int MsgNum)
        {
            if (MsgNum > 10)
            {
                return 100.0;
            }
            else if (MsgNum <= 0)
            {
                return 0.0;
            }
            else
            {
                return MsgNum * 10;
            }
        }
        #endregion



    }
}