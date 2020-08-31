using System;
using System.ComponentModel.DataAnnotations;

namespace MrJuerga.Entity {
    public class Paciente {
        public int Id { get; set; }

        [Required]
        
        public string Nombres { get; set; }

        [Required]
        
        public string Apellidos { get; set; }
        public string Dni { get; set; }

        public string Direccion { get; set; }
        public string Telefono { get; set; }

    }
}