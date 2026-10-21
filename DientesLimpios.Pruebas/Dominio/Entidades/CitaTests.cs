using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Enums;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Pruebas.Dominio.Entidades
{
    /// <summary>
    /// Verifica la creación de citas y las reglas de cambio de estado.
    /// </summary>
    [TestClass]
    public class CitaTests
    {
        // Datos reutilizados para evitar repetir la preparación en cada prueba.
        private Guid _pacienteId = Guid.NewGuid();
        private Guid _dentistaId = Guid.NewGuid();
        private Guid _consultorioId = Guid.NewGuid();

        // Intervalo futuro válido para crear una cita.
        private IntervaloDeTiempo _intervalo = new IntervaloDeTiempo(
            DateTime.UtcNow.AddDays(1),
            DateTime.UtcNow.AddDays(2)
        );

        /// <summary>
        /// Comprueba que una cita válida se cree con los datos recibidos
        /// y comience en estado programado.
        /// </summary>
        [TestMethod]
        public void Constructor_CitaValida_EstadoEsProgramada()
        {
            var cita = new Cita(
                _pacienteId,
                _dentistaId,
                _consultorioId,
                _intervalo
            );

            Assert.AreEqual(_pacienteId, cita.PacienteId);
            Assert.AreEqual(_dentistaId, cita.DentistaId);
            Assert.AreEqual(_consultorioId, cita.ConsultorioId);
            Assert.AreEqual(_intervalo, cita.IntervaloDeTiempo);
            Assert.AreEqual(EstadoCita.Programada, cita.Estado);
            Assert.AreNotEqual(Guid.Empty, cita.Id);
        }

        /// <summary>
        /// Comprueba que no se pueda crear una cita cuyo inicio esté en el pasado.
        /// </summary>
        [TestMethod]
        public void Constructor_FechaInicioEnElPasado_LanzaExcepcion()
        {
            var intervalo = new IntervaloDeTiempo(
                DateTime.UtcNow.AddDays(-1),
                DateTime.UtcNow
            );

            Assert.Throws<ExcepcionDeReglaDeNegocio>(
                () => new Cita(
                    _pacienteId,
                    _dentistaId,
                    _consultorioId,
                    intervalo
                )
            );
        }

        /// <summary>
        /// Comprueba que una cita programada pueda cancelarse.
        /// </summary>
        [TestMethod]
        public void Cancelar_CitaProgramada_CambiaEstadoACancelada()
        {
            var cita = new Cita(
                _pacienteId,
                _dentistaId,
                _consultorioId,
                _intervalo
            );

            cita.Cancelar();

            Assert.AreEqual(EstadoCita.Cancelada, cita.Estado);
        }

        /// <summary>
        /// Comprueba que una cita cancelada no pueda cancelarse nuevamente.
        /// </summary>
        [TestMethod]
        public void Cancelar_CitaNoProgramada_LanzaExcepcion()
        {
            var cita = new Cita(
                _pacienteId,
                _dentistaId,
                _consultorioId,
                _intervalo
            );

            cita.Cancelar();

            Assert.Throws<ExcepcionDeReglaDeNegocio>(
                () => cita.Cancelar()
            );
        }

        /// <summary>
        /// Comprueba que una cita programada pueda marcarse como completada.
        /// </summary>
        [TestMethod]
        public void Completar_CitaProgramada_CambiaEstadoACompletada()
        {
            var cita = new Cita(
                _pacienteId,
                _dentistaId,
                _consultorioId,
                _intervalo
            );

            cita.Completar();

            Assert.AreEqual(EstadoCita.Completada, cita.Estado);
        }

        /// <summary>
        /// Comprueba que una cita cancelada no pueda completarse.
        /// </summary>
        [TestMethod]
        public void Completar_CitaCancelada_LanzaExcepcion()
        {
            var cita = new Cita(
                _pacienteId,
                _dentistaId,
                _consultorioId,
                _intervalo
            );

            cita.Cancelar();

            Assert.Throws<ExcepcionDeReglaDeNegocio>(
                () => cita.Completar()
            );
        }
    }
}