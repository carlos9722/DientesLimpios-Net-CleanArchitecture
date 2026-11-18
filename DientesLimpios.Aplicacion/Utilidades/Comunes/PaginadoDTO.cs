using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.Utilidades.Comunes
{
    /// <summary>
    /// Representa una estructura genérica para transportar resultados paginados.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de dato de los elementos incluidos en el resultado.
    /// </typeparam>
    public class PaginadoDTO<T>
    {
        /// <summary>
        /// Obtiene o establece la lista de elementos correspondientes a la página actual.
        /// </summary>
        public List<T> Elementos { get; set; } = [];

        /// <summary>
        /// Obtiene o establece la cantidad total de registros disponibles.
        /// </summary>
        public int Total { get; set; }
    }
}