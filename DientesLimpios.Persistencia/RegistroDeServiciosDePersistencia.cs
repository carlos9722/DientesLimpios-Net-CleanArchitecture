
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Persistencia.Repositorio;
using DientesLimpios.Persistencia.UnidadDeTrabajo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Persistencia
{
    /// <summary>
    /// Registra los servicios de la capa de persistencia en el contenedor
    /// de inyección de dependencias de la aplicación.
    /// </summary>
    /// <remarks>
    /// Centraliza la configuración de Entity Framework Core y el registro
    /// de los repositorios y la unidad de trabajo.
    /// </remarks>
    public static class RegistroDeServiciosDePersistencia
    {
        /// <summary>
        /// Registra las dependencias necesarias para acceder y gestionar
        /// los datos de la aplicación.
        /// </summary>
        /// <param name="services">
        /// Contenedor de servicios donde se registran las dependencias.
        /// </param>
        /// <returns>
        /// La colección de servicios actualizada, permitiendo encadenar
        /// otras configuraciones.
        /// </returns>
        /// <remarks>
        /// Configura Entity Framework Core para utilizar SQL Server
        /// y registra los repositorios y la unidad de trabajo con ciclo
        /// de vida Scoped (una instancia por ámbito de servicios).
        /// </remarks>
        public static IServiceCollection AgregarServiciosDePersistencia(
            this IServiceCollection services)
        {
            // Registra el contexto de Entity Framework Core y configura
            // SQL Server para obtener la cadena de conexión desde la
            // configuración identificada como "DientesLimpiosConecctionString".
            services.AddDbContext<DientesLimpiosDbContext>(options =>
                options.UseSqlServer("name=DientesLimpiosConecctionString"));

            // Registra el repositorio de consultorios con ciclo de vida Scoped.
            // Cuando se solicite IRepositorioConsultorios, el contenedor
            // proporcionará una instancia de RepositorioConsultorios dentro
            // del ámbito de servicios correspondiente.
            services.AddScoped<IRepositorioConsultorios, RepositorioConsultorios>();

            // Registra la implementación del repositorio de pacientes.
            // Cuando se solicite IRepositorioPacientes, el contenedor
            // proporcionará una instancia de RepositorioPacientes.
            services.AddScoped<IRepositorioPacientes, RepositorioPacientes>();

            // Registra la implementación del repositorio de dentistas.
            // Cuando se solicite IRepositorioDentista, el contenedor
            // proporcionará una instancia de RepositorioDentista.
            services.AddScoped<IRepositorioDentista, RepositorioDentista>();

            // Registra la implementación del repositorio de citas.
            // Cuando se solicite IRepositorioCitas, el contenedor
            // proporcionará una instancia de RepositorioCitas.
            services.AddScoped<IRepositorioCitas, RepositorioCitas>();

            // Registra la implementación de la unidad de trabajo con ciclo
            // de vida Scoped, permitiendo coordinar las operaciones de acceso
            // a datos dentro del mismo ámbito de servicios.
            services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajoEFCore>();

            // Devuelve la colección de servicios para permitir encadenar
            // otras configuraciones durante el registro de dependencias.
            return services;
        }
    }
}
