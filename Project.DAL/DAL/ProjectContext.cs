using Microsoft.EntityFrameworkCore;
using Project.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.DAL
{
   public class ProjectContext:DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options)
        : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Text> Texts { get; set; }
        public DbSet<UserExam> UserExams { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserExam>().HasKey(pk => new { pk.ExamID, pk.UserID });
        }
    }
}
