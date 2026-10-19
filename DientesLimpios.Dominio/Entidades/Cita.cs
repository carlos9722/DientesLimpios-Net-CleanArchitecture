using DientesLimpios.Dominio.Enums;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;

namespace DientesLimpios.Dominio.Entidades
{
    /// <summary>
    /// Representa una cita odontológica y controla sus cambios de estado.
    /// </summary>
    public class Cita
    {
        public Guid Id { get; private set; }
        public Guid PacienteId { get; private set; }
        public Guid DentistaId { get; private set; }
        public Guid ConsultorioId { get; private set; }
        public EstadoCita Estado { get; private set; }
        public IntervaloDeTiempo IntervaloDeTiempo { get; private set; }

        // Referencias opcionales a las entidades relacionadas, útiles al consultar la cita.
        public Paciente? Paciente { get; private set; }
        public Dentista? Dentista { get; private set; }
        public Consultorio? Consultorio { get; private set; }

        /// <summary>
        /// Crea una cita programada y valida que su inicio no esté en el pasado.
        /// </summary>
        /// <param name="pacienteId">Identificador del paciente.</param>
        /// <param name="dentistaId">Identificador del dentista.</param>
        /// <param name="consultorioId">Identificador del consultorio.</param>
        /// <param name="intervaloDeTiempo">Intervalo de inicio y fin de la cita.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza si la cita comienza antes de la fecha y hora actuales.
        /// </exception>
        public Cita(Guid pacienteId, Guid dentistaId, Guid consultorioId, IntervaloDeTiempo intervaloDeTiempo)
        {
            // Impide programar citas cuyo inicio ya haya pasado.
            if (intervaloDeTiempo.Inicio < DateTime.UtcNow)
            {
                throw new ExcepcionDeReglaDeNegocio("La fecha de inicio no puede ser anterior a la fecha actual");
            }

            PacienteId = pacienteId;
            DentistaId = dentistaId;
            ConsultorioId = consultorioId;
            IntervaloDeTiempo = intervaloDeTiempo;

            // Toda cita nueva comienza en estado programado y recibe un identificador único.
            Estado = EstadoCita.Programada;
            Id = Guid.CreateVersion7();
        }

        /// <summary>
        /// Cancela la cita si todavía está programada.
        /// </summary>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza si la cita no está programada.
        /// </exception>
        public void Cancelar()
        {
            // Solo se permite cancelar citas que aún no han cambiado de estado.
            if (Estado != EstadoCita.Programada)
            {
                throw new ExcepcionDeReglaDeNegocio("Solo se pueden cancelar citas programadas.");
            }

            Estado = EstadoCita.Cancelada;
        }

        /// <summary>
        /// Marca la cita como completada si todavía está programada.
        /// </summary>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza si la cita no está programada.
        /// </exception>
        public void Completar()
        {
            // Evita completar citas canceladas o que ya fueron completadas.
            if (Estado != EstadoCita.Programada)
            {
                throw new ExcepcionDeReglaDeNegocio("Solo se pueden completar citas programadas.");
            }

            Estado = EstadoCita.Completada;
        }
    }
}