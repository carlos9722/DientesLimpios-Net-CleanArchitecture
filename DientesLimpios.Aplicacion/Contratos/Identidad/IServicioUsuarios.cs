
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.Contratos.Identidad
{
    // Define el contrato para los servicios encargados de consultar información del usuario autenticado.
    public interface IServicioUsuarios
    {
        // Obtiene el identificador único del usuario que realiza la solicitud actual.
        public string ObtenerUsuarioId();
    }
}
