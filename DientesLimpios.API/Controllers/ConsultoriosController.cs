
using DientesLimpios.API.DTOs.Consultorios;
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{

    /// <summary>
    /// Expone los endpoints para consultar y crear consultorios.
    /// </summary>
    [ApiController]
    [Route("api/consultorios")]
    public class ConsultoriosController : ControllerBase
    {
        private readonly IMediator mediator;

        public ConsultoriosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Consulta el detalle de un consultorio mediante su identificador.
        /// </summary>
        /// <param name="id">Identificador único del consultorio.</param>
        /// <returns>El detalle del consultorio encontrado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultorioDetalleDTO>> Get(Guid id)
        {
            // Crea la consulta que solicita el detalle del consultorio.
            var consulta = new ConsultaObtenerDetalleConsultorio { Id = id };

            // Delega la consulta al mediador para que la procese su manejador.
            var resultado = await mediator.Send(consulta);

            return resultado;
        }

        /// <summary>
        /// Registra un nuevo consultorio con el nombre proporcionado.
        /// </summary>
        /// <param name="crearConsultorioDTO">Datos necesarios para crear el consultorio.</param>
        /// <returns>Una respuesta HTTP que indica que la solicitud se procesó correctamente.</returns>
        [HttpPost]
        public async Task<IActionResult> Post(CrearConsultorioDTO crearConsultorioDTO)
        {
            // Construye el comando que solicita la creación del consultorio.
            var comando = new ComandoCrearConsultorio
            {
                Nombre = crearConsultorioDTO.Nombre
            };

            // Delega la ejecución del comando al mediador.
            await mediator.Send(comando);

            return Ok();
        }
    }

}
