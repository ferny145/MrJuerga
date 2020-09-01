using System.Collections.Generic;
using System.Linq;
using MrJuerga.Entity;
using MrJuerga.Repository.dbcontext;
using Microsoft.EntityFrameworkCore;

namespace MrJuerga.Repository.implementation
{
    public class ProductoPaqueteRepository: IProductoPaqueteRepository
    {
         private ApplicationDbContext context;

        public ProductoPaqueteRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public ProductoPaquete Get(int id)
        {
            var result = new ProductoPaquete();
            try
            {
                result = context.ProductoPaquetes.Single(x => x.Id == id);
            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public IEnumerable<ProductoPaquete> GetAll()
        {
            var result = new List<ProductoPaquete>();
            try
            {
                result = context.ProductoPaquetes.Include(c => c.Paquete).Include(d => d.Producto).ToList();

                result.Select(c => new ProductoPaquete
                {
                    Id = c.Id,
                    PaqueteId = c.PaqueteId,
                    ProductoId = c.ProductoId                    
                });

            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public bool Save(ProductoPaquete entity)
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

        public bool Update(ProductoPaquete entity)
        {
            try
            {
                 var productopaqueteOriginal = context.ProductoPaquetes.Single(
                    x => x.Id == entity.Id
                );

                productopaqueteOriginal.Id = entity.Id;
                productopaqueteOriginal.ProductoId = entity.ProductoId;
                productopaqueteOriginal.PaqueteId = entity.PaqueteId;                

                context.Update(productopaqueteOriginal);
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
            throw new System.NotImplementedException();
        }
    }
}