
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.CrearDentista;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Comandos.CrearDentista
{
    /// <summary>
    /// Gestiona la creación de un nuevo dentista dentro de la aplicación.
    /// </summary>
    /// <remarks>
    /// Coordina la creación del objeto de valor Email, la construcción de la entidad
    /// Dentista y la persistencia de la información mediante el repositorio
    /// y la unidad de trabajo.
    /// </remarks>
    public class CasoDeUsoCrearDentista : IRequestHandler<ComandoCrearDentista, Guid>
    {
        // Dependencia encargada de realizar las operaciones de acceso a los datos del dentista.
        private readonly IRepositorioDentista repositorio;

        // Dependencia encargada de coordinar la persistencia de los cambios en la base de datos.
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        /// <summary>
        /// Inicializa una instancia del caso de uso para crear un dentista.
        /// </summary>
        /// <param name="repositorio">
        /// Repositorio encargado de agregar la información del dentista.
        /// </param>
        /// <param name="unidadDeTrabajo">
        /// Unidad de trabajo encargada de confirmar o revertir las operaciones de persistencia.
        /// </param>
        public CasoDeUsoCrearDentista(IRepositorioDentista repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        /// <summary>
        /// Ejecuta el proceso de creación de un nuevo dentista.
        /// </summary>
        /// <param name="peticion">
        /// Comando que contiene el nombre y el correo electrónico del dentista.
        /// </param>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona y devuelve el identificador
        /// único del dentista creado.
        /// </returns>
        /// <remarks>
        /// Construye la entidad Dentista aplicando las reglas de negocio del dominio,
        /// registra la información mediante el repositorio y persiste los cambios.
        /// Si ocurre un error durante la operación, ejecuta la reversión
        /// y propaga la excepción.
        /// </remarks>
        public async Task<Guid> Handle(ComandoCrearDentista peticion)
        {
            // Crea el objeto de valor Email a partir del correo recibido en el comando.
            var email = new Email(peticion.Email);

            // Construye la entidad Dentista aplicando las reglas de negocio del dominio.
            var dentista = new Dentista(peticion.Nombre, email);

            try
            {
                // Agrega el nuevo dentista mediante el repositorio y obtiene la entidad creada.
                var respuesta = await repositorio.Agregar(dentista);

                // Confirma y persiste los cambios realizados en la base de datos.
                await unidadDeTrabajo.Persistir();

                // Devuelve el identificador único del dentista creado.
                return respuesta.Id;
            }
            catch (Exception)
            {
                // Revierte las operaciones de persistencia cuando ocurre un error.
                await unidadDeTrabajo.Reversar();

                // Propaga la excepción para que pueda ser gestionada por una capa superior.
                throw;
            }
        }
    }
}
