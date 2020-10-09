using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace MrJuerga.Entity
{
    public class Boleta
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }        
        public Usuario Usuario { get; set; }        
        public DateTime Fecha { get; set; }
        public string Direccion { get; set; }
        public float Total { get; set; }
        public IEnumerable<DetalleBoleta> DetalleBoleta { get; set; }
        public string Estado {get;set;}
    }
}