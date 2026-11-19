
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ObtenerDetalleDentista
{
    /// <summary>
    /// Representa la solicitud para consultar el detalle de un dentista.
    /// </summary>
    /// <remarks>
    /// Contiene el identificador único necesario para localizar al dentista
    /// y define el tipo de respuesta que devolverá el manejador mediante un DTO.
    /// </remarks>
    public class ConsultaObtenerDetalleDentista : IRequest<DentistaDTO>
    {
        /// <summary>
        /// Obtiene o establece el identificador único del dentista que se desea consultar.
        /// </summary>
        public required Guid Id { get; set; }
    }
}
