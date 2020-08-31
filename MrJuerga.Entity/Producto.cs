using System;
using System.ComponentModel.DataAnnotations;

namespace MrJuerga.Entity
{
    public class Producto
    {
         public int Id { get; set; }        
        public string Nombre { get; set; }        
        public string Descripcion { get; set; }
        public string Precio { get; set; }

        public string Categoria { get; set; }
        public string Estado { get; set; }
    }
}