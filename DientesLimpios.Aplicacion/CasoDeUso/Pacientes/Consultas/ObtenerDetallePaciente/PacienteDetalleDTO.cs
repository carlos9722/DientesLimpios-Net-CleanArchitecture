
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerDetallePaciente
{
    /// <summary>
    /// Representa el objeto de transferencia de datos (DTO) utilizado
    /// para transportar la información detallada de un paciente.
    /// </summary>
    /// <remarks>
    /// Contiene únicamente los datos necesarios para presentar
    /// la información del paciente sin exponer directamente
    /// la entidad del dominio.
    /// </remarks>
    public class PacienteDetalleDTO
    {
        /// <summary>
        /// Identificador único (GUID) del paciente.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre completo del paciente.
        /// </summary>
        /// <remarks>
        /// Es una propiedad obligatoria y debe contener un valor al
        /// inicializar una instancia de esta clase.
        /// </remarks>
        public required string Nombre { get; set; }

        /// <summary>
        /// Dirección de correo electrónico del paciente.
        /// </summary>
        /// <remarks>
        /// Es una propiedad obligatoria y debe contener un valor al
        /// inicializar una instancia de esta clase.
        /// </remarks>
        public required string Email { get; set; }
    }
}
