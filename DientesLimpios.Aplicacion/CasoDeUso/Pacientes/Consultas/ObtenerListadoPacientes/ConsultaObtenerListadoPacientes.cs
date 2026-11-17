using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    /// <summary>
    /// Representa la solicitud para obtener el listado de pacientes registrados
    /// en el sistema.
    /// </summary>
    /// <remarks>
    /// Define una consulta que solicita al mediador una colección de objetos
    /// PacienteListadoDTO con la información de los pacientes.
    /// No requiere parámetros adicionales para su ejecución.
    /// </remarks>
    public class ConsultaObtenerListadoPacientes : IRequest<List<PacienteListadoDTO>>
    {

    }
}
