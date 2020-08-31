using System;
using System.ComponentModel.DataAnnotations;


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
    }
}