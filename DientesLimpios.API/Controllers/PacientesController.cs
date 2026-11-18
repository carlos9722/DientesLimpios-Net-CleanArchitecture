using DientesLimpios.API.DTOs.Pacientes;
using DientesLimpios.API.Utilidades;
using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Comandos.BorrarPaciente;
using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerDetallePaciente;
using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente;
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
        /// Obtiene el listado paginado de pacientes registrados en el sistema.
        /// </summary>
        /// <param name="consulta">
        /// Parámetros de consulta utilizados para filtrar y paginar el listado de pacientes.
        /// </param>
        /// <returns>
        /// Una respuesta HTTP 200 (OK) con la lista de pacientes en formato
        /// PacienteListadoDTO y la cantidad total de registros en la cabecera HTTP.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<List<PacienteListadoDTO>>> Get(
            [FromQuery] ConsultaObtenerListadoPacientes consulta)
        {
            var resultado = await mediator.Send(consulta);
            HttpContext.InsertarPaginacionEnCabecera(resultado.Total);
            return resultado.Elementos; ;
        }

        /// <summary>
        /// Obtiene la información detallada de un paciente específico
        /// mediante su identificador único.
        /// </summary>
        /// <param name="id">
        /// Identificador único (GUID) del paciente que se desea consultar.
        /// </param>
        /// <returns>
        /// Una respuesta HTTP 200 (OK) con la información del paciente
        /// en formato PacienteDetalleDTO.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDetalleDTO>> Get(Guid id)
        {
            // Construye la consulta utilizando el identificador recibido
            // desde la ruta del endpoint.
            var consulta = new ConsultaObtenerDetallePaciente() { Id = id };
            // Envía la consulta al mediador para obtener el detalle del paciente.
            var resultado = await mediator.Send(consulta);
            // Retorna la información del paciente encontrado.
            return resultado;
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


        /// <summary>
        /// Actualiza la información de un paciente existente.
        /// </summary>
        /// <param name="id">
        /// Identificador único (GUID) del paciente que se desea actualizar.
        /// </param>
        /// <param name="actualizarPacienteDTO">
        /// Objeto que contiene los nuevos datos del paciente.
        /// </param>
        /// <returns>
        /// Una respuesta HTTP 200 (OK) cuando la actualización
        /// se procesa correctamente.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ActualizarPacienteDTO actualizarPacienteDTO)
        {
            // Construye el comando de actualización con el identificador
            // recibido en la ruta y los datos proporcionados en el cuerpo de la solicitud.
            var comando = new ComandoActualizarPaciente
            {
                Id = id,
                Nombre = actualizarPacienteDTO.Nombre,
                Email = actualizarPacienteDTO.Email
            };

            // Envía el comando al mediador para ejecutar el caso de uso de actualización.
            await mediator.Send(comando);

            // Retorna una respuesta indicando que la operación fue exitosa.
            return Ok();
        }



        /// <summary>
        /// Elimina un paciente existente del sistema mediante su identificador único.
        /// </summary>
        /// <param name="id">
        /// Identificador único (GUID) del paciente que se desea eliminar.
        /// </param>
        /// <returns>
        /// Una respuesta HTTP 200 (OK) cuando el comando de eliminación
        /// se procesa correctamente.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Construye el comando de eliminación utilizando el identificador
            // recibido desde la ruta del endpoint.
            var comando = new ComandoBorrarPaciente { Id = id };

            // Envía el comando al mediador para que sea procesado
            // por el caso de uso correspondiente.
            await mediator.Send(comando);

            // Retorna una respuesta indicando que la operación fue exitosa.
            return Ok();
        }

    }
}
