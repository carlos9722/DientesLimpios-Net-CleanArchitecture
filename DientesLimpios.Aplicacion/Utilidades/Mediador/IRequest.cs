using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    /// <summary>
    /// Representa una solicitud que espera una respuesta de tipo específico.
    /// </summary>
    /// <typeparam name="TResponse">Tipo de respuesta que producirá la solicitud.</typeparam>
    public interface IRequest<TResponse>
    {
    }
}

