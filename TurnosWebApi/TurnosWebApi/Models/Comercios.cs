using System;
using System.Collections.Generic;

namespace TurnosWebApi.Models
{
    public partial class Comercios
    {
        public Comercios()
        {
            Servicios = new HashSet<Servicios>();
        }

        public int IdComercio { get; set; }
        public string? NomComercio { get; set; }
        public string? AforoMaximo { get; set; }

        public virtual ICollection<Servicios> Servicios { get; set; }
    }
}
