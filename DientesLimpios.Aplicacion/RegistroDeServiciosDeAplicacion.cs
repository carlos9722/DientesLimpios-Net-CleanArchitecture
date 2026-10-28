using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion
{
    /// <summary>
    /// Proporciona métodos para registrar los servicios de la capa de aplicación.
    /// </summary>
    public static class RegistroDeServiciosDeAplicacion
    {
        /// <summary>
        /// Registra el mediador y los manejadores de solicitudes de la aplicación.
        /// </summary>
        /// <param name="services">
        /// Contenedor de servicios donde se registran las dependencias.
        /// </param>
        /// <returns>La colección de servicios para permitir encadenar configuraciones.</returns>
        public static IServiceCollection AgregarServiciosDeAplicacion(
            this IServiceCollection services)
        {
            // Registra el mediador para resolverlo cuando sea necesario.
            services.AddTransient<IMediator, MediadorSimple>();

            // Registra el manejador encargado de crear consultorios.
            services.AddScoped<IRequestHandler<ComandoCrearConsultorio, Guid>,
                               CasoDeUsoCrearConsultorio>();

            // Registra el manejador encargado de consultar el detalle de un consultorio.
            services.AddScoped<IRequestHandler<ConsultaObtenerDetalleConsultorio, ConsultorioDetalleDTO>,
                               CasoDeUsoObtenerDetalleConsultorio>();

            // Registra el manejador para obtener el listado de consultorios.
            services.AddScoped < IRequestHandler < ConsultaObtenerListadoConsultorios,
                                List<ConsultorioListadoDTO>>, CasoDeUsoObtenerListadoConsultorios>();

            return services;
        }
    }
}

