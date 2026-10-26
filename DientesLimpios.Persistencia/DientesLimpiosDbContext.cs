using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Persistencia
{
    /// <summary>
    /// Representa el contexto de acceso a datos de la aplicación DientesLimpios.
    /// </summary>
    /// <remarks>
    /// Administra las entidades que se consultan y persisten en la base de datos,
    /// y configura el modelo de Entity Framework Core a partir de las
    /// configuraciones definidas en el ensamblado de persistencia.
    /// </remarks>
    public class DientesLimpiosDbContext : DbContext
    {
        /// <summary>
        /// Inicializa una nueva instancia del contexto con las opciones
        /// de configuración proporcionadas.
        /// </summary>
        /// <param name="options">
        /// Contiene la configuración del contexto, como el proveedor de base
        /// de datos y otros ajustes de Entity Framework Core.
        /// </param>
        public DientesLimpiosDbContext(DbContextOptions<DientesLimpiosDbContext> options) : base(options)
        {
        }


        /// <summary>
        /// Inicializa una nueva instancia del contexto sin opciones explícitas.
        /// </summary>
        /// <remarks>
        /// Este constructor está protegido para permitir su uso desde clases
        /// derivadas. No debe utilizarse como sustituto de la configuración
        /// habitual del contexto mediante inyección de dependencias.
        /// </remarks>
        protected DientesLimpiosDbContext()
        {
        }


        /// <summary>
        /// Configura el modelo de entidades que Entity Framework Core utilizará
        /// para interactuar con la base de datos.
        /// </summary>
        /// <param name="modelBuilder">
        /// Objeto utilizado para definir las entidades, sus relaciones,
        /// restricciones y demás configuraciones del modelo.
        /// </param>
        /// <remarks>
        /// Aplica las configuraciones de entidades que implementan
        /// IEntityTypeConfiguration y que se encuentran en el mismo ensamblado
        /// que DientesLimpiosDbContext.
        /// </remarks>
        /// <param name="modelBuilder">Constructor del modelo de entidades.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica primero las configuraciones predeterminadas de la clase base.
            base.OnModelCreating(modelBuilder);

            // Busca y aplica las configuraciones de entidades del ensamblado actual,
            // evitando tener que registrar cada configuración manualmente.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DientesLimpiosDbContext).Assembly);
        }

        /// <summary>
        /// Obtiene o establece el conjunto de consultorios gestionados por el contexto.
        /// </summary>
        /// <remarks>
        /// Permite consultar, agregar, modificar y eliminar consultorios
        /// mediante las operaciones proporcionadas por Entity Framework Core.
        /// </remarks>
        public DbSet<Consultorio> Consultorioes { get; set; }
    }
}
