using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Entities.Models
{
    public class Notepad
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public string Status { get; set; } = "false";
    }
}
