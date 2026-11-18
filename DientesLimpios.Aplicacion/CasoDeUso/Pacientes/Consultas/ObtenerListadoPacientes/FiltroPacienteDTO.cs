using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    /// <summary>
    /// Define los criterios de filtrado y paginación para consultar pacientes.
    /// </summary>
    public class FiltroPacienteDTO
    {
        /// <summary>
        /// Obtiene o establece el número de página que se desea consultar.
        /// </summary>
        /// <value>Por defecto, corresponde a la primera página.</value>
        public int Pagina { get; set; } = 1;

        /// <summary>
        /// Obtiene o establece la cantidad máxima de registros por página.
        /// </summary>
        /// <value>Por defecto, se establecen 10 registros por página.</value>
        public int RegistrosPorPagina { get; set; } = 10;

        /// <summary>
        /// Obtiene o establece el nombre utilizado para filtrar los pacientes.
        /// </summary>
        /// <value>Si es nulo o está vacío, no se aplica este filtro.</value>
        public string? Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece el correo electrónico utilizado para filtrar los pacientes.
        /// </summary>
        /// <value>Si es nulo o está vacío, no se aplica este filtro.</value>
        public string? Email { get; set; }
    }
}