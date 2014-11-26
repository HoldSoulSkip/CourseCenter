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
        public DbSet<Admin> admin { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<StudentInfo> StudentInfo { get; set; }
        public DbSet<TeacherInfo> TeacherInfo { get; set; }
        public DbSet<User> User { get; set; }
        
       

    }
}