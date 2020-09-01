using System.Collections.Generic;
using System.Linq;
using MrJuerga.Entity;
using MrJuerga.Repository.dbcontext;

namespace MrJuerga.Repository.implementation
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private ApplicationDbContext context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Usuario Get(int id)
        {
            var result = new Usuario();
            try
            {
                result = context.Usuarios.Single(x => x.Id == id);
            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public IEnumerable<Usuario> GetAll()
        {

            var result = new List<Usuario>();
            try
            {
                result = context.Usuarios.ToList();
            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public bool Save(Usuario entity)
        {
            try
            {
                context.Add(entity);
                context.SaveChanges();
            }
            catch (System.Exception)
            {

                return false;
            }
            return true;
        }

        public bool Update(Usuario entity)
        {
            try
            {
                var usuariooriginal = context.Usuarios.Single(
                    x => x.Id == entity.Id
                );

                usuariooriginal.Id = entity.Id;
                usuariooriginal.Nombre = entity.Nombre;
                usuariooriginal.Apellido = entity.Apellido;
                usuariooriginal.Correo = entity.Correo;
                usuariooriginal.Telefono = entity.Telefono;
                usuariooriginal.FechaNacimiento = entity.FechaNacimiento;                
                usuariooriginal.Password = entity.Password;
                usuariooriginal.Rol = entity.Rol;
                usuariooriginal.Dni = entity.Dni;
                usuariooriginal.Estado = entity.Estado;


                context.Update(usuariooriginal);
                context.SaveChanges();
            }
            catch (System.Exception)
            {

                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var usuariooriginal = context.Usuarios.Single(
                         x => x.Id == id
                     );
                usuariooriginal.Estado = "Inactivo";
                context.Update(usuariooriginal);
                context.SaveChanges();
            }
            catch (System.Exception)
            {

                return false;
            }
            return true;
        }
    }
}