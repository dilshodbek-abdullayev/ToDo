using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Abstractions;
using ToDo.Application.Abstractions.IServices;
using ToDo.Domain.Entities.DTOs;
using ToDo.Domain.Entities.Models;

namespace ToDo.Application.Services.NotepadServices
{
    public class NotepadService : INotepadService
    {
        private readonly INotepadRepository _notepadRepository;

        public NotepadService(INotepadRepository notepadRepository)
        {
            _notepadRepository = notepadRepository;
        }

        public async Task<string> Add(NotepadDTO notepadDTO)
        {
            var note = new Notepad()
            {
                Note = notepadDTO.Notepad,
                Status = notepadDTO.Status
            };
            if(note != null )
            {
                await _notepadRepository.Create(note);
                return "Added";
            }
            return "Not Added";
            
        }

        public async Task<string> Delete(int id)
        {
            var result = await _notepadRepository.Delete(x => x.Id == id);
            if (result)
            {
                return "Deleted";
            }
            return "Not Deleted";
        }


        public async Task<List<Notepad>> GetAllNotepad()
        {
            var result =await _notepadRepository.GetAll();
            return result.ToList();
        }

        public async Task<List<Notepad>> GetByNote(string name)
        {
            var result = await _notepadRepository.GetAll();
            return result.Where(x => x.Note == name).ToList();
        }

        public async Task<Notepad> GetNotepadById(int id)
        {
            var result = await _notepadRepository.GetByAny(x => x.Id == id);
            return result;
        }

        public async Task<string> Update(int id, NotepadDTO notepadDTO)
        {
            var result = await _notepadRepository.GetByAny(x => x.Id == id);
            if(result != null)
            {
                result.Note = notepadDTO.Notepad;
                result.Status = "True";

                var res = await _notepadRepository.Update(result);
                if(res != null)
                {
                    return "Updated";
                }
                return "Not Updated";
            }
            return "Failed";
        }
        public async Task<string> GetNotePdf()
        {

            var text = "";

            var getall = await _notepadRepository.GetAll();
            foreach (var note in getall)
            {
                text = text + $"Note Id => {note.Id} | Note status => {note.Status}\nTitle => {note.Note}\n----------------------------------\n";
            }




            DirectoryInfo projectDirectoryInfo =
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent;

            var file = Guid.NewGuid().ToString();

            string pdfsFolder = Directory.CreateDirectory(
                 Path.Combine(projectDirectoryInfo.FullName, "pdfs")).FullName;

            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                      .Text("Notepad Users")
                      .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                      .PaddingVertical(1, Unit.Centimetre)
                      .Column(x =>
                      {
                          x.Spacing(20);

                          x.Item().Text(text);
                      });

                    page.Footer()
                      .AlignCenter()
                      .Text(x =>
                      {
                          x.Span("Page ");
                          x.CurrentPageNumber();
                      });
                });
            })
            .GeneratePdf(Path.Combine(pdfsFolder, $"{file}.pdf"));
            return Path.Combine(pdfsFolder, $"{file}.pdf");

        }
    }
}
