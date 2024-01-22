using APIapp.Model;
using Microsoft.EntityFrameworkCore;

namespace APIapp.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)         
        {
            
        }
        public DbSet<ToDo> ToDoList { get; set; }

    }
}
