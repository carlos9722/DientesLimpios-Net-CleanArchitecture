using DientesLimpios.Aplicacion.Contratos.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Persistencia.Repositorio
{
    /// <summary>
    /// Proporciona operaciones genéricas de acceso a datos para entidades
    /// administradas por Entity Framework Core.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de entidad que gestionará el repositorio. Debe ser una clase.
    /// </typeparam>
    /// <remarks>
    /// Encapsula las operaciones básicas de consulta, inserción, actualización
    /// y eliminación mediante DientesLimpiosDbContext.
    /// Los cambios realizados se mantienen en el contexto hasta que se
    /// confirman mediante SaveChanges o SaveChangesAsync.
    /// </remarks>
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        /// <summary>
        /// Contexto de Entity Framework Core utilizado para acceder a los datos.
        /// </summary>
        private readonly DientesLimpiosDbContext context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio genérico.
        /// </summary>
        /// <param name="context">
        /// Contexto de persistencia que permite consultar y modificar
        /// las entidades de la base de datos.
        /// </param>
        public Repositorio(DientesLimpiosDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Marca una entidad existente para actualizarla.
        /// </summary>
        /// <param name="entidad">
        /// Entidad cuyos cambios se desean guardar en la base de datos.
        /// </param>
        /// <returns>
        /// Una tarea completada cuando la entidad ha sido marcada
        /// para actualización en el contexto.
        /// </returns>
        /// <remarks>
        /// Este método no ejecuta por sí mismo una operación de actualización
        /// en la base de datos. Los cambios deben confirmarse posteriormente
        /// mediante SaveChanges o SaveChangesAsync.
        /// </remarks>
        public Task Actualizar(T entidad)
        {
            context.Update(entidad);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Agrega una nueva entidad al contexto de persistencia.
        /// </summary>
        /// <param name="entidad">
        /// Entidad que se desea agregar.
        /// </param>
        /// <returns>
        /// Una tarea que contiene la entidad agregada al contexto.
        /// </returns>
        /// <remarks>
        /// La inserción en la base de datos se realiza cuando los cambios
        /// se confirman mediante SaveChanges o SaveChangesAsync.
        /// </remarks>
        public Task<T> Agregar(T entidad)
        {
            context.Add(entidad);
            return Task.FromResult(entidad);
        }

        /// <summary>
        /// Marca una entidad para su eliminación.
        /// </summary>
        /// <param name="entidad">
        /// Entidad que se desea eliminar.
        /// </param>
        /// <returns>
        /// Una tarea completada cuando la entidad ha sido marcada
        /// para eliminación en el contexto.
        /// </returns>
        /// <remarks>
        /// La eliminación en la base de datos se ejecuta cuando los cambios
        /// se confirman mediante SaveChanges o SaveChangesAsync.
        /// </remarks>
        public Task Borrar(T entidad)
        {
            context.Remove(entidad);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Busca una entidad por su identificador único.
        /// </summary>
        /// <param name="id">
        /// Identificador GUID de la entidad que se desea buscar.
        /// </param>
        /// <returns>
        /// Una tarea que contiene la entidad encontrada o null si no existe.
        /// </returns>
        public async Task<T?> ObtenerPorId(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Obtiene todas las entidades del tipo administrado por el repositorio.
        /// </summary>
        /// <returns>
        /// Una tarea que contiene una colección con las entidades encontradas.
        /// </returns>
        public async Task<IEnumerable<T>> ObtenerTodos()
        {
            return await context.Set<T>().ToListAsync();
        }
    }
}
