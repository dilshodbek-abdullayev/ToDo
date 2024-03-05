using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ToDo.Application.Abstractions;
using ToDo.Application.Abstractions.IServices;
using ToDo.Domain.Entities.DTOs;
using ToDo.Domain.Entities.Models;
using ToDo.Domain.Entities.ViewModels;

namespace ToDo.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> CreateUser(UserDTO userDTO)
        {
            var res = await _userRepository.GetAll();
            var email = res.Any( x => x.Email == userDTO.Email );
            var login = res.Any(x => x.Login == userDTO.Login );
            if (!email)
            {
                if (!login)
                {
                    var newUser = new User()
                    {
                        FullName = userDTO.FullName,

                        Login = userDTO.Login,
                        Email = userDTO.Email,
                        Password = userDTO.Password,
                        Role = userDTO.Role
                    };
                    await _userRepository.Create(newUser);
                    return "Created";
                }
                return "Login already exists";
            }
            return "Email already exists";
        }

        public async Task<string> DeleteUser(int id)
        {
            var result = await _userRepository.Delete(x => x.UserId == id);
            if (result)
            {
                return "Deleted";
            }
            else
            {
                return "Failed";
            }
        }

        public async Task<List<ViewModel>> GetAll()
        {
            var get = await _userRepository.GetAll();

            var result = get.Select(x => new ViewModel
            {
                UserId = x.UserId,
                Email = x.Email,
                Name = x.FullName,
                Role = x.Role
            }).ToList();
            return result;
            
        }

        public async Task<ViewModel> GetByEmail(string email)
        {
            var get = await _userRepository.GetByAny(x => x.Email == email);

            var result = new ViewModel
            {
                UserId = get.UserId,
                Email = get.Email,
                Name = get.FullName,
                Role = get.Role
            };
            return result;
        }

        public async Task<ViewModel> GetById(int id)
        {
            var get = await _userRepository.GetByAny(x => x.UserId == id);

            var result = new ViewModel
            {
                UserId =get.UserId,
                Email = get.Email,
                Name = get.FullName,
                Role = get.Role
            };

            return result;
        }

        public async Task<List<ViewModel>> GetByName(string name)
        {
            var get = await _userRepository.GetAll();
            var find = get.Where(x=> x.FullName == name);
            var result = find.Select(x=>new ViewModel
            {
                UserId = x.UserId,
                Name = x.FullName,
                Role = x.Role,
                Email = x.Email
            }).ToList();
            return result;
        }

        public async Task<List<ViewModel>> GetByRole(string role)
        {
            var get = await _userRepository.GetAll();
            var find = get.Where(x => x.Role == role);
            var result = find.Select(x => new ViewModel
            {
                UserId = x.UserId,
                Name = x.FullName,
                Role = x.Role,
                Email = x.Email
            }).ToList();
            return result;
        }


        public async Task<string> UpdateUser(int id, UserDTO userDTO)
        {
            var res = await _userRepository.GetAll();

            var email = res.Any(x => x.Email == userDTO.Email);
            var login = res.Any(x => x.Login == userDTO.Login);
            if(!email)
            {
                if(!login)
                {
                    var old = await _userRepository.GetByAny(x => x.UserId == id);

                    if (old == null) return "Failed";
                    old.FullName = userDTO.FullName;
                    old.Login = userDTO.Login;
                    old.Role = userDTO.Role;
                    old.Email = userDTO.Email;

                    await _userRepository.Update(old);
                    return "Updated";
                }
                return "This Login already exists";
            }
            return "This Email already exists";
        }
        public async Task<string> GetPdfPath()
        {
            var text = "";

            var getall = await _userRepository.GetAll();
            foreach (var user in getall.Where(x => x.Role != "Admin"))
            {
                text = text + $"{user.FullName}|{user.Email}\n";
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
