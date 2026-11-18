
using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.API.DTOs.Pacientes
{
    /// <summary>
    /// Representa los datos necesarios para actualizar la información
    /// de un paciente existente desde la API.
    /// </summary>
    /// <remarks>
    /// Incluye reglas de validación para garantizar que los datos recibidos
    /// cumplan con los requisitos establecidos antes de procesar la solicitud.
    /// </remarks>
    public class ActualizarPacienteDTO
    {
        /// <summary>
        /// Nombre del paciente que se desea actualizar.
        /// </summary>
        /// <remarks>
        /// Es un campo obligatorio y admite una longitud máxima de 250 caracteres.
        /// </remarks>
        [Required]
        [StringLength(250)]
        public required string Nombre { get; set; }

        /// <summary>
        /// Dirección de correo electrónico del paciente.
        /// </summary>
        /// <remarks>
        /// Es un campo obligatorio, debe tener un formato de correo electrónico
        /// válido y admite una longitud máxima de 254 caracteres.
        /// </remarks>
        [Required]
        [StringLength(254)]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
