using DientesLimpios.Aplicacion.Contratos.Notificaciones;
using DientesLimpios.Infraestructura.Notificaciones;
using Microsoft.Extensions.DependencyInjection;

namespace DientesLimpios.Infraestructura
{
    /// <summary>
    /// Centraliza el registro de dependencias correspondientes a la capa de Infraestructura.
    /// </summary>
    public static class RegistroDeServiciosDeInfraestructura
    {
        /// <summary>
        /// Registra los servicios de infraestructura en el contenedor de dependencias.
        /// </summary>
        /// <param name="services">Contenedor de servicios de la aplicación.</param>
        /// <returns>Contenedor de servicios con las dependencias de infraestructura registradas.</returns>
        public static IServiceCollection AgregarServiciosDeInfraestructura(
            this IServiceCollection services)
        {
            // Registra el servicio de correo con ciclo de vida por solicitud HTTP.
            services.AddScoped<IServicioNotificaciones, ServicioCorreos>();

            return services;
        }
    }
}
