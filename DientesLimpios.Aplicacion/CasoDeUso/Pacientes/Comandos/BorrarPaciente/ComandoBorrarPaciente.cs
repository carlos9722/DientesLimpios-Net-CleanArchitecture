
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Comandos.BorrarPaciente
{
    /// <summary>
    /// Representa el comando encargado de solicitar la eliminación
    /// de un paciente existente en el sistema.
    /// </summary>
    /// <remarks>
    /// Implementa IRequest para identificarlo como una solicitud
    /// que será enviada al mediador para su procesamiento
    /// y que no requiere retornar un resultado específico.
    /// </remarks>
    public class ComandoBorrarPaciente : IRequest
    {
        /// <summary>
        /// Identificador único (GUID) del paciente que se desea eliminar.
        /// </summary>
        /// <remarks>
        /// Es obligatorio para localizar la entidad correspondiente
        /// en el repositorio antes de ejecutar la eliminación.
        /// </remarks>
        public required Guid Id { get; set; }
    }
}
