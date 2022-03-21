using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Entity
{
    public class UserExam:BaseEntity
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public int ExamID { get; set; }
        public Exam Exam { get; set; }
        public bool IsStarted { get; set; } = false;
        public DateTime ExamDate { get; set; }
    }
}
