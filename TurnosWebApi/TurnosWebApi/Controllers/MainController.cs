using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using TurnosWebApi.Models;

namespace TurnosWebApi.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class MainController : Controller
        {
        private readonly PermisosDbContext _context;

        public MainController(PermisosDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("ListaServicios")]
        public async Task<IActionResult> GetListaServicios()
        {

            try
            {                
                var entidad = await _context.Servicios.ToListAsync();
                if (entidad != null)
                {
                    return Ok(entidad);
                }
                return BadRequest("Consulta vacía");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("ListaConsultaServicios")]
        public async Task<IActionResult> GetListaConsultaServicios([FromBody] ConsultaServicios request)
        {            

            try
            {
                StringBuilder query = new StringBuilder("exec [dbo].[ConsultaServicios] ");
                query.Append($"@id_servicio = {request.IdServicio}");                
                var entidad = await _context.ConsultaServicios.FromSqlRaw(query.ToString()).ToListAsync();                
                if (entidad != null)
                {
                    List<Turnos> turnos = new List<Turnos>();

                    if (entidad != null)
                    {
                        var horaInicio = entidad[0].HoraApertura;
                        var horaFin = entidad[0].HoraCierre;
                        var duracion = entidad[0].Duracion;
                        var fecha1 = request.FechaInicio.ToString();
                        var fechaInicio = DateTime.Parse(fecha1);
                        var fechaFin = request.FechaFin;
                        for (var i = fechaInicio; i <= fechaFin; i = i.AddDays(1))
                        {
                            for (TimeSpan? j = horaInicio; j < horaFin; j = j + duracion)
                            {
                                Turnos turno = new Turnos();
                                turno.IdServicio = entidad[0].IdServicio;
                                turno.FechaTurno = i;
                                turno.HoraInicio = j;
                                turno.HoraFin = j + duracion;
                                turno.Estado = "Activo";
                                turnos.Add(turno);
                            }
                        }
                    }                    
                    _context.Turnos.AddRange(turnos);
                    await _context.SaveChangesAsync();
                    return Ok(turnos);
                }
                return BadRequest("Consulta vacía");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }        
    }
}
 