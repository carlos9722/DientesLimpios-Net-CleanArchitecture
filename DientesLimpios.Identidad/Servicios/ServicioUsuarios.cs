
using DientesLimpios.Aplicacion.Contratos.Identidad;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace DientesLimpios.Identidad.Servicios
{
    // Proporciona funcionalidades para consultar información del usuario autenticado durante una solicitud HTTP.
    public class ServicioUsuarios : IServicioUsuarios
    {
        // Permite acceder al contexto HTTP actual y consultar la información del usuario que realiza la solicitud.
        private readonly IHttpContextAccessor httpContextAccessor;

        // Recibe el acceso al contexto HTTP mediante inyección de dependencias.
        public ServicioUsuarios(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        // Obtiene el identificador único del usuario autenticado a partir de sus claims.
        public string ObtenerUsuarioId()
        {
            return httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
    }
}
