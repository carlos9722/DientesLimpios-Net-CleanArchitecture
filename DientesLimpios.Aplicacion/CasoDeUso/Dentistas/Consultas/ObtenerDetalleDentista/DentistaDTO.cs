
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ObtenerDetalleDentista
{
    /// <summary>
    /// Representa el objeto de transferencia de datos utilizado para devolver
    /// la información detallada de un dentista.
    /// </summary>
    /// <remarks>
    /// Contiene únicamente los datos necesarios para transferir la información
    /// del dentista entre las capas de la aplicación, sin exponer directamente
    /// la entidad del dominio.
    /// </remarks>
    public class DentistaDTO
    {
        /// <summary>
        /// Obtiene o establece el identificador único del dentista.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del dentista.
        /// </summary>
        public required string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece el correo electrónico del dentista.
        /// </summary>
        public required string Email { get; set; }
    }
}
