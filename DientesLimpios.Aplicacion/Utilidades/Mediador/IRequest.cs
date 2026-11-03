
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    /// <summary>
    /// Define una solicitud que espera obtener una respuesta de un tipo específico.
    /// Se utiliza para identificar solicitudes cuyo procesamiento devuelve un resultado.
    /// </summary>
    /// <typeparam name="TResponse">
    /// Tipo de dato que tendrá la respuesta generada al procesar la solicitud.
    /// </typeparam>
    public interface IRequest<TResponse>
    {
    }

    /// <summary>
    /// Define una solicitud que no espera recibir una respuesta.
    /// Se utiliza para representar operaciones cuyo propósito es ejecutar una acción
    /// sin necesidad de retornar un resultado.
    /// </summary>
    public interface IRequest
    {
    }

}
