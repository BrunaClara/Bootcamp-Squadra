using Gerenciador.Model;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ): base(options)
        {
        }
        public DbSet<Curso> Cursos { get; set; }
    }
}
