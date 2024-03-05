using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities.DTOs;

namespace ToDo.Application.Abstractions.IServices
{
    public interface IAuthService
    {
        public Task<ResponseLogin> GenerateToken(RequestLogin request);
    }
}
