
using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Pacientes
{
    /// <summary>
    /// Contiene las pruebas unitarias del caso de uso encargado de obtener
    /// el listado de pacientes con soporte para paginación.
    /// </summary>
    [TestClass]
    public class CasoDeUsoObtenerListadoPacientesTests
    {
        // Se deshabilita temporalmente la advertencia de nulabilidad porque
        // estos campos se inicializan en el método Setup antes de cada prueba.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// Repositorio simulado que permite controlar las respuestas
        /// utilizadas durante las pruebas, sin acceder a una base de datos real.
        /// </summary>
        private IRepositorioPacientes repositorio;

        /// <summary>
        /// Instancia del caso de uso que se desea probar.
        /// </summary>
        private CasoDeUsoObtenerListadoPacientes casoDeUso;

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// Prepara las dependencias necesarias antes de ejecutar cada prueba.
        /// Crea un repositorio simulado y lo inyecta en el caso de uso.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioPacientes>();
            casoDeUso = new CasoDeUsoObtenerListadoPacientes(repositorio);
        }

        /// <summary>
        /// Verifica que el caso de uso retorne correctamente los pacientes
        /// y el total de registros cuando se solicita un listado paginado.
        /// </summary>
        /// <returns>Una tarea que representa la ejecución asíncrona de la prueba.</returns>
        [TestMethod]
        public async Task Handle_RetornaPacientesPaginadosCorrectamente()
        {
            // Arrange: se definen los parámetros de paginación.
            var pagina = 1;
            var registrosPorPagina = 2;

            var filtroPacienteDTO = new FiltroPacienteDTO { Pagina = pagina, RegistrosPorPagina = registrosPorPagina };

            // Se crean los pacientes que devolverá el repositorio simulado.
            var paciente1 = new Paciente("Felipe", new Email("felipe@ejemplo.com"));
            var paciente2 = new Paciente("Claudia", new Email("claudia@ejemplo.com"));

            IEnumerable<Paciente> pacientes = new List<Paciente> { paciente1, paciente2 };

            // Se configura el comportamiento del repositorio utilizando NSubstitute.
            repositorio.ObtenerFiltrado(Arg.Any<FiltroPacienteDTO>()).Returns(Task.FromResult(pacientes));

            // Se simula la cantidad total de registros disponibles.
            repositorio.ObtenerCantitadTotalRegistros().Returns(Task.FromResult(10));

            // Se construye la solicitud que recibirá el caso de uso.
            var request = new ConsultaObtenerListadoPacientes
            {
                Pagina = pagina,
                RegistrosPorPagina = registrosPorPagina
            };

            // Act: se ejecuta el caso de uso de manera asíncrona.
            var resultado = await casoDeUso.Handle(request);

            // Assert: se verifica el total de registros y los pacientes retornados.
            Assert.AreEqual(10, resultado.Total);
            Assert.AreEqual(2, resultado.Elementos.Count);
            Assert.AreEqual("Felipe", resultado.Elementos[0].Nombre);
            Assert.AreEqual("Claudia", resultado.Elementos[1].Nombre);

        }

        /// <summary>
        /// Verifica que el caso de uso retorne una colección vacía
        /// y un total de cero cuando no existen pacientes registrados.
        /// </summary>
        /// <returns>Una tarea que representa la ejecución asíncrona de la prueba.</returns>
        [TestMethod]
        public async Task Handle_CuandoNoHayPacientes_RetornaListaVaciaYTotalCero()
        {
            // Arrange: se definen los parámetros de paginación.
            var pagina = 1;
            var registrosPorPagina = 5;

            var filtroPacienteDTO = new FiltroPacienteDTO { Pagina = pagina, RegistrosPorPagina = registrosPorPagina };

            // Se configura una colección vacía para simular que no existen pacientes.
            IEnumerable<Paciente> pacientes = new List<Paciente>();

            // Se configura el repositorio para retornar la colección vacía.
            repositorio.ObtenerFiltrado(Arg.Any<FiltroPacienteDTO>()).Returns(Task.FromResult(pacientes));

            // Se simula que no existen registros en el repositorio.
            repositorio.ObtenerCantitadTotalRegistros()
                    .Returns(Task.FromResult(0));

            // Se construye la solicitud que recibirá el caso de uso.
            var request = new ConsultaObtenerListadoPacientes
            {
                Pagina = pagina,
                RegistrosPorPagina = registrosPorPagina
            };

            // Act: se ejecuta el caso de uso de manera asíncrona.
            var resultado = await casoDeUso.Handle(request);

            // Assert: se verifica que el total sea cero y la colección esté vacía.
            Assert.AreEqual(0, resultado.Total);
            Assert.IsNotNull(resultado.Elementos);
            Assert.AreEqual(0, resultado.Elementos.Count);
        }
    }
}
