using System.Collections.Generic;
using MrJuerga.Entity;
using MrJuerga.Repository;

namespace MrJuerga.Service.implementation
{
    public class PacienteService : IPacienteService
    {

        private IPacienteRepository pacienteRepository;
        public PacienteService(IPacienteRepository pacienteRepository)
        {
            this.pacienteRepository=pacienteRepository;
        }
        
        public bool Delete(int id)
        {
            return pacienteRepository.Delete(id);
        }

        public Paciente Get(int id)
        {
            return pacienteRepository.Get(id);
        }

        public IEnumerable<Paciente> GetAll()
        {
           return pacienteRepository.GetAll();
        }

        public bool Save(Paciente entity)
        {
            return pacienteRepository.Save(entity);
        }

        public bool Update(Paciente entity)
        {
            return pacienteRepository.Update(entity);
        }
    }
}