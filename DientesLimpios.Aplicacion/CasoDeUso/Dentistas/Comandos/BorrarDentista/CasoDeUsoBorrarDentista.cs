
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Comandos.BorrarDentista
{
    /// <summary>
    /// Gestiona la eliminación de un dentista existente.
    /// </summary>
    /// <remarks>
    /// Coordina la búsqueda del dentista, su eliminación mediante el repositorio
    /// y la persistencia de los cambios mediante la unidad de trabajo.
    /// </remarks>
    public class CasoDeUsoBorrarDentista : IRequestHandler<ComandoBorrarDentista>
    {
        // Dependencia encargada de realizar las operaciones de acceso a los datos del dentista.
        private readonly IRepositorioDentista repositorio;

        // Dependencia encargada de coordinar la persistencia de los cambios en la base de datos.
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        /// <summary>
        /// Inicializa una instancia del caso de uso para eliminar un dentista.
        /// </summary>
        /// <param name="repositorio">
        /// Repositorio encargado de consultar y eliminar la información del dentista.
        /// </param>
        /// <param name="unidadDeTrabajo">
        /// Unidad de trabajo encargada de confirmar o revertir las operaciones de persistencia.
        /// </param>
        public CasoDeUsoBorrarDentista(IRepositorioDentista repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        /// <summary>
        /// Ejecuta el proceso de eliminación de un dentista.
        /// </summary>
        /// <param name="peticion">
        /// Comando que contiene el identificador del dentista que se desea eliminar.
        /// </param>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona del proceso de eliminación.
        /// </returns>
        /// <exception cref="ExcepcionNoEncontrado">
        /// Se lanza cuando no existe un dentista con el identificador proporcionado.
        /// </exception>
        /// <remarks>
        /// Recupera el dentista mediante su identificador y, si existe,
        /// ejecuta su eliminación y persiste los cambios. Si ocurre un error
        /// durante la persistencia, realiza la reversión y propaga la excepción.
        /// </remarks>
        public async Task Handle(ComandoBorrarDentista peticion)
        {
            // Busca el dentista mediante el identificador recibido en el comando.
            var dentista = await repositorio.ObtenerPorId(peticion.Id);

            // Verifica que el dentista exista antes de intentar eliminarlo.
            if (dentista is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            try
            {
                // Solicita al repositorio la eliminación del dentista encontrado.
                await repositorio.Borrar(dentista);

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
