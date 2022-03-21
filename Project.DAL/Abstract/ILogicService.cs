using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Abstract
{
    public interface ILogicService
    {
        IUserRepository User { get; }
        ITextRepository Text { get; }
        IExamRepository Exam { get; }
        IQuestionRepository Question { get; }
        IOptionRepository Option { get; }
        IUserExamRepository UserExam { get; }
        void Save();
    }
}
