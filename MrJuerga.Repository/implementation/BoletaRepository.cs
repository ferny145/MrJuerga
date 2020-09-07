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
                result = context.Boletas.Include(o => o.DetalleBoleta).Single(u => u.Id == id);            
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
                    if (result.Stock - detalle.Cantidad < 0)
                    {
                        context.Boletas.Remove(boleta);
                        context.SaveChanges();
                        return false;
                    }
                    else
                    {
                        result.Stock = result.Stock - detalle.Cantidad;
                        detalle.Subtotal = (result.Precio * detalle.Cantidad);
                        var result3 = new Boleta();
                        result3 = context.Boletas.Single(x => x.Id == boleta.Id);
                        boleta.Total = result3.Total + detalle.Subtotal;
                        context.Boletas.Update(boleta);
                        context.DetalleBoletas.Add(detalle);
                    }
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
                boletaOriginal.Total =  0;              
                context.Update(boletaOriginal);
                context.SaveChanges();

                var BoletaId = boletaOriginal.Id;

                //Objeto DetalleBoleta
                foreach (var item in entity.DetalleBoleta)
                {
                    var detboleta = new DetalleBoleta();
                    detboleta = context.DetalleBoletas.Single(x => x.Id == item.Id);                    
                        
                        detboleta.ProductoId = item.ProductoId;
                        detboleta.Cantidad = item.Cantidad;
                        detboleta.Subtotal = 0;                        
                    
                    var result = new Producto();
                    result = context.Productos.Single(x => x.Id == detboleta.ProductoId);
                    if (result.Stock - detboleta.Cantidad < 0)
                    {                        
                        return false;
                    }
                    else
                    {
                        result.Stock = result.Stock - detboleta.Cantidad;
                        detboleta.Subtotal = (result.Precio * detboleta.Cantidad);
                        var result3 = new Boleta();
                        result3 = context.Boletas.Single(x => x.Id == boletaOriginal.Id);
                        boletaOriginal.Total = result3.Total + detboleta.Subtotal;
                        context.Boletas.Update(boletaOriginal);
                        context.DetalleBoletas.Update(detboleta);
                    }
                }
                 context.SaveChanges();
            }
            catch (System.Exception ex)
            {

                Console.WriteLine(ex);
            }
            return true;
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}