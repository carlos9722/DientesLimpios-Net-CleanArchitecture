
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.Consultas.ObtenerDetalleCita
{
    /// <summary>
    /// Representa la solicitud para obtener el detalle de una cita
    /// mediante su identificador.
    /// </summary>
    public class ConsultaObtenerDetalleCita : IRequest<CitaDetalleDTO>
    {
        /// <summary>
        /// Identificador único de la cita que se desea consultar.
        /// </summary>
        public required Guid Id { get; set; }
    }
}
