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
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ProjectContext Context)
           : base(Context)
        {
        }
    }
}