
using DientesLimpios.Identidad.Modelos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Identidad
{
    public static class RegistroDeServiciosDeIdentidad
    {
        // Registra los servicios necesarios para gestionar la identidad, autenticación y autorización de los usuarios.
        public static void AgregarServiciosDeIdentidad(this IServiceCollection services)
        {
            // Configura la autenticación mediante tokens Bearer para validar la identidad de los usuarios en las solicitudes HTTP.
            services.AddAuthentication(IdentityConstants.BearerScheme).AddBearerToken(IdentityConstants.BearerScheme);

            // Registra las políticas de autorización que permiten controlar el acceso a los recursos de la aplicación.
            services.AddAuthorization(opciones =>
            {
                // Define una política que permite el acceso únicamente a los usuarios que tengan el claim "esadmin".
                opciones.AddPolicy("esadmin", politica => politica.RequireClaim("esadmin"));
            });

            // Registra el contexto de base de datos de Identity y configura su conexión mediante la cadena definida en la configuración de la aplicación.
            services.AddDbContext<DientesLimpiosIdentityDbContext>(options =>
            options.UseSqlServer("name=DientesLimpiosConecctionString"));

            // Configura ASP.NET Core Identity para gestionar los usuarios utilizando el modelo personalizado Usuario.
            services.AddIdentityCore<Usuario>()
                // Configura Entity Framework Core como mecanismo de persistencia de los usuarios y sus datos de identidad.
                .AddEntityFrameworkStores<DientesLimpiosIdentityDbContext>()
                // Habilita los endpoints de API necesarios para las operaciones de identidad, como registro e inicio de sesión.
                .AddApiEndpoints();

            // Registra servicios adicionales relacionados con la gestión de usuarios.
            //services.AddTransient<IServicioUsuarios, ServicioUsuarios>();

            // Permite acceder al contexto HTTP actual desde los servicios de la aplicación.
            //services.AddHttpContextAccessor();
        }

    }
}
