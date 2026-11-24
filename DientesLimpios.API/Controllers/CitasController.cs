
using DientesLimpios.API.DTOs.Citas;
using DientesLimpios.Aplicacion.CasoDeUso.Citas.CrearCita;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    /// <summary>
    /// Gestiona las solicitudes HTTP relacionadas con las citas.
    /// </summary>
    [ApiController]
    [Route("api/citas")]
    public class CitasController : ControllerBase
    {
        /// <summary>
        /// Mediador encargado de enviar los comandos a sus respectivos
        /// casos de uso dentro de la aplicación.
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// Inicializa el controlador con la instancia del mediador.
        /// </summary>
        /// <param name="mediator">
        /// Servicio encargado de distribuir las solicitudes a los casos de uso correspondientes.
        /// </param>
        public CitasController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        /// <summary>
        /// Registra una nueva cita en el sistema.
        /// </summary>
        /// <param name="crearCitaDTO">
        /// Datos necesarios para crear la cita, incluyendo el paciente,
        /// dentista, consultorio y rango de fechas.
        /// </param>
        /// <returns>
        /// Respuesta HTTP 200 (OK) cuando se completa el envío del comando.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Post(CrearCitaDTO crearCitaDTO)
        {
            var comando = new ComandoCrearCita
            {
                ConsultorioId = crearCitaDTO.ConsultorioId,
                DentistaId = crearCitaDTO.DentistaId,
                PacienteId = crearCitaDTO.PacienteId,
                FechaInicio = crearCitaDTO.FechaInicio,
                FechaFin = crearCitaDTO.FechaFin
            };

            var resultado = await mediator.Send(comando);
            return Ok();
        }

    }
}
