
using DientesLimpios.API.DTOs.Consultorios;
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    /// <summary>
    /// Expone los endpoints HTTP para consultar, crear y actualizar consultorios.
    /// Delega el procesamiento de las solicitudes al mediador de la aplicación.
    /// </summary>
    [ApiController]
    [Route("api/consultorios")]
    public class ConsultoriosController : ControllerBase
    {
        /// <summary>
        /// Mediador encargado de enviar las consultas y comandos
        /// a sus respectivos manejadores.
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// Inicializa el controlador con el mediador de la aplicación.
        /// </summary>
        /// <param name="mediator">Instancia del mediador proporcionada mediante inyección de dependencias.</param>
        public ConsultoriosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Obtiene el listado de consultorios registrados en la aplicación.
        /// </summary>
        /// <returns>
        /// Una respuesta HTTP que contiene la lista de consultorios.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<List<ConsultorioListadoDTO>>> Get()
        {
            // Crea la solicitud para obtener el listado de consultorios.
            var consulta = new ConsultaObtenerListadoConsultorios();

            // Envía la solicitud al mediador y espera la respuesta del manejador.
            var resultado = await mediator.Send(consulta);

            // Retorna el listado obtenido como respuesta del endpoint.
            return resultado;
        }

        /// <summary>
        /// Obtiene el detalle de un consultorio a partir de su identificador.
        /// </summary>
        /// <param name="id">Identificador único del consultorio que se desea consultar.</param>
        /// <returns>
        /// Una respuesta HTTP que contiene el detalle del consultorio encontrado.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultorioDetalleDTO>> Get(Guid id)
        {
            // Crea la consulta con el identificador del consultorio solicitado.
            var consulta = new ConsultaObtenerDetalleConsultorio { Id = id };

            // Envía la consulta al mediador para que sea procesada por su manejador.
            var resultado = await mediator.Send(consulta);

            return resultado;
        }

        /// <summary>
        /// Registra un nuevo consultorio utilizando la información recibida.
        /// </summary>
        /// <param name="crearConsultorioDTO">Datos necesarios para registrar el consultorio.</param>
        /// <returns>
        /// Una respuesta HTTP 200 OK cuando la operación se procesa correctamente.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Post(CrearConsultorioDTO crearConsultorioDTO)
        {
            // Construye el comando que solicita la creación del consultorio.
            var comando = new ComandoCrearConsultorio
            {
                Nombre = crearConsultorioDTO.Nombre
            };

            // Envía el comando al mediador para ejecutar el proceso de creación.
            await mediator.Send(comando);

            return Ok();
        }

        /// <summary>
        /// Actualiza la información de un consultorio existente mediante su identificador.
        /// </summary>
        /// <param name="id">Identificador único del consultorio que se desea actualizar.</param>
        /// <param name="actualizarConsultorioDTO">Datos que contienen la nueva información del consultorio.</param>
        /// <returns>
        /// Una respuesta HTTP 204 No Content cuando la actualización finaliza correctamente.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ActualizarConsultorioDTO actualizarConsultorioDTO)
        {
            // Construye el comando con el identificador y los nuevos datos del consultorio.
            var comando = new ComandoActualizarConsultorio { Id = id, Nombre = actualizarConsultorioDTO.Nombre };

            // Envía el comando al mediador para ejecutar la actualización.
            await mediator.Send(comando);

            return NoContent();
        }

        /// <summary>
        /// Elimina un consultorio existente mediante su identificador.
        /// </summary>
        /// <param name="id">Identificador único del consultorio que se desea eliminar.</param>
        /// <returns>
        /// Una respuesta HTTP 204 No Content cuando la eliminación finaliza correctamente.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Construye el comando que solicita la eliminación del consultorio.
            var comando = new ComandoBorrarConsultorio { Id = id };

            // Envía el comando al mediador para ejecutar el proceso de eliminación.
            await mediator.Send(comando);

            return NoContent();
        }
    }
}
