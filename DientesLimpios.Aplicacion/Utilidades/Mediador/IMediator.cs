using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    /// <summary>
    /// Define el contrato para enviar solicitudes a sus respectivos manejadores.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Envía una solicitud y obtiene su respuesta de forma asíncrona.
        /// </summary>
        /// <typeparam name="TResponse">Tipo de respuesta de la solicitud.</typeparam>
        /// <param name="request">Solicitud que se desea procesar.</param>
        /// <returns>Una tarea que contiene la respuesta del manejador.</returns>
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
    }
}

