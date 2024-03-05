using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities.Models;

namespace ToDo.Infrastructure.Persistance
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) 
        {
            Database.Migrate();
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Notepad> Notepads { get; set; }
    }
}
