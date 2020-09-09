using System;

namespace MrJuerga.Entity
{
    
[Serializable]    
    public class BoletaDTO
    {
        public int Id { get; set; }        
        public string Nombre { get; set; }
        public double total { get; set; }
    }
}