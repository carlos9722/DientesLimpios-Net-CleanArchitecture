
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.CrearCita
{
    /// <summary>
    /// Representa la solicitud para crear una nueva cita y obtener
    /// su identificador como resultado.
    /// </summary>
    public class ComandoCrearCita : IRequest<Guid>
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
