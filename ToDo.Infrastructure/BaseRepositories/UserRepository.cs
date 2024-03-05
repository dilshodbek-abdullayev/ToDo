using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Abstractions;
using ToDo.Domain.Entities.Models;
using ToDo.Infrastructure.Persistance;

namespace ToDo.Infrastructure.BaseRepositories
{
    public class UserRepository : BaseRepository<User>,IUserRepository
    {
        public UserRepository(ToDoDbContext context):base(context) 
        { }
    }
}
