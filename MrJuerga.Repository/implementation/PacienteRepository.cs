using System.Collections.Generic;
using System.Linq;
using MrJuerga.Entity;
using MrJuerga.Repository.dbcontext;

namespace MrJuerga.Repository.implementation
{
    public class PacienteRepository : IPacienteRepository
    {

        private ApplicationDbContext context;

        public PacienteRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Paciente Get(int id)
        {
            var result = new Paciente();
            try
            {
                result = context.Pacientes.Single(x => x.Id == id);
            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public IEnumerable<Paciente> GetAll()
        {

            var result = new List<Paciente>();
            try
            {
                result = context.Pacientes.ToList();
            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public bool Save(Paciente entity)
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

        public bool Update(Paciente entity)
        {
            try
            {
                 var pacienteOrigina = context.Pacientes.Single(
                     x => x.Id == entity.Id
                 );

                 pacienteOrigina.Id=entity.Id;
                 pacienteOrigina.Nombres=entity.Nombres;
                 pacienteOrigina.Apellidos=entity.Apellidos;
                 pacienteOrigina.Dni=entity.Dni;
                 pacienteOrigina.Direccion=entity.Direccion;
                 pacienteOrigina.Telefono=entity.Telefono;

                 context.Update(pacienteOrigina);
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