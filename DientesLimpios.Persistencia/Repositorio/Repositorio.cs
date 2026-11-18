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
    /// Tipo de entidad gestionada por el repositorio. Debe ser una clase.
    /// </typeparam>
    /// <remarks>
    /// Centraliza las operaciones comunes de consulta, inserción, actualización
    /// y eliminación mediante DientesLimpiosDbContext.
    /// Los cambios realizados se confirman en la base de datos mediante
    /// SaveChanges o SaveChangesAsync.
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
        /// Contexto de persistencia utilizado para consultar y modificar las entidades.
        /// </param>
        public Repositorio(DientesLimpiosDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Marca una entidad existente para su actualización.
        /// </summary>
        /// <param name="entidad">
        /// Entidad que se desea actualizar.
        /// </param>
        /// <returns>
        /// Una tarea que representa la operación de marcado de la entidad.
        /// </returns>
        /// <remarks>
        /// Los cambios se aplican en la base de datos al ejecutar
        /// SaveChanges o SaveChangesAsync.
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
        /// La inserción en la base de datos se realiza al ejecutar
        /// SaveChanges o SaveChangesAsync.
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
        /// Una tarea que representa la operación de marcado de la entidad.
        /// </returns>
        /// <remarks>
        /// La eliminación en la base de datos se realiza al ejecutar
        /// SaveChanges o SaveChangesAsync.
        /// </remarks>
        public Task Borrar(T entidad)
        {
            context.Remove(entidad);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Obtiene la cantidad total de registros de la entidad gestionada.
        /// </summary>
        /// <returns>
        /// El número total de registros almacenados.
        /// </returns>
        public async Task<int> ObtenerCantitadTotalRegistros()
        {
            return await context.Set<T>().CountAsync();
        }

        /// <summary>
        /// Busca una entidad por su identificador único.
        /// </summary>
        /// <param name="id">
        /// Identificador GUID de la entidad que se desea consultar.
        /// </param>
        /// <returns>
        /// La entidad encontrada o null si no existe.
        /// </returns>
        public async Task<T?> ObtenerPorId(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Obtiene todos los registros de la entidad gestionada por el repositorio.
        /// </summary>
        /// <returns>
        /// Una colección con las entidades encontradas.
        /// </returns>
        public async Task<IEnumerable<T>> ObtenerTodos()
        {
            return await context.Set<T>().ToListAsync();
        }
    }
}