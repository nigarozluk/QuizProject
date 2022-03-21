using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Entity
{
   public class Text :BaseEntity
    {
        public string TextTitle { get; set; }
        public string TextContent { get; set; }
        public List<Question> Questions { get; set; }
    }
}
