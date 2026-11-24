
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.Consultas.ObtenerListadoCitas
{
    /// <summary>
    /// Proporciona métodos de extensión para convertir entidades Cita
    /// en objetos de transferencia de datos.
    /// </summary>
    public static class MapeadorExtensions
    {
        /// <summary>
        /// Convierte una entidad Cita en un DTO con la información
        /// necesaria para mostrarla en el listado de citas.
        /// </summary>
        /// <param name="cita">
        /// Entidad de cita que contiene la información que se desea convertir.
        /// </param>
        /// <returns>
        /// DTO con el identificador, fechas, consultorio, dentista,
        /// paciente y estado de la cita.
        /// </returns>
        public static CitaListadoDTO ADto(this Cita cita)
        {
            var dto = new CitaListadoDTO
            {
                Id = cita.Id,
                FechaInicio = cita.IntervaloDeTiempo.Inicio,
                FechaFin = cita.IntervaloDeTiempo.Fin,
                Consultorio = cita.Consultorio!.Nombre,
                Dentista = cita.Dentista!.Nombre,
                Paciente = cita.Paciente!.Nombre,
                EstadoCita = cita.Estado.ToString()
            };
            return dto;
        }

    }
}
