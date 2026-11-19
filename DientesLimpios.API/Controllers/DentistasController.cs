
using DientesLimpios.API.DTOs.Dentistas;
using DientesLimpios.API.Utilidades;
using DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Comandos.ActualizarDentista;
using DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Comandos.BorrarDentista;
using DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ObtenerDetalleDentista;
using DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ObtenerListadoDentista;
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.CrearDentista;
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    /// <summary>
    /// Expone los endpoints HTTP para gestionar las operaciones relacionadas con los dentistas.
    /// </summary>
    /// <remarks>
    /// Actúa como punto de entrada de las solicitudes HTTP y utiliza el mediador
    /// para enviar consultas y comandos a la capa de aplicación,
    /// manteniendo separado el controlador de la lógica de negocio.
    /// </remarks>
    [ApiController]
    [Route("api/Dentistas")]
    public class DentistasController : ControllerBase
    {
        // Dependencia encargada de enviar las consultas y comandos a sus respectivos manejadores.
        private readonly IMediator mediator;

        /// <summary>
        /// Inicializa una instancia del controlador de dentistas.
        /// </summary>
        /// <param name="mediator">
        /// Mediador encargado de distribuir las solicitudes hacia sus respectivos manejadores.
        /// </param>
        public DentistasController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Obtiene el listado de dentistas aplicando criterios de filtrado y paginación.
        /// </summary>
        /// <param name="consulta">
        /// Consulta recibida mediante los parámetros de la URL que contiene los filtros y datos de paginación.
        /// </param>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona y devuelve una respuesta HTTP
        /// con la lista de dentistas encontrados.
        /// </returns>
        /// <remarks>
        /// Envía la consulta al mediador, obtiene los resultados y agrega
        /// la cantidad total de registros en las cabeceras HTTP para informar
        /// los datos de paginación.
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<List<DentistaListadoDTO>>> Get(
            [FromQuery] ConsultaObtenerListadoDentistas consulta)
        {
            // Envía la consulta al manejador correspondiente y obtiene el resultado paginado.
            var resultado = await mediator.Send(consulta);

            // Agrega la cantidad total de registros en las cabeceras de la respuesta HTTP.
            HttpContext.InsertarPaginacionEnCabecera(resultado.Total);

            // Devuelve los elementos del listado al cliente.
            return resultado.Elementos;
        }

        /// <summary>
        /// Obtiene el detalle de un dentista mediante su identificador único.
        /// </summary>
        /// <param name="id">
        /// Identificador único del dentista que se desea consultar.
        /// </param>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona y devuelve una respuesta HTTP
        /// con la información detallada del dentista.
        /// </returns>
        /// <remarks>
        /// Construye una consulta con el identificador recibido y la envía
        /// al mediador para recuperar la información del dentista.
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<DentistaDTO>> Get(Guid id)
        {
            // Construye la consulta utilizando el identificador recibido en la solicitud.
            var consulta = new ConsultaObtenerDetalleDentista() { Id = id };

            // Envía la consulta al manejador correspondiente y obtiene el detalle del dentista.
            var resultado = await mediator.Send(consulta);

            // Devuelve la información del dentista al cliente.
            return resultado;
        }

        /// <summary>
        /// Registra un nuevo dentista en el sistema.
        /// </summary>
        /// <param name="crearDentistaDTO">
        /// Objeto que contiene el nombre y el correo electrónico del dentista que se desea registrar.
        /// </param>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona y devuelve una respuesta HTTP
        /// indicando que la solicitud fue procesada correctamente.
        /// </returns>
        /// <remarks>
        /// Transforma los datos recibidos en un comando de creación y lo envía
        /// al mediador para ejecutar el caso de uso correspondiente.
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Post(CrearDentistaDTO crearDentistaDTO)
        {
            // Construye el comando con los datos recibidos desde el DTO de la API.
            var comando = new ComandoCrearDentista { Nombre = crearDentistaDTO.Nombre, Email = crearDentistaDTO.Email };

            // Envía el comando al manejador encargado de registrar el dentista.
            await mediator.Send(comando);

            // Devuelve una respuesta HTTP indicando que la operación fue procesada correctamente.
            return Ok();
        }

        /// <summary>
        /// Actualiza la información de un dentista existente.
        /// </summary>
        /// <param name="id">
        /// Identificador único del dentista que se desea actualizar.
        /// </param>
        /// <param name="crearDentistaDTO">
        /// Objeto que contiene el nuevo nombre y correo electrónico del dentista.
        /// </param>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona y devuelve una respuesta HTTP
        /// indicando que la solicitud fue procesada correctamente.
        /// </returns>
        /// <remarks>
        /// Construye un comando de actualización con el identificador recibido
        /// y los nuevos datos proporcionados, y lo envía al mediador para ejecutar
        /// el caso de uso correspondiente.
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, CrearDentistaDTO crearDentistaDTO)
        {
            // Construye el comando con el identificador y los nuevos datos del dentista.
            var comando = new ComandoActualizarDentista { Id = id, Nombre = crearDentistaDTO.Nombre, Email = crearDentistaDTO.Email };

            // Envía el comando al manejador encargado de actualizar el dentista.
            await mediator.Send(comando);

            // Devuelve una respuesta HTTP indicando que la operación fue procesada correctamente.
            return Ok();
        }

        /// <summary>
        /// Elimina un dentista mediante su identificador único.
        /// </summary>
        /// <param name="id">
        /// Identificador único del dentista que se desea eliminar.
        /// </param>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona y devuelve una respuesta HTTP
        /// indicando que la solicitud fue procesada correctamente.
        /// </returns>
        /// <remarks>
        /// Construye un comando de eliminación con el identificador recibido
        /// y lo envía al mediador para ejecutar el caso de uso correspondiente.
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Construye el comando utilizando el identificador del dentista que se desea eliminar.
            var comando = new ComandoBorrarDentista { Id = id };

            // Envía el comando al manejador encargado de eliminar el dentista.
            await mediator.Send(comando);

            // Devuelve una respuesta HTTP indicando que la operación fue procesada correctamente.
            return Ok();
        }
    }
}
