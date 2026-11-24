
namespace DientesLimpios.API.DTOs.Citas
{
    /// <summary>
    /// Representa los datos necesarios para registrar una nueva cita
    /// en el sistema.
    /// </summary>
    public class CrearCitaDTO
    {
        /// <summary>
        /// Identificador del paciente asociado a la cita.
        /// </summary>
        public Guid PacienteId { get; set; }

        /// <summary>
        /// Identificador del dentista asignado a la cita.
        /// </summary>
        public Guid DentistaId { get; set; }

        /// <summary>
        /// Identificador del consultorio donde se realizará la cita.
        /// </summary>
        public Guid ConsultorioId { get; set; }

        /// <summary>
        /// Fecha y hora de inicio de la cita.
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha y hora de finalización de la cita.
        /// </summary>
        public DateTime FechaFin { get; set; }
    }
}
