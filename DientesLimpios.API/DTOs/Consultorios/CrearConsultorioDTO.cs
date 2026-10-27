
using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.API.DTOs.Consultorios
{
    /// <summary>
    /// Representa los datos necesarios para crear un consultorio.
    /// </summary>
    public class CrearConsultorioDTO
    {
        /// <summary>
        /// Nombre del consultorio. Es obligatorio y admite hasta 150 caracteres.
        /// </summary>
        [Required]
        [StringLength(150)]
        public required string Nombre { get; set; }
    }
}
