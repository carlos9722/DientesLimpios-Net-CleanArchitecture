
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerDetallePaciente
{
    /// <summary>
    /// Representa la consulta para obtener el detalle de un paciente
    /// mediante su identificador único.
    /// </summary>
    /// <remarks>
    /// Implementa IRequest para indicar que esta consulta espera
    /// como resultado un objeto de tipo PacienteDetalleDTO.
    /// </remarks>
    public class ConsultaObtenerDetallePaciente : IRequest<PacienteDetalleDTO>
    {
        /// <summary>
        /// Identificador único (GUID) del paciente que se desea consultar.
        /// </summary>
        /// <remarks>
        /// Es obligatorio proporcionar este valor para ejecutar la consulta.
        /// </remarks>
        public required Guid Id { get; set; }
    }
}
