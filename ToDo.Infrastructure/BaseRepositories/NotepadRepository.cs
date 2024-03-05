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
    public class NotepadRepository : BaseRepository<Notepad>,INotepadRepository
    {
        public NotepadRepository(ToDoDbContext context) : base(context) { }
        
    }
}
