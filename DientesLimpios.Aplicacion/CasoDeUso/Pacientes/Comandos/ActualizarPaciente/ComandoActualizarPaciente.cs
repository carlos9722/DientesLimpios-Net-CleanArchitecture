
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente
{
    /// <summary>
    /// Representa el comando encargado de solicitar la actualización
    /// de la información de un paciente existente.
    /// </summary>
    /// <remarks>
    /// Implementa IRequest para indicar que representa una operación
    /// que se envía al mediador para su procesamiento y no requiere
    /// retornar un resultado específico.
    /// </remarks>
    public class ComandoActualizarPaciente : IRequest
    {
        /// <summary>
        /// Identificador único (GUID) del paciente que se desea actualizar.
        /// </summary>
        /// <remarks>
        /// Es obligatorio para localizar la entidad correspondiente
        /// en el repositorio.
        /// </remarks>
        public required Guid Id { get; set; }

        /// <summary>
        /// Nuevo nombre que se asignará al paciente.
        /// </summary>
        /// <remarks>
        /// Es un dato obligatorio y debe cumplir las reglas de validación
        /// definidas para el comando.
        /// </remarks>
        public required string Nombre { get; set; }

        /// <summary>
        /// Nueva dirección de correo electrónico que se asignará al paciente.
        /// </summary>
        /// <remarks>
        /// Es un dato obligatorio y debe cumplir con el formato y la longitud
        /// establecidos por las reglas de validación.
        /// </remarks>
        public required string Email { get; set; }
    }
}
