using System.Collections.Generic;
using MrJuerga.Entity;
using MrJuerga.Repository;

namespace MrJuerga.Service.implementation
{
    public class ProductoPaqueteService: IProductoPaqueteService
    {
        private IProductoPaqueteRepository productopaqueteRepository;
        public ProductoPaqueteService(IProductoPaqueteRepository productopaqueteRepository)
        {
            this.productopaqueteRepository=productopaqueteRepository;
        }
        
        public bool Delete(int id)
        {
            return productopaqueteRepository.Delete(id);
        }

        public ProductoPaquete Get(int id)
        {
            return productopaqueteRepository.Get(id);
        }

        public IEnumerable<ProductoPaquete> GetAll()
        {
           return productopaqueteRepository.GetAll();
        }

        public bool Save(ProductoPaquete entity)
        {
            return productopaqueteRepository.Save(entity);
        }

        public bool Update(ProductoPaquete entity)
        {
            return productopaqueteRepository.Update(entity);
        }
    }
}