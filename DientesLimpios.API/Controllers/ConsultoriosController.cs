
using DientesLimpios.API.DTOs.Consultorios;
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
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
        /// <summary>
        /// Mediador encargado de enviar las solicitudes a sus manejadores.
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// Inicializa el controlador con el mediador de la aplicación.
        /// </summary>
        /// <param name="mediator">Instancia del mediador inyectada por dependencias.</param>
        public ConsultoriosController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        /// <summary>
        /// Obtiene el listado de consultorios.
        /// </summary>
        /// <returns>Lista de consultorios disponibles.</returns>
        [HttpGet]
        public async Task<ActionResult<List<ConsultorioListadoDTO>>> Get()
        {
            // Crea la solicitud para obtener el listado de consultorios.
            var consulta = new ConsultaObtenerListadoConsultorios();
            // Envía la solicitud al mediador y espera la respuesta del manejador 
            var resultado = await mediator.Send(consulta);
            // Retorna el listado obtenido como respuesta del endpoint.
            return resultado;
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
