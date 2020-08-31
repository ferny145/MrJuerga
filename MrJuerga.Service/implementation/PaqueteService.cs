using System.Collections.Generic;
using MrJuerga.Entity;
using MrJuerga.Repository;

namespace MrJuerga.Service.implementation
{
    public class PaqueteService: IPaqueteService
    {
         private IPaqueteRepository paqueteRepository;
        public PaqueteService(IPaqueteRepository paqueteRepository)
        {
            this.paqueteRepository=paqueteRepository;
        }
        
        public bool Delete(int id)
        {
            return paqueteRepository.Delete(id);
        }

        public Paquete Get(int id)
        {
            return paqueteRepository.Get(id);
        }

        public IEnumerable<Paquete> GetAll()
        {
           return paqueteRepository.GetAll();
        }

        public bool Save(Paquete entity)
        {
            return paqueteRepository.Save(entity);
        }

        public bool Update(Paquete entity)
        {
            return paqueteRepository.Update(entity);
        }
    }
}