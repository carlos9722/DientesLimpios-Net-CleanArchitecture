using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    /// <summary>
    /// Contiene las pruebas unitarias del caso de uso encargado de obtener
    /// el listado de consultorios registrados.
    /// </summary>
    [TestClass]
    public class CasoDeUsoObtenerListadoConsultoriosTests
    {
        /// <summary>
        /// Simula el repositorio de consultorios para controlar los datos
        /// utilizados durante las pruebas, sin depender de una base de datos.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IRepositorioConsultorios repositorio;

        /// <summary>
        /// Instancia del caso de uso que será evaluado en las pruebas.
        /// </summary>
        private CasoDeUsoObtenerListadoConsultorios casoDeUso;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// Prepara los objetos necesarios antes de ejecutar cada prueba.
        /// Crea un repositorio simulado y lo inyecta en el caso de uso.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            casoDeUso = new CasoDeUsoObtenerListadoConsultorios(repositorio);
        }

        /// <summary>
        /// Verifica que el caso de uso retorne correctamente una lista de
        /// consultorios convertidos al DTO de listado cuando existen registros.
        /// </summary>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona de la prueba.
        /// </returns>
        [TestMethod]
        public async Task Handle_CuandoHayConsultorios_RetornaListaDeConsultorioListadoDTO()
        {
            // Arrange: prepara los consultorios que devolverá el repositorio.
            var consultorios = new List<Consultorio>
                {
                    new Consultorio( "Consultorio A"),
                    new Consultorio( "Consultorio B"),
                };

            // Configura el repositorio simulado para retornar los consultorios preparados.
            repositorio.ObtenerTodos().Returns(consultorios);

            // Construye la lista esperada, convirtiendo cada entidad en su DTO correspondiente.
            var esperado = consultorios.Select(c =>
            new ConsultorioListadoDTO { Id = c.Id, Nombre = c.Nombre }).ToList();

            // Act: ejecuta el caso de uso para obtener el listado de consultorios.
            var resultado = await casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());

            // Assert: verifica que ambas listas tengan la misma cantidad de elementos.
            Assert.AreEqual(esperado.Count, resultado.Count);

            // Comprueba que cada consultorio tenga el mismo identificador y nombre esperado.
            for (int i = 0; i < esperado.Count; i++)
            {
                Assert.AreEqual(esperado[i].Id, resultado[i].Id);
                Assert.AreEqual(esperado[i].Nombre, resultado[i].Nombre);
            }
        }

        /// <summary>
        /// Verifica que el caso de uso retorne una lista vacía cuando
        /// el repositorio no encuentra consultorios registrados.
        /// </summary>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona de la prueba.
        /// </returns>
        [TestMethod]
        public async Task Handle_CuandoNoHayConsultorios_RetornaListaVacia()
        {
            // Arrange: configura el repositorio para retornar una lista vacía.
            repositorio.ObtenerTodos().Returns(new List<Consultorio>());

            // Act: ejecuta el caso de uso para obtener el listado de consultorios.
            var resultado = await casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());

            // Assert: verifica que el resultado no sea nulo y que no contenga elementos.
            Assert.IsNotNull(resultado);
            Assert.AreEqual(0, resultado.Count);
        }
    }
}
