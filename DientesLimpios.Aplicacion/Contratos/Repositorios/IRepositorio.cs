using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.Contratos.Repositorios
{
    /// <summary>
    /// Define las operaciones básicas de acceso a datos para las entidades del sistema.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de entidad gestionada por el repositorio. Debe ser una clase.
    /// </typeparam>
    public interface IRepositorio<T> where T : class
    {
        /// <summary>
        /// Obtiene una entidad mediante su identificador único.
        /// </summary>
        /// <param name="id">Identificador de la entidad que se desea consultar.</param>
        /// <returns>La entidad encontrada o null si no existe.</returns>
        Task<T?> ObtenerPorId(Guid id);

        /// <summary>
        /// Obtiene todas las entidades disponibles del tipo especificado.
        /// </summary>
        /// <returns>Una colección con las entidades encontradas.</returns>
        Task<IEnumerable<T>> ObtenerTodos();

        /// <summary>
        /// Obtiene la cantidad total de registros de la entidad gestionada.
        /// </summary>
        /// <returns>El número total de registros disponibles.</returns>
        Task<int> ObtenerCantitadTotalRegistros();

        /// <summary>
        /// Agrega una nueva entidad al repositorio.
        /// </summary>
        /// <param name="entidad">Entidad que se desea agregar.</param>
        /// <returns>La entidad agregada.</returns>
        Task<T> Agregar(T entidad);

        /// <summary>
        /// Actualiza una entidad existente en el repositorio.
        /// </summary>
        /// <param name="entidad">Entidad cuyos datos se desean actualizar.</param>
        Task Actualizar(T entidad);

        /// <summary>
        /// Elimina una entidad del repositorio.
        /// </summary>
        /// <param name="entidad">Entidad que se desea eliminar.</param>
        Task Borrar(T entidad);
    }
}