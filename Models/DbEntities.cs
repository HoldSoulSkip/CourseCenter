using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace CourseCenter.Models
{
    public class DBEntities : DbContext
    {
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<StudentInfo> StudentInfo { get; set; }
        public DbSet<TeacherInfo> TeacherInfo { get; set; }
        public DbSet<Stu_Course> Stu_Course { get; set; }
        public DbSet<CourseGroupTitle> CourseGroupTitle { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<TitleContent> TitleContent { get; set; }
        public DbSet<SysScore> SysScore { get; set; }
        public DbSet<CouScore> CouScore { get; set; }
        public DbSet<BlogReply> BlogReply { get; set; }
        public DbSet<BlogTitle> BlogTitle { get; set; }
        public DbSet<StudentWork> StudentWork { get; set; }
        public DbSet<ScoreProportion> ScoreProportion { get; set; }
        public DbSet<PaperQuestion> PaperQuestion { get; set; }
        public DbSet<PaperStudentAnswer> PaperStudentAnswer { get; set; }
        public DbSet<StudentWorkImage> StudentWorkImage { get; set; }
        //第一次请求时 会根据数据库连接字符串 和model 创建数据库
        public DBEntities()
            : base("name=DBEntities")
        {
            // 如果数据库不存在 则创建。
            Database.SetInitializer<DBEntities>(new DropCreateDatabaseIfModelChanges<DBEntities>());

        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

    }
}