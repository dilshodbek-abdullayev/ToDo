using ToDo.Domain.Entities.DTOs;
using ToDo.Domain.Entities.ViewModels;

namespace ToDo.Application.Abstractions.IServices
{
    public interface IUserService
    {
        public Task<string> CreateUser(UserDTO userDTO);
        public Task<string> UpdateUser(int id, UserDTO userDTO);
        public Task<string> DeleteUser(int id);
        public Task<List<ViewModel>> GetAll();
        public Task<List<ViewModel>> GetByRole(string role);
        public Task<List<ViewModel>> GetByName(string name);
        public Task<ViewModel> GetById(int id);
        public Task<ViewModel> GetByEmail(string email);
        Task<string> GetPdfPath();
    }
}
