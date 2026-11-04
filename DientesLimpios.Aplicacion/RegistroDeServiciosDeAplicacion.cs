
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion
{
    /// <summary>
    /// Proporciona los métodos necesarios para registrar las dependencias
    /// y servicios que utiliza la capa de aplicación.
    /// </summary>
    public static class RegistroDeServiciosDeAplicacion
    {
        /// <summary>
        /// Registra el mediador y los manejadores de solicitudes de los casos de uso
        /// en el contenedor de inyección de dependencias.
        /// </summary>
        /// <param name="services">
        /// Colección de servicios donde se registran las dependencias de la aplicación.
        /// </param>
        /// <returns>
        /// La colección de servicios configurada para permitir encadenar otros registros.
        /// </returns>
        public static IServiceCollection AgregarServiciosDeAplicacion(
            this IServiceCollection services)
        {
            // Registra el mediador para que pueda ser utilizado por los componentes de la aplicación.
            services.AddTransient<IMediator, MediadorSimple>();

            // Registra el manejador encargado de procesar las solicitudes de creación de consultorios.
            services.AddScoped<IRequestHandler<ComandoCrearConsultorio, Guid>,
                               CasoDeUsoCrearConsultorio>();

            // Registra el manejador encargado de obtener el detalle de un consultorio.
            services.AddScoped<IRequestHandler<ConsultaObtenerDetalleConsultorio, ConsultorioDetalleDTO>,
                               CasoDeUsoObtenerDetalleConsultorio>();

            // Registra el manejador encargado de obtener el listado de consultorios.
            services.AddScoped<IRequestHandler<ConsultaObtenerListadoConsultorios,
                                List<ConsultorioListadoDTO>>, CasoDeUsoObtenerListadoConsultorios>();

            // Registra el manejador encargado de procesar las solicitudes de actualización de consultorios.
            services.AddScoped<IRequestHandler<ComandoActualizarConsultorio>, CasoDeUsoActualizarConsultorio>();

            // Registra el manejador encargado de procesar las solicitudes de eliminación de consultorios.
            services.AddScoped<IRequestHandler<ComandoBorrarConsultorio>, CasoDeUsoBorrarConsultorio>();

            // Retorna la colección de servicios con las dependencias registradas.
            return services;
        }
    }
}
