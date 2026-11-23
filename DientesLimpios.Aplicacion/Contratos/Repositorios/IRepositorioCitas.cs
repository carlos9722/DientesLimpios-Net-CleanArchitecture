
using DientesLimpios.Aplicacion.Contratos.Repositorios.Modelos;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.Contratos.Repositorios
{
    /// <summary>
    /// Define las operaciones de acceso a datos específicas para
    /// la gestión y consulta de citas.
    /// </summary>
    public interface IRepositorioCitas : IRepositorio<Cita>
    {
        /// <summary>
        /// Verifica si existe una cita programada del dentista
        /// que se solape con el intervalo de tiempo indicado.
        /// </summary>
        /// <param name="dentistaId">
        /// Identificador del dentista cuya disponibilidad se desea verificar.
        /// </param>
        /// <param name="inicio">
        /// Fecha y hora de inicio del intervalo que se desea validar.
        /// </param>
        /// <param name="fin">
        /// Fecha y hora de finalización del intervalo que se desea validar.
        /// </param>
        /// <returns>
        /// True si existe una cita solapada; de lo contrario, false.
        /// </returns>
        Task<bool> ExisteCitaSolapada(Guid dentistaId, DateTime inicio, DateTime fin);

        /// <summary>
        /// Obtiene una cita por su identificador, incluyendo la información
        /// del paciente, dentista y consultorio relacionados.
        /// </summary>
        /// <param name="id">
        /// Identificador único de la cita que se desea consultar.
        /// </param>
        /// <returns>
        /// La cita encontrada o null si no existe una cita con el identificador proporcionado.
        /// </returns>
        new Task<Cita?> ObtenerPorId(Guid id);

        /// <summary>
        /// Obtiene las citas que cumplen con los criterios de búsqueda
        /// especificados, ordenadas por fecha de inicio.
        /// </summary>
        /// <param name="filtroCitasDTO">
        /// Objeto que contiene los filtros opcionales y el rango de fechas
        /// para realizar la búsqueda.
        /// </param>
        /// <returns>
        /// Colección de citas que cumplen con los filtros establecidos.
        /// </returns>
        Task<IEnumerable<Cita>> ObtenerFiltrado(FiltroCitasDTO filtroCitasDTO);
    }
}
