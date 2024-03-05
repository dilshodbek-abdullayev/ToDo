using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities.Models;

namespace ToDo.Application.Abstractions
{
    public interface INotepadRepository : IBaseRepository<Notepad>
    {
    }
}
