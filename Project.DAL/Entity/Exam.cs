using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Entity
{
   public class Exam : BaseEntity
    {
        public int TextID { get; set; }
        public Text Text { get; set; }
        public List<UserExam> UserExams { get; set; }
    }
}
