using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.API.Attributes;
using ToDo.Application.Abstractions.IServices;
using ToDo.Application.Services.UserServices;
using ToDo.Domain.Entities.DTOs;
using ToDo.Domain.Entities.Enums;
using ToDo.Domain.Entities.Models;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotepadController : ControllerBase
    {
        private readonly INotepadService _notepadService;

        public NotepadController(INotepadService notepadService)
        {
            _notepadService = notepadService;
        }

        [HttpPost]
        [IdentityFilter(Permission.AddNotePad)]

        public async Task<string> AddNotepad(NotepadDTO notepadDTO)
        {
            var res = await _notepadService.Add(notepadDTO);
            return res;
        }
        [HttpGet]
        [IdentityFilter(Permission.GetAllNotePad)]
        public async Task<List<Notepad>> GetAllNotepad()
        {
            var res = await _notepadService.GetAllNotepad();
            return res;
        }
        [HttpGet]
        [IdentityFilter(Permission.GetAllNotePad)]
        public async Task<Notepad> GetAllNotepadById(int id)
        {
            var res = await _notepadService.GetNotepadById(id);
            return res;
        }
        [HttpGet]
        [IdentityFilter(Permission.GetAllNotePad)]
        public async Task<List<Notepad>> GetByNote(string note)
        {
            var res = await _notepadService.GetByNote(note);
            return res;
        }
        [HttpPut]
        [IdentityFilter(Permission.UpdateNotePad)]
        public async Task<string> Update(int id,NotepadDTO notepadDTO)
        {
            var res = await _notepadService.Update(id, notepadDTO);
            return res;
        }
        [HttpDelete]
        [IdentityFilter(Permission.DeleteNotePad)]
        public async Task<string> Delete(int id)
        {
            var res =await _notepadService.Delete(id);
            return res;
        }
        [HttpGet("Notepadni Yuklab Ol")]
        [IdentityFilter(Permission.GetNotePdf)]
        public async Task<IActionResult> DownloadFile()
        {
            // Replace this with the path to the file you want to download
            var filePath = await _notepadService.GetNotePdf();

            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found");


            var fileBytes = System.IO.File.ReadAllBytes(filePath);


            var contentType = "application/octet-stream";

            var fileExtension = Path.GetExtension(filePath).ToLowerInvariant();


            // Send the file as a response
            return File(fileBytes, contentType, Path.GetFileName(filePath));
        }

    }
}
