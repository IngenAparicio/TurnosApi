using System;
using System.Collections.Generic;

namespace TurnosWebApi.Models
{
    public partial class Servicios
    {
        public Servicios()
        {
            Turnos = new HashSet<Turnos>();
        }

        public int IdServicio { get; set; }
        public int? IdComercio { get; set; }
        public string? NomServicio { get; set; }
        public TimeSpan? HoraApertura { get; set; }
        public TimeSpan? HoraCierre { get; set; }
        public TimeSpan? Duracion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public virtual Comercios? IdComercioNavigation { get; set; }
        public virtual ICollection<Turnos> Turnos { get; set; }
    }
}
