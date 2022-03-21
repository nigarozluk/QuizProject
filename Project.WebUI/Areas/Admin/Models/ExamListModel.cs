using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Areas.Admin.Models
{
    public class ExamListModel
    {
        public int ExamID { get; set; }
        public string Title { get; set; }
        public DateTime ExamDate { get; set; }
        public string UserMail { get; set; }
    }
}
