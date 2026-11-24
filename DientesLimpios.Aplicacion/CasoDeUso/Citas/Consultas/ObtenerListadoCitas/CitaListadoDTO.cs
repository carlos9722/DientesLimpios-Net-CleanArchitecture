
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.Consultas.ObtenerListadoCitas
{
    /// <summary>
    /// Representa los datos de una cita para mostrarla en el listado
    /// de citas registradas en el sistema.
    /// </summary>
    public class CitaListadoDTO
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
        /// Nombre del dentista asignado a la cita.
        /// </summary>
        public required string Dentista { get; set; }

        /// <summary>
        /// Nombre del consultorio donde se realizará la cita.
        /// </summary>
        public required string Consultorio { get; set; }

        /// <summary>
        /// Fecha y hora de inicio de la cita.
        /// </summary>
        public required DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha y hora de finalización de la cita.
        /// </summary>
        public required DateTime FechaFin { get; set; }

        /// <summary>
        /// Estado actual de la cita.
        /// </summary>
        public required string EstadoCita { get; set; }
    }
}
