using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Pacientes
{
    /// <summary>
    /// Contiene las pruebas unitarias del caso de uso encargado
    /// de crear pacientes.
    /// </summary>
    /// <remarks>
    /// Utiliza MSTest para ejecutar las pruebas y NSubstitute
    /// para simular las dependencias del repositorio y la unidad de trabajo,
    /// permitiendo comprobar el comportamiento del caso de uso de forma aislada.
    /// </remarks>
    [TestClass]
    public class CasoDeUsoCrearPacienteTests
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// Repositorio simulado utilizado para comprobar las operaciones
        /// de persistencia de pacientes.
        /// </summary>
        private IRepositorioPacientes repositorio;

        /// <summary>
        /// Unidad de trabajo simulada utilizada para verificar
        /// la confirmación o reversión de los cambios.
        /// </summary>
        private IUnidadDeTrabajo unidadDeTrabajo;

        /// <summary>
        /// Instancia del caso de uso que será evaluada en las pruebas.
        /// </summary>
        private CasoDeUsoCrearPaciente casoDeUso;

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// Inicializa las dependencias simuladas y crea una nueva instancia
        /// del caso de uso antes de ejecutar cada prueba.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            // Crea sustitutos de las dependencias para evitar acceder
            // a implementaciones reales de persistencia.
            repositorio = Substitute.For<IRepositorioPacientes>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();

            // Construye el caso de uso utilizando las dependencias simuladas.
            casoDeUso = new CasoDeUsoCrearPaciente(repositorio, unidadDeTrabajo);
        }

        /// <summary>
        /// Verifica que, cuando se proporcionan datos válidos,
        /// el paciente se agrega al repositorio, se persisten los cambios
        /// y se retorna su identificador.
        /// </summary>
        [TestMethod]
        public async Task Handle_CuandoDatosValidos_CreaPacienteYPersisteYRetornaId()
        {
            // Prepara un comando con los datos necesarios para crear un paciente.
            var comando = new ComandoCrearPaciente
            {
                Nombre = "Felipe",
                Email = "felipe@ejemplo.com"
            };

            // Crea el paciente que será devuelto por el repositorio simulado.
            var pacienteCreado = new Paciente(comando.Nombre, new Email(comando.Email));
            var id = pacienteCreado.Id;

            // Configura el repositorio para devolver el paciente al agregarlo.
            repositorio.Agregar(Arg.Any<Paciente>()).Returns(pacienteCreado);

            // Ejecuta el caso de uso con el comando preparado.
            var idResultado = await casoDeUso.Handle(comando);

            // Comprueba que el identificador retornado corresponda al paciente creado.
            Assert.AreEqual(id, idResultado);

            // Verifica que el repositorio haya recibido una única solicitud para agregar el paciente.
            await repositorio.Received(1).Agregar(Arg.Any<Paciente>());

            // Verifica que la unidad de trabajo haya confirmado los cambios una sola vez.
            await unidadDeTrabajo.Received(1).Persistir();

        }

        /// <summary>
        /// Verifica que, cuando ocurre una excepción durante la operación
        /// de persistencia, el caso de uso revierta los cambios y propague
        /// la excepción al componente que realizó la solicitud.
        /// </summary>
        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcion_ReversarYLanzaExcepcion()
        {
            // Prepara un comando con datos válidos para iniciar la operación.
            var comando = new ComandoCrearPaciente
            {
                Nombre = "Felipe",
                Email = "felipe@ejemplo.com"
            };

            // Configura el repositorio para simular un error durante la inserción.
            repositorio.Agregar(Arg.Any<Paciente>())
                .Throws(new InvalidOperationException("Error al insertar"));

            // Ejecuta el caso de uso y comprueba que la excepción se propague.
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => casoDeUso.Handle(comando)
            );

            // Verifica que la unidad de trabajo haya intentado revertir los cambios.
            await unidadDeTrabajo.Received(1).Reversar();

            // Comprueba que no se haya intentado confirmar la operación.
            await unidadDeTrabajo.DidNotReceive().Persistir();
        }

    }
}
