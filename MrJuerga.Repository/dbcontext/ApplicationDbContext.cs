using MrJuerga.Entity;
using Microsoft.EntityFrameworkCore;

namespace MrJuerga.Repository.dbcontext
{
    public class ApplicationDbContext: DbContext 
    {
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Producto> Productos { get; set; }

        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) {

        }
    }     
}
