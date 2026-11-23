
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Contratos.Repositorios.Modelos;
using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Persistencia.Repositorio
{
    /// <summary>
    /// Implementa las operaciones de acceso a datos para gestionar las citas
    /// mediante Entity Framework Core.
    /// </summary>
    public class RepositorioCitas : Repositorio<Cita>, IRepositorioCitas
    {
        /// <summary>
        /// Contexto de Entity Framework Core utilizado para acceder
        /// a las citas y sus entidades relacionadas.
        /// </summary>
        private readonly DientesLimpiosDbContext context;

        /// <summary>
        /// Inicializa el repositorio de citas utilizando el contexto
        /// de base de datos recibido.
        /// </summary>
        /// <param name="context">
        /// Contexto de Entity Framework Core para acceder a los datos.
        /// </param>
        public RepositorioCitas(DientesLimpiosDbContext context)
            : base(context)
        {
            this.context = context;
        }

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
        /// True si existe una cita programada que se solapa con el intervalo;
        /// de lo contrario, false.
        /// </returns>
        public async Task<bool> ExisteCitaSolapada(Guid dentistaId, DateTime inicio, DateTime fin)
        {
            return await context.Citas
                .Where(x => x.DentistaId == dentistaId && x.Estado == Dominio.Enums.EstadoCita.Programada &&
                inicio < x.IntervaloDeTiempo.Fin && fin > x.IntervaloDeTiempo.Inicio
                ).AnyAsync();
        }

        /// <summary>
        /// Obtiene las citas que cumplen con los criterios de búsqueda
        /// especificados, incluyendo la información del paciente,
        /// dentista y consultorio asociados.
        /// </summary>
        /// <param name="filtroCitasDTO">
        /// Objeto que contiene los filtros opcionales y el rango de fechas
        /// para realizar la búsqueda de citas.
        /// </param>
        /// <returns>
        /// Colección de citas que cumplen con los filtros aplicados,
        /// ordenadas por fecha de inicio.
        /// </returns>
        public async Task<IEnumerable<Cita>> ObtenerFiltrado(FiltroCitasDTO filtroCitasDTO)
        {
            var queryable = context.Citas
                                .Include(x => x.Paciente)
                                .Include(x => x.Dentista)
                                .Include(x => x.Consultorio)
                                .AsQueryable();

            if (filtroCitasDTO.ConsultorioId is not null)
            {
                queryable = queryable.Where(x => x.ConsultorioId == filtroCitasDTO.ConsultorioId);
            }

            if (filtroCitasDTO.DentistaId is not null)
            {
                queryable = queryable.Where(x => x.DentistaId == filtroCitasDTO.DentistaId);
            }

            if (filtroCitasDTO.PacienteId is not null)
            {
                queryable = queryable.Where(x => x.PacienteId == filtroCitasDTO.PacienteId);
            }

            if (filtroCitasDTO.EstadoCita is not null)
            {
                queryable = queryable.Where(x => x.Estado == filtroCitasDTO.EstadoCita);
            }

            return await queryable.Where(x => x.IntervaloDeTiempo.Inicio >= filtroCitasDTO.FechaInicio
            && x.IntervaloDeTiempo.Fin < filtroCitasDTO.FechaFin)
                .OrderBy(x => x.IntervaloDeTiempo.Inicio)
                .ToListAsync();

        }

        /// <summary>
        /// Obtiene una cita por su identificador, incluyendo la información
        /// del paciente, dentista y consultorio relacionados.
        /// </summary>
        /// <param name="id">
        /// Identificador único de la cita que se desea consultar.
        /// </param>
        /// <returns>
        /// La cita encontrada con sus entidades relacionadas o null
        /// si no existe una cita con el identificador proporcionado.
        /// </returns>
        new public async Task<Cita?> ObtenerPorId(Guid id)
        {
            return await context.Citas
                .Include(x => x.Paciente)
                .Include(x => x.Dentista)
                .Include(x => x.Consultorio)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
