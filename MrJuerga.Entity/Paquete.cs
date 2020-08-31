using System;
using System.ComponentModel.DataAnnotations;

namespace MrJuerga.Entity
{
    public class Paquete
    {
         public int Id { get; set; }        
        public string Nombre { get; set; }        
        public string Descripcion { get; set; }
        public string Precio { get; set; }
        public string Estado { get; set; }
        public string Stock { get; set; }
        
    }
}