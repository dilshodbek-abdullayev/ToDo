using System.Text.Json.Serialization;

namespace ToDo.Domain.Entities.DTOs
{
    public class NotepadDTO
    {
        public string Notepad { get; set; }
        [JsonIgnore]
        public string Status { get; set; } = "false";
    }
}
