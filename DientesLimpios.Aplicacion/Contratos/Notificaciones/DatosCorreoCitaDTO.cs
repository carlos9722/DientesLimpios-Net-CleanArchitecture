
namespace DientesLimpios.Aplicacion.Contratos.Notificaciones
{
    /// <summary>
    /// Contiene la información general de una cita necesaria para enviar notificaciones por correo electrónico.
    /// </summary>
    public class DatosCorreoCitaDTO
    {
        /// <summary>
        /// Identificador único de la cita.
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Nombre del paciente asociado a la cita.
        /// </summary>
        public required string Paciente { get; set; }

        /// <summary>
        /// Dirección de correo electrónico del paciente.
        /// </summary>
        public required string Paciente_Email { get; set; }

        /// <summary>
        /// Nombre del dentista encargado de la cita.
        /// </summary>
        public required string Dentista { get; set; }

        /// <summary>
        /// Nombre del consultorio donde se realizará la cita.
        /// </summary>
        public required string Consultorio { get; set; }

        /// <summary>
        /// Fecha y hora programadas para la cita.
        /// </summary>
        public required DateTime Fecha { get; set; }
    }
}
