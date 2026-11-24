
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Excepciones;
using System.Net;
using System.Text.Json;

namespace DientesLimpios.API.Middlewares
{
    /// <summary>
    /// Intercepta las excepciones de la aplicación y las convierte en respuestas HTTP.
    /// </summary>
    public class ManejadorExcepcionesMiddleware
    {
        /// <summary>
        /// Representa el siguiente componente del pipeline HTTP.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Inicializa el middleware con el siguiente componente del pipeline.
        /// </summary>
        /// <param name="next">Delegado que permite continuar con la ejecución de la solicitud.</param>
        public ManejadorExcepcionesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Ejecuta la solicitud y captura cualquier excepción generada durante su procesamiento.
        /// </summary>
        /// <param name="context">Contexto HTTP de la solicitud actual.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ManejarExcepcion(context, ex);
            }
        }

        /// <summary>
        /// Convierte una excepción en una respuesta HTTP según su tipo.
        /// </summary>
        /// <param name="context">Contexto HTTP donde se establece la respuesta.</param>
        /// <param name="excepcion">Excepción que se desea procesar.</param>
        /// <returns>Tarea que representa la escritura de la respuesta HTTP.</returns>
        private Task ManejarExcepcion(HttpContext context, Exception excepcion)
        {
            // Por defecto, las excepciones no identificadas generan un error interno.
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";
            var resultado = string.Empty;

            // Determina el código HTTP y el contenido según el tipo de excepción.
            switch (excepcion)
            {
                case ExcepcionNoEncontrado:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;

                case ExcepcionDeValidacion excepcionDeValidacion:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    resultado = JsonSerializer.Serialize(excepcionDeValidacion.ErroresDeValidacion);
                    break;

                case ExcepcionDeReglaDeNegocio excepcionReglaDeNegocio:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    resultado = JsonSerializer.Serialize(excepcionReglaDeNegocio.Message);
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            return context.Response.WriteAsync(resultado);
        }
    }

    /// <summary>
    /// Proporciona métodos de extensión para registrar el middleware de manejo de excepciones.
    /// </summary>
    public static class ManejadorExcepcionesMiddlewareExtensions
    {
        /// <summary>
        /// Agrega el middleware de manejo de excepciones al pipeline de solicitudes HTTP.
        /// </summary>
        /// <param name="builder">Constructor del pipeline de la aplicación.</param>
        /// <returns>El constructor del pipeline para permitir encadenar configuraciones.</returns>
        public static IApplicationBuilder UseManejadorExcepciones(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ManejadorExcepcionesMiddleware>();
        }
    }
}
