using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.API.DTOs.Pacientes
{
    /// <summary>
    /// Representa los datos de entrada necesarios para registrar un nuevo paciente
    /// mediante los endpoints de la API.
    /// </summary>
    /// <remarks>
    /// Utiliza anotaciones de validación para comprobar que la información recibida
    /// cumpla los requisitos básicos antes de ser procesada por la aplicación.
    /// </remarks>
    public class CrearPacienteDTO
    {
        /// <summary>
        /// Obtiene o establece el nombre del paciente.
        /// </summary>
        /// <remarks>
        /// Es un campo obligatorio y admite una longitud máxima de 250 caracteres.
        /// </remarks>
        [Required]
        [StringLength(250)]
        public required string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece el correo electrónico del paciente.
        /// </summary>
        /// <remarks>
        /// Es un campo obligatorio, admite una longitud máxima de 254 caracteres
        /// y debe cumplir con un formato de correo electrónico válido.
        /// </remarks>
        [Required]
        [StringLength(254)]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
