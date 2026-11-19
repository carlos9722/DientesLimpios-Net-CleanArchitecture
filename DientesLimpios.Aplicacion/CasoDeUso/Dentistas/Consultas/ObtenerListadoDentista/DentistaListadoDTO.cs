
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas
{
    /// <summary>
    /// Representa el objeto de transferencia de datos utilizado para mostrar
    /// la información de un dentista en un listado.
    /// </summary>
    /// <remarks>
    /// Contiene los datos necesarios para presentar la información básica
    /// de los dentistas sin exponer directamente las entidades del dominio.
    /// </remarks>
    public class DentistaListadoDTO
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
