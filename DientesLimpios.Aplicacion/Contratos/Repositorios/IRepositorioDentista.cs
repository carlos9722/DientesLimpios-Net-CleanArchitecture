
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.Contratos.Repositorios
{
    /// <summary>
    /// Define las operaciones de acceso a datos específicas para la entidad Dentista.
    /// </summary>
    /// <remarks>
    /// Hereda las operaciones genéricas de IRepositorio y establece
    /// las operaciones adicionales necesarias para consultar dentistas
    /// mediante criterios de filtrado y paginación.
    /// </remarks>
    public interface IRepositorioDentista : IRepositorio<Dentista>
    {
        /// <summary>
        /// Recupera los dentistas aplicando criterios de filtrado y paginación.
        /// </summary>
        /// <param name="filtro">
        /// Objeto que contiene los criterios de búsqueda y los parámetros de paginación.
        /// </param>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona y devuelve una colección
        /// de entidades Dentista que cumplen con los criterios establecidos.
        /// </returns>
        Task<IEnumerable<Dentista>> ObtenerFiltrado(FiltroDentistasDTO filtro);
    }
}
