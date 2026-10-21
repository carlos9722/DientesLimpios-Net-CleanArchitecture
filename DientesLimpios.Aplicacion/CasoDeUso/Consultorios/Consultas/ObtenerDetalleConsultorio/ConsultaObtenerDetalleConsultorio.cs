using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio
{
    /// <summary>
    /// Representa la solicitud para consultar el detalle de un consultorio.
    /// </summary>
    public class ConsultaObtenerDetalleConsultorio : IRequest<ConsultorioDetalleDTO>
    {
        /// <summary>
        /// Obtiene o establece el identificador único del consultorio.
        /// </summary>
        public Guid Id { get; set; }
    }
}
