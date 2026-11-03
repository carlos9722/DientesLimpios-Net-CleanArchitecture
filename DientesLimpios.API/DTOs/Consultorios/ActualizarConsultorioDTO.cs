
using System.ComponentModel.DataAnnotations;

namespace DientesLimpios.API.DTOs.Consultorios
{
    /// <summary>
    /// Representa los datos necesarios para actualizar la información de un consultorio.
    /// </summary>
    public class ActualizarConsultorioDTO
    {
        /// <summary>
        /// Nombre del consultorio que se desea actualizar.
        /// Es un campo obligatorio y admite una longitud máxima de 150 caracteres.
        /// </summary>
        [Required]
        [StringLength(150)]
        public required string Nombre { get; set; }
    }
}
