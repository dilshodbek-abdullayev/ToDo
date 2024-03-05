using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities.DTOs;
using ToDo.Domain.Entities.Models;

namespace ToDo.Application.Abstractions.IServices
{
    public interface INotepadService
    {
        public Task<string> Add(NotepadDTO notepadDTO);
        public Task<Notepad> GetNotepadById(int id);
        public Task<List<Notepad>> GetAllNotepad();
        public Task<List<Notepad>> GetByNote(string name);
        public Task<string> Delete(int id);
        public Task<string> Update(int id,NotepadDTO notepadDTO);
        public Task<string> GetNotePdf();
    }
}
