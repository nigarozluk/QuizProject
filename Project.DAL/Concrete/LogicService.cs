using Project.DAL.Abstract;
using Project.DAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Concrete
{
    public class LogicService : ILogicService
    {
        private ProjectContext _Context;
        public LogicService(ProjectContext Context)
        {
            _Context = Context;
        }
        private IUserRepository _user;
        private ITextRepository _text;
        private IExamRepository _exam;
        private IQuestionRepository _question;
        private IOptionRepository _option;
        private IUserExamRepository _userExam;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_Context);
                }
                return _user;
            }
        }
        public ITextRepository Text
        {
            get
            {
                if (_text == null)
                {
                    _text = new TextRepository(_Context);
                }
                return _text;
            }
        }
        public IExamRepository Exam
        {
            get
            {
                if (_exam == null)
                {
                    _exam = new ExamRepository(_Context);
                }
                return _exam;
            }
        }
        public IQuestionRepository Question
        {
            get
            {
                if (_question == null)
                {
                    _question = new QuestionRepository(_Context);
                }
                return _question;
            }
        }
        public IOptionRepository Option
        {
            get
            {
                if (_option == null)
                {
                    _option = new OptionRepository(_Context);
                }
                return _option;
            }
        }
        public IUserExamRepository UserExam
        {
            get
            {
                if (_userExam == null)
                {
                    _userExam = new UserExamRepository(_Context);
                }
                return _userExam;
            }
        }

        public void Save()
        {
            _Context.SaveChanges();
        }
    }
}
