using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Entity
{
   public class User:BaseEntity
    {
        public string UserName { get; set; }
        public string UserMail { get; set; }
        public string UserPassword { get; set; }
        public RoleType Role { get; set; }
        public List<UserExam> UserExams { get; set; }
    }
    public enum RoleType : byte
    {
        [Description("Admin")]
        Admin,
        [Description("User")]
        User
    }
}
