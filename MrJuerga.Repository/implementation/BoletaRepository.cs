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
                //Objecto boleta
                Boleta boleta = new Boleta
                {
                    UsuarioId = entity.UsuarioId,
                    Fecha = entity.Fecha,
                    Direccion = entity.Direccion,
                    Total = 0
                };

                context.Boletas.Add(boleta);
                context.SaveChanges ();
                var BoletaId = boleta.Id;

                //Objeto DetalleOrden
                foreach (var item in entity.DetalleBoleta)
                {
                    DetalleBoleta detalle = new DetalleBoleta
                    {
                        ProductoId = item.ProductoId,
                        BoletaId = BoletaId,
                        Cantidad = item.Cantidad,
                        Subtotal = item.Cantidad * item.Producto.Precio,
                        PaqueteId = item.PaqueteId
                    };
                    boleta.Total += item.Subtotal;
                    context.DetalleBoletas.Add(detalle);
                }
                context.Boletas.Add(boleta);
                context.SaveChanges();
            }
            catch (System.Exception ex)
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