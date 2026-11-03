
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    /// <summary>
    /// Define el contrato para un manejador encargado de procesar una solicitud
    /// y devolver una respuesta de un tipo específico.
    /// </summary>
    /// <typeparam name="TRequest">Tipo de solicitud que se procesará.</typeparam>
    /// <typeparam name="TResponse">Tipo de dato que tendrá la respuesta generada.</typeparam>
    public interface IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Ejecuta el procesamiento de la solicitud y devuelve el resultado
        /// de forma asíncrona.
        /// </summary>
        /// <param name="request">Solicitud que contiene la información necesaria para ejecutar la operación.</param>
        /// <returns>Una tarea que contiene la respuesta generada por el procesamiento.</returns>
        Task<TResponse> Handle(TRequest request);
    }

    /// <summary>
    /// Define el contrato para un manejador encargado de procesar solicitudes
    /// que no requieren devolver una respuesta.
    /// </summary>
    /// <typeparam name="TRequest">Tipo de solicitud que se procesará.</typeparam>
    public interface IRequestHandler<TRequest>
            where TRequest : IRequest
    {
        /// <summary>
        /// Ejecuta el procesamiento de la solicitud de forma asíncrona,
        /// sin devolver un resultado específico.
        /// </summary>
        /// <param name="request">Solicitud que contiene la información necesaria para ejecutar la operación.</param>
        /// <returns>Una tarea que representa la ejecución de la operación.</returns>
        Task Handle(TRequest request);
    }

}
