using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Abstractions.IServices;
using ToDo.Application.Services.AuthServices;
using ToDo.Application.Services.NotepadServices;
using ToDo.Application.Services.UserServices;

namespace ToDo.Application
{
    public static class ToDoDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<INotepadService,NotepadService>();
         
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
