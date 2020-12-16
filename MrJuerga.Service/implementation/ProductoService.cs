using System.Collections.Generic;
using MrJuerga.Entity;
using MrJuerga.Repository;

namespace MrJuerga.Service.implementation
{
    public class ProductoService: IProductoService
    {
        private IProductoRepository productoRepository;
        public ProductoService(IProductoRepository productoRepository)
        {
            this.productoRepository=productoRepository;
        }
        
        public bool Delete(int id)
        {
            return productoRepository.Delete(id);
        }

        public IEnumerable<Producto> FetchProductobyCategory(int id)
        {
            return productoRepository.FetchProductobyCategory(id);
        }

        public IEnumerable<Producto> FetchProductobyName(string name)
        {
            return productoRepository.FetchProductobyName(name);
        }

        public Producto Get(int id)
        {
            return productoRepository.Get(id);
        }

        public IEnumerable<Producto> GetAll()
        {
           return productoRepository.GetAll();
        }

        public byte[] GetImage(string name)
        {
            return productoRepository.GetImage(name);
        }

        public bool Save(Producto entity)
        {
            return productoRepository.Save(entity);
        }

        public bool Update(Producto entity)
        {
            return productoRepository.Update(entity);
        }
    }
}