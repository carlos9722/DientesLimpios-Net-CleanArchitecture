
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using System;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    /// <summary>
    /// Contiene las pruebas unitarias del caso de uso encargado de eliminar
    /// consultorios, verificando su comportamiento ante diferentes escenarios.
    /// </summary>
    [TestClass]
    public class CasoDeUsoBorrarConsultorioTests
    {
        /// <summary>
        /// Repositorio simulado que permite controlar las operaciones
        /// de consulta y eliminación de consultorios durante las pruebas.
        /// </summary>
#pragma warning disable CS8618
        private IRepositorioConsultorios repositorio;

        /// <summary>
        /// Unidad de trabajo simulada que permite verificar las operaciones
        /// de persistencia y reversión de cambios.
        /// </summary>
        private IUnidadDeTrabajo unidadDeTrabajo;

        /// <summary>
        /// Instancia del caso de uso que será evaluada en las pruebas unitarias.
        /// </summary>
        private CasoDeUsoBorrarConsultorio casoDeUso;
#pragma warning restore CS8618

        /// <summary>
        /// Prepara las dependencias necesarias antes de ejecutar cada prueba.
        /// Crea los objetos simulados y los inyecta en el caso de uso.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();

            casoDeUso = new CasoDeUsoBorrarConsultorio(
                repositorio,
                unidadDeTrabajo
            );
        }

        /// <summary>
        /// Verifica que, cuando el consultorio existe, se solicite su eliminación
        /// y se persistan correctamente los cambios.
        /// </summary>
        /// <returns>Una tarea que representa la ejecución asíncrona de la prueba.</returns>
        [TestMethod]
        public async Task Handle_CuandoConsultorioExiste_BorraConsultorioYPersiste()
        {
            // Arrange: prepara el identificador, el comando y el consultorio existente.
            var id = Guid.NewGuid();
            var comando = new ComandoBorrarConsultorio { Id = id };
            var consultorio = new Consultorio("Consultorio A");

            // Configura el repositorio simulado para devolver el consultorio solicitado.
            repositorio.ObtenerPorId(id).Returns(consultorio);

            // Act: ejecuta el caso de uso para eliminar el consultorio.
            await casoDeUso.Handle(comando);

            // Assert: verifica que el repositorio solicite eliminar el consultorio una sola vez.
            await repositorio.Received(1).Borrar(consultorio);

            // Comprueba que la unidad de trabajo persista los cambios una sola vez.
            await unidadDeTrabajo.Received(1).Persistir();
        }

        /// <summary>
        /// Verifica que se lance una excepción cuando el consultorio solicitado
        /// no existe en el repositorio.
        /// </summary>
        /// <returns>Una tarea que representa la ejecución asíncrona de la prueba.</returns>
        [TestMethod]
        public async Task Handle_CuandoConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            // Arrange: prepara un comando con un identificador sin un consultorio asociado.
            var comando = new ComandoBorrarConsultorio
            {
                Id = Guid.NewGuid()
            };

            // Configura el repositorio simulado para devolver un valor nulo.
            repositorio.ObtenerPorId(comando.Id).ReturnsNull();

            // Act y Assert: ejecuta el caso de uso y verifica que lance
            // la excepción correspondiente cuando el consultorio no existe.
            await Assert.ThrowsAsync<ExcepcionNoEncontrado>(
                () => casoDeUso.Handle(comando)
            );
        }

        /// <summary>
        /// Verifica que, cuando ocurre un error durante la eliminación,
        /// se solicite revertir los cambios y se propague la excepción original.
        /// </summary>
        /// <returns>Una tarea que representa la ejecución asíncrona de la prueba.</returns>
        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcion_LlamaAReversarYLanzaExcepcion()
        {
            // Arrange: prepara el identificador, el comando y el consultorio existente.
            var id = Guid.NewGuid();
            var comando = new ComandoBorrarConsultorio { Id = id };
            var consultorio = new Consultorio("Consultorio A");

            // Configura el repositorio simulado para devolver el consultorio existente.
            repositorio.ObtenerPorId(id).Returns(consultorio);

            // Configura el repositorio para simular un error durante la eliminación.
            repositorio.Borrar(consultorio).Throws(
                new InvalidOperationException("Fallo al borrar")
            );

            // Act y Assert: ejecuta el caso de uso y verifica que se propague
            // la excepción generada durante la eliminación.
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => casoDeUso.Handle(comando)
            );

            // Verifica que la unidad de trabajo solicite revertir los cambios una sola vez.
            await unidadDeTrabajo.Received(1).Reversar();
        }
    }
}
