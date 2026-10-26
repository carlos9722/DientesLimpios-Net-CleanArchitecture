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
    /// Esta clase centraliza la configuración de Entity Framework Core
    /// y el registro de las implementaciones de los repositorios y de
    /// la unidad de trabajo.
    /// </remarks>
    public static class RegistroDeServiciosDePersistencia
    {
        /// <summary>
        /// Agrega al contenedor de inyección de dependencias los servicios
        /// necesarios para acceder y gestionar los datos de la aplicación.
        /// </summary>
        /// <param name="services">
        /// El contenedor de servicios de la aplicación, al que se agregan
        /// las dependencias de la capa de persistencia.
        /// </param>
        /// <returns>
        /// La misma colección de servicios recibida, con las dependencias
        /// de persistencia registradas, para permitir encadenar
        /// configuraciones adicionales.
        /// </returns>
        /// <remarks>
        /// Configura el contexto de Entity Framework Core para utilizar
        /// SQL Server y registra los repositorios y la unidad de trabajo
        /// con un ciclo de vida Scoped, es decir, una instancia por ámbito
        /// de servicios, normalmente asociado a una solicitud HTTP.
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

