using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Entities.Enums
{
    public enum Permission
    {

        CreateUser = 1,
        GetAll = 2,
        GetByUserId = 3,
        GetByUserEmail = 4,
        GetByRole = 5,
        GetByUserName = 6,
        UpdateUser = 7,
        DeleteUser = 8,
        
        AddNotePad = 9,
        GetNotePadById = 10,
        GetAllNotePad = 11,
        UpdateNotePad = 12,
        DeleteNotePad = 13,
        
        GetUserPdf = 14, 
        GetNotePdf = 15
    }
}
