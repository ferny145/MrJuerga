using System.Collections.Generic;
using System.Linq;
using MrJuerga.Entity;
using MrJuerga.Repository.dbcontext;

namespace MrJuerga.Repository.implementation
{
    public class PaqueteRepository: IPaqueteRepository
    {
        private ApplicationDbContext context;

        public PaqueteRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Paquete Get(int id)
        {
            var result = new Paquete();
            try
            {
                result = context.Paquetes.Single(x => x.Id == id);
            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public IEnumerable<Paquete> GetAll()
        {

            var result = new List<Paquete>();
            try
            {
                result = context.Paquetes.ToList();
            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public bool Save(Paquete entity)
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

        public bool Update(Paquete entity)
        {
            try
            {
                var paqueteoriginal = context.Paquetes.Single(
                    x => x.Id == entity.Id
                );

                paqueteoriginal.Id = entity.Id;
                paqueteoriginal.Nombre = entity.Nombre;
                paqueteoriginal.Descripcion = entity.Descripcion;
                paqueteoriginal.Precio = entity.Precio;
                paqueteoriginal.Estado = entity.Estado;
                paqueteoriginal.Stock = entity.Stock;
              
                context.Update(paqueteoriginal);
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
                var paqueteoriginal = context.Paquetes.Single(
                         x => x.Id == id
                     );
                paqueteoriginal.Estado = "Inactivo";
                context.Update(paqueteoriginal);
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