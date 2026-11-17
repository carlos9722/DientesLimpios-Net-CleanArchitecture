using DientesLimpios.API.DTOs.Pacientes;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    /// <summary>
    /// Expone los endpoints HTTP encargados de gestionar las operaciones
    /// relacionadas con los pacientes.
    /// </summary>
    /// <remarks>
    /// Actúa como punto de entrada de las solicitudes HTTP y delega
    /// la ejecución de los casos de uso a través del mediador.
    /// </remarks>
    [ApiController]
    [Route("api/pacientes")]
    public class PacientesController : ControllerBase
    {
        /// <summary>
        /// Mediador encargado de enviar los comandos y consultas
        /// hacia sus respectivos manejadores.
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// Inicializa una nueva instancia del controlador de pacientes.
        /// </summary>
        /// <param name="mediator">
        /// Instancia del mediador utilizada para enviar las solicitudes
        /// a la capa de aplicación.
        /// </param>
        public PacientesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Registra un nuevo paciente en el sistema.
        /// </summary>
        /// <param name="crearPacienteDTO">
        /// Objeto que contiene los datos necesarios para crear el paciente.
        /// </param>
        /// <returns>
        /// Una respuesta HTTP 200 (OK) cuando el comando se procesa correctamente.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Post(CrearPacienteDTO crearPacienteDTO)
        {
            // Convierte los datos recibidos desde la API en un comando
            // que representa la solicitud de creación de un paciente.
            var comando = new ComandoCrearPaciente
            {
                Nombre = crearPacienteDTO.Nombre,
                Email = crearPacienteDTO.Email
            };

            // Envía el comando al mediador para que sea procesado
            // por el caso de uso correspondiente.
            await mediator.Send(comando);

            // Retorna una respuesta indicando que la operación fue exitosa.
            return Ok();
        }
    }
}
