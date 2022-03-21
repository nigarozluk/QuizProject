using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Entity
{
   public class Option :BaseEntity
    {
        public string OptionText { get; set; }
        public bool IsTrue { get; set; }
        public int QuestionID { get; set; }
        public Question Questions { get; set; }

    }
}
