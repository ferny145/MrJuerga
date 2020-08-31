using System.Collections.Generic;
using System.Linq;
using MrJuerga.Entity;
using MrJuerga.Repository.dbcontext;
using Microsoft.EntityFrameworkCore;

namespace MrJuerga.Repository.implementation
{
    public class BoletaRepository : IBoletaRepository
    {
        private ApplicationDbContext context;

        public BoletaRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Boleta Get(int id)
        {
            var result = new Boleta();
            try
            {
                result = context.Boletas.Single(x => x.Id == id);

            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public IEnumerable<Boleta> GetAll()
        {
            var result = new List<Boleta>();
            try
            {
                result = context.Boletas.Include(c => c.Usuario).ToList();

                result.Select(c => new Boleta
                {
                    Id = c.Id,
                    UsuarioId = c.UsuarioId,
                    Fecha = c.Fecha,
                    Direccion = c.Direccion,
                    Total = c.Total
                });

            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public bool Save(Boleta entity)
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

        public bool Update(Boleta entity)
        {
            try
            {
                var boletaOriginal = context.Boletas.Single(
                    x => x.Id == entity.Id
                );

                boletaOriginal.Id = entity.Id;
                boletaOriginal.UsuarioId = entity.UsuarioId;
                boletaOriginal.Fecha = entity.Fecha;
                boletaOriginal.Direccion = entity.Direccion;
                boletaOriginal.Total = entity.Total;


                context.Update(boletaOriginal);
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