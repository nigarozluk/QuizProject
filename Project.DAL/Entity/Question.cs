using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Entity
{
   public class Question : BaseEntity
    {
        public int TextID { get; set; }
        public String QuestionText { get; set; }
        public List<Option> Options { get; set; }
    }
}
