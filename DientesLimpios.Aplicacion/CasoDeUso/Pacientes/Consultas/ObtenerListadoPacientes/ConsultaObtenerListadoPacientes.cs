using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    /// <summary>
    /// Representa la consulta para obtener el listado paginado de pacientes.
    /// </summary>
    /// <remarks>
    /// Hereda los criterios de filtrado y paginación de FiltroPacienteDTO
    /// y define el tipo de respuesta que será procesado por el mediador.
    /// </remarks>
    public class ConsultaObtenerListadoPacientes : FiltroPacienteDTO, IRequest<PaginadoDTO<PacienteListadoDTO>>
    {

    }
}