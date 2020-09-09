using System;

namespace MrJuerga.Entity
{
    [Serializable]    
    public class DetalleBoletaDTO
    {
        public int Id { get; set; }        
        public string Nombre { get; set; }
        public int cantidad { get; set; }
    }
}