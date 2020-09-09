using System.Collections.Generic;
using MrJuerga.Entity;
using MrJuerga.Repository;

namespace MrJuerga.Service.implementation
{
    public class BoletaService: IBoletaService
    {
        private IBoletaRepository boletaRepository;
        public BoletaService(IBoletaRepository boletaRepository)
        {
            this.boletaRepository=boletaRepository;
        }
        
        public bool Delete(int id)
        {
            return boletaRepository.Delete(id);
        }

        public IEnumerable<BoletaDTO> FetchTop5Customers()
        {
            return boletaRepository.FetchTop5Customers();
        }
        public Boleta Get(int id)
        {
            return boletaRepository.Get(id);
        }

        public IEnumerable<Boleta> GetAll()
        {
           return boletaRepository.GetAll();
        }

        public bool Save(Boleta entity)
        {
            return boletaRepository.Save(entity);
        }

        public bool Update(Boleta entity)
        {
            return boletaRepository.Update(entity);
        }
    }
}