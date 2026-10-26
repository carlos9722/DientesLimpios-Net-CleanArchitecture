using DientesLimpios.Aplicacion.Contratos.Persistencia;
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Persistencia.UnidadDeTrabajo
{
    /// <summary>
    /// Implementa el patrón Unidad de Trabajo utilizando Entity Framework Core
    /// para coordinar la persistencia de los cambios realizados en el contexto.
    /// </summary>
    /// <remarks>
    /// Centraliza la confirmación de los cambios pendientes mediante
    /// DientesLimpiosDbContext. Las operaciones de los repositorios que
    /// comparten este contexto pueden confirmarse conjuntamente.
    /// </remarks>
    public class UnidadDeTrabajoEFCore : IUnidadDeTrabajo
    {
        /// <summary>
        /// Contexto de Entity Framework Core que administra las entidades
        /// y los cambios pendientes de persistencia.
        /// </summary>
        private readonly DientesLimpiosDbContext context;

        /// <summary>
        /// Inicializa una nueva instancia de la unidad de trabajo.
        /// </summary>
        /// <param name="context">
        /// Contexto de persistencia que se utilizará para confirmar
        /// los cambios realizados en las entidades.
        /// </param>
        public UnidadDeTrabajoEFCore(DientesLimpiosDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Persiste en la base de datos los cambios pendientes del contexto.
        /// </summary>
        /// <returns>
        /// Una tarea que representa la operación asíncrona de persistencia.
        /// </returns>
        /// <remarks>
        /// Utiliza SaveChangesAsync para guardar los cambios pendientes
        /// de las entidades que administra el contexto. Si la operación
        /// falla, la tarea puede finalizar con una excepción.
        /// </remarks>
        public async Task Persistir()
        {
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Representa la operación de reversión de cambios pendientes.
        /// </summary>
        /// <returns>
        /// Una tarea completada sin realizar ninguna operación de reversión.
        /// </returns>
        /// <remarks>
        /// Actualmente, este método no revierte los cambios realizados
        /// en el contexto ni deshace operaciones ya confirmadas en la base
        /// de datos. Su implementación debe completarse si se requiere
        /// ofrecer esa funcionalidad.
        /// </remarks>
        public Task Reversar()
        {
            return Task.CompletedTask;
        }
    }
}
