using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    /// <summary>
    /// Define el contrato para procesar una solicitud y generar una respuesta.
    /// </summary>
    /// <typeparam name="TRequest">Tipo de solicitud que se procesará.</typeparam>
    /// <typeparam name="TResponse">Tipo de respuesta que se devolverá.</typeparam>
    public interface IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Procesa la solicitud y devuelve su respuesta de forma asíncrona.
        /// </summary>
        /// <param name="request">Solicitud que se desea procesar.</param>
        /// <returns>Una tarea que contiene la respuesta generada.</returns>
        Task<TResponse> Handle(TRequest request);
    }
}

