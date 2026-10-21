using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio
{
    /// <summary>
    /// Contiene los datos del consultorio devueltos en una consulta de detalle.
    /// </summary>
    public class ConsultorioDetalleDTO
    {
        /// <summary>
        /// Obtiene o establece el identificador único del consultorio.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del consultorio.
        /// </summary>
        public required string Nombre { get; set; }
    }
}

