using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Abstractions;
using ToDo.Domain.Entities.Models;
using ToDo.Infrastructure.BaseRepositories;
using ToDo.Infrastructure.Persistance;

namespace ToDo.Infrastructure
{
    public static class ToDoDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext <ToDoDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("ToDotConnectionString"));
            });
            services.AddScoped<IBaseRepository<User>,BaseRepository<User>>();
            services.AddScoped<INotepadRepository, NotepadRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
