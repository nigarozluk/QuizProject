using Project.DAL.Abstract;
using Project.DAL.DAL;
using Project.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Concrete
{
    public class UserExamRepository : RepositoryBase<UserExam>, IUserExamRepository
    {
        public UserExamRepository(ProjectContext Context)
           : base(Context)
        {
        }
    }
}