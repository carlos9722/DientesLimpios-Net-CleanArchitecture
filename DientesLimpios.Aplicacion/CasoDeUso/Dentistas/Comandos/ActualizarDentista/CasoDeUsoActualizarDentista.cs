
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Comandos.ActualizarDentista
{
    /// <summary>
    /// Gestiona la actualización de la información de un dentista existente.
    /// </summary>
    /// <remarks>
    /// Coordina la recuperación del dentista, la actualización de sus datos
    /// y la persistencia de los cambios mediante el repositorio y la unidad de trabajo.
    /// </remarks>
    public class CasoDeUsoActualizarDentista : IRequestHandler<ComandoActualizarDentista>
    {
        // Dependencia encargada de realizar las operaciones de acceso a los datos del dentista.
        private readonly IRepositorioDentista repositorio;

        // Dependencia encargada de coordinar la persistencia de los cambios en la base de datos.
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        /// <summary>
        /// Inicializa una instancia del caso de uso para actualizar un dentista.
        /// </summary>
        /// <param name="repositorio">
        /// Repositorio encargado de consultar y actualizar la información del dentista.
        /// </param>
        /// <param name="unidadDeTrabajo">
        /// Unidad de trabajo encargada de confirmar o revertir las operaciones de persistencia.
        /// </param>
        public CasoDeUsoActualizarDentista(IRepositorioDentista repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        /// <summary>
        /// Ejecuta la actualización de los datos de un dentista.
        /// </summary>
        /// <param name="peticion">
        /// Comando que contiene el identificador y los nuevos datos del dentista.
        /// </param>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona del proceso de actualización.
        /// </returns>
        /// <exception cref="ExcepcionNoEncontrado">
        /// Se lanza cuando no existe un dentista con el identificador proporcionado.
        /// </exception>
        /// <remarks>
        /// Recupera el dentista, actualiza su nombre y correo electrónico,
        /// y persiste los cambios. Si ocurre un error durante la persistencia,
        /// ejecuta la reversión de las operaciones y propaga la excepción.
        /// </remarks>
        public async Task Handle(ComandoActualizarDentista peticion)
        {
            // Busca el dentista mediante el identificador recibido en el comando.
            var dentista = await repositorio.ObtenerPorId(peticion.Id);

            // Verifica que el dentista exista antes de intentar actualizar sus datos.
            if (dentista is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            // Actualiza el nombre aplicando las reglas de negocio de la entidad.
            dentista.ActualizarNombre(peticion.Nombre);

            // Crea el objeto de valor Email y actualiza el correo del dentista.
            var email = new Email(peticion.Email);
            dentista.ActualizarEmail(email);

            try
            {
                // Actualiza la información del dentista mediante el repositorio.
                await repositorio.Actualizar(dentista);

                // Confirma y persiste los cambios realizados en la base de datos.
                await unidadDeTrabajo.Persistir();
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
