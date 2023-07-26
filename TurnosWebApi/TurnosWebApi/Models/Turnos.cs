using System;
using System.Collections.Generic;

namespace TurnosWebApi.Models
{
    public partial class Turnos
    {
        public int IdTurno { get; set; }
        public int? IdServicio { get; set; }
        public DateTime? FechaTurno { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public string? Estado { get; set; }

        public virtual Servicios? IdServicioNavigation { get; set; }
    }
}
