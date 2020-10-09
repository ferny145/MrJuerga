using System.Collections.Generic;
using System.Linq;
using MrJuerga.Entity;
using MrJuerga.Repository.dbcontext;

namespace MrJuerga.Repository.implementation
{
    public class ProductoRepository : IProductoRepository
    {
        private ApplicationDbContextDTO context;

        public ProductoRepository(ApplicationDbContextDTO context)
        {
            this.context = context;
        }
        public Producto Get(int id)
        {
            var result = new Producto();
            try
            {
                result = context.Productos.Single(x => x.Id == id);
            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public IEnumerable<Producto> GetAll()
        {

            var result = new List<Producto>();
            try
            {
                result = context.Productos.ToList();
            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public bool Save(Producto entity)
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

        public bool Update(Producto entity)
        {
            try
            {
                var productoriginal = context.Productos.Single(
                    x => x.Id == entity.Id
                );

                productoriginal.Id = entity.Id;
                productoriginal.Nombre = entity.Nombre;
                productoriginal.Descripcion = entity.Descripcion;
                productoriginal.Precio = entity.Precio;
                productoriginal.Categoria = entity.Categoria;
                productoriginal.Estado = entity.Estado;
                productoriginal.Stock = entity.Stock;

                context.Update(productoriginal);
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
                var productoriginal = context.Productos.Single(
                         x => x.Id == id
                     );
                productoriginal.Estado = "Inactivo";
                context.Update(productoriginal);
                context.SaveChanges();
            }
            catch (System.Exception)
            {

                return false;
            }
            return true;
        }
        public IEnumerable<Producto> FetchProductobyName(string name)
        {
            var result = new List<Producto> ();
            try {
                result = context.Productos.Where(m=> m.Nombre.Contains(name)).ToList ();
            } catch (System.Exception) {

                throw;
            }
            return result;
        }

        public IEnumerable<Producto> FetchProductobyCategory(string name)
        {
            var result = new List<Producto> ();
            try {
                result = context.Productos.Where(m=> m.Categoria.Contains(name)).ToList ();
            } catch (System.Exception) {

                throw;
            }
            return result;
        }

        public byte[] GetImage(string name)
        {
            string path = @"C:\Users\foi12\Music\MrJuerga\files\" + name + ".jpg";
            byte[] b = System.IO.File.ReadAllBytes(path);

            return b;
        }
    }
}