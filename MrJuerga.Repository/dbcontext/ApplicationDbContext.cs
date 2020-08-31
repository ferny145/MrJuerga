using MrJuerga.Entity;
using Microsoft.EntityFrameworkCore;

namespace MrJuerga.Repository.dbcontext
{
    public class ApplicationDbContext: DbContext 
    {       
        public DbSet<Producto> Productos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paquete> Paquetes { get; set; }

        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) {

        }
    }     
}
