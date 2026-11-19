
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas
{
    /// <summary>
    /// Representa los criterios de filtrado y paginación para consultar el listado de dentistas.
    /// </summary>
    /// <remarks>
    /// Permite establecer los parámetros necesarios para buscar dentistas por nombre
    /// y correo electrónico, así como controlar la cantidad de registros
    /// que se recuperan en cada página.
    /// </remarks>
    public class FiltroDentistasDTO
    {
        /// <summary>
        /// Obtiene o establece el número de página que se desea consultar.
        /// </summary>
        /// <remarks>
        /// Su valor predeterminado es 1 para iniciar la consulta desde la primera página.
        /// </remarks>
        public int Pagina { get; set; } = 1;

        /// <summary>
        /// Obtiene o establece la cantidad de registros que se mostrarán por página.
        /// </summary>
        /// <remarks>
        /// Su valor predeterminado es 10 registros por página.
        /// </remarks>
        public int RegistrosPorPagina { get; set; } = 10;

        /// <summary>
        /// Obtiene o establece el nombre utilizado como criterio de búsqueda.
        /// </summary>
        /// <remarks>
        /// Es opcional y permite filtrar los dentistas cuyo nombre contenga
        /// el texto proporcionado.
        /// </remarks>
        public string? Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece el correo electrónico utilizado como criterio de búsqueda.
        /// </summary>
        /// <remarks>
        /// Es opcional y permite filtrar los dentistas cuyo correo electrónico
        /// contenga el texto proporcionado.
        /// </remarks>
        public string? Email { get; set; }
    }
}
