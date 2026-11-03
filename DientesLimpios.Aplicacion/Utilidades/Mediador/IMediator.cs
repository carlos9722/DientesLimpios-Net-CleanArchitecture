
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    /// <summary>
    /// Define el contrato del mediador, encargado de recibir solicitudes
    /// y enviarlas al manejador correspondiente para su procesamiento.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Envía una solicitud que espera una respuesta y devuelve
        /// el resultado generado por su manejador de forma asíncrona.
        /// </summary>
        /// <typeparam name="TResponse">Tipo de dato que tendrá la respuesta de la solicitud.</typeparam>
        /// <param name="request">Solicitud que se desea procesar.</param>
        /// <returns>Una tarea que contiene la respuesta generada por el manejador.</returns>
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);


        /// <summary>
        /// Envía una solicitud que no requiere una respuesta y espera
        /// a que su procesamiento finalice de forma asíncrona.
        /// </summary>
        /// <param name="request">Solicitud que se desea procesar.</param>
        /// <returns>Una tarea que representa la ejecución de la operación.</returns>
        Task Send(IRequest request);

    }
}
