using System.Collections.Generic;
using System.Linq;
using MrJuerga.Entity;
using MrJuerga.Repository.dbcontext;
using Microsoft.EntityFrameworkCore;
using System;

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
                result = context.Boletas.Include(o => o.DetalleBoleta).Single(u => u.Id == id); //.Single(x => x.Id == id).de //.Include(o => o.DetalleBoleta).ToList();
                //result = context.Boletas.Include(c => c.Usuario).Include(d => d.DetalleBoleta).ToList();              
            }

            catch (System.Exception)
            {

                throw;
            }
            return result;
        }

        public IEnumerable<Boleta> GetAll()
        {
            //LINQ
            var boleta = context.Boletas
                .Include(o => o.DetalleBoleta)
                .ToList();

            return boleta.Select(o => new Boleta
            {
                Id = o.Id,
                UsuarioId = o.UsuarioId,
                Fecha = o.Fecha,
                Direccion = o.Direccion,
                Total = o.Total,
                DetalleBoleta = o.DetalleBoleta                
            });
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
                context.SaveChanges();
                var BoletaId = boleta.Id;

                //Objeto DetalleOrden
                foreach (var item in entity.DetalleBoleta)
                {
                    DetalleBoleta detalle = new DetalleBoleta
                    {
                        ProductoId = item.ProductoId,
                        BoletaId = BoletaId,
                        Cantidad = item.Cantidad,                       
                        Subtotal = 0                      
                    };
                    var result = new Producto();
                    result = context.Productos.Single(x => x.Id == detalle.ProductoId);                  
                    detalle.Subtotal = (result.Precio * detalle.Cantidad);
                    var result3 = new Boleta();
                    result3 = context.Boletas.Single(x => x.Id == boleta.Id);
                    boleta.Total = result3.Total + detalle.Subtotal;
                    context.Boletas.Update(boleta);
                    context.DetalleBoletas.Add(detalle);

                }
                context.SaveChanges();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
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