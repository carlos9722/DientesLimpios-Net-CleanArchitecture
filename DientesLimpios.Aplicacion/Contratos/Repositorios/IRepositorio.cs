using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.Contratos.Repositorios
{
    /// <summary>
    /// Define las operaciones básicas de acceso a los datos de una entidad.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad gestionada por el repositorio.</typeparam>
    public interface IRepositorio<T> where T : class
    {
        /// <summary>
        /// Obtiene una entidad por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la entidad.</param>
        /// <returns>La entidad encontrada o null si no existe.</returns>
        Task<T?> ObtenerPorId(Guid id);

        /// <summary>
        /// Obtiene todas las entidades disponibles.
        /// </summary>
        /// <returns>Una colección de entidades.</returns>
        Task<IEnumerable<T>> ObtenerTodos();

        /// <summary>
        /// Agrega una entidad al repositorio.
        /// </summary>
        /// <param name="entidad">Entidad que se desea agregar.</param>
        /// <returns>La entidad agregada.</returns>
        Task<T> Agregar(T entidad);

        /// <summary>
        /// Actualiza una entidad existente.
        /// </summary>
        /// <param name="entidad">Entidad con los datos que se desean actualizar.</param>
        Task Actualizar(T entidad);

        /// <summary>
        /// Elimina una entidad del repositorio.
        /// </summary>
        /// <param name="entidad">Entidad que se desea eliminar.</param>
        Task Borrar(T entidad);
    }
}

