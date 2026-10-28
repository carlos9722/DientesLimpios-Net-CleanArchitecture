
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerListadoConsultorios
{
    /// <summary>
    /// DTO que representa los datos básicos de un consultorio
    /// para la consulta del listado de consultorios.
    /// </summary>
    public class ConsultorioListadoDTO
    {
        /// <summary>
        /// Identificador único del consultorio.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del consultorio. Su inicialización es obligatoria.
        /// </summary>
        public required string Nombre { get; set; }
    }
}
