
using DientesLimpios.Identidad.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Identidad
{
    // Separa las responsabilidades de acceso y persistencia de los datos de identidad respecto a las demás capas de la aplicación.
    public class DientesLimpiosIdentityDbContext : IdentityDbContext<Usuario>
    {
        // Recibe las opciones de configuración del contexto de base de datos y las envía a la clase base para inicializar Entity Framework Core.
        public DientesLimpiosIdentityDbContext(DbContextOptions<DientesLimpiosIdentityDbContext> options) :
            base(options)
        {
        }

        // Constructor protegido utilizado por Entity Framework Core en determinados escenarios de creación del contexto.
        protected DientesLimpiosIdentityDbContext()
        {
        }
    }
}
