
using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.API.DTOs.Dentistas
{
    /// <summary>
    /// Representa los datos de entrada necesarios para registrar un nuevo dentista desde la API.
    /// </summary>
    /// <remarks>
    /// Define las propiedades y las reglas de validación mediante Data Annotations,
    /// permitiendo verificar los datos recibidos antes de enviarlos a la capa de aplicación.
    /// </remarks>
    public class CrearDentistaDTO
    {
        /// <summary>
        /// Obtiene o establece el nombre del dentista que se desea registrar.
        /// </summary>
        /// <remarks>
        /// Es obligatorio y admite una longitud máxima de 250 caracteres.
        /// </remarks>
        [Required]
        [StringLength(250)]
        public required string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece el correo electrónico del dentista que se desea registrar.
        /// </summary>
        /// <remarks>
        /// Es obligatorio, admite una longitud máxima de 254 caracteres
        /// y debe cumplir con un formato de correo electrónico válido.
        /// </remarks>
        [Required]
        [StringLength(254)]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
