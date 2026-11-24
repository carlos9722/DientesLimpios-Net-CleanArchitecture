
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.Consultas.ObtenerDetalleCita
{
    /// <summary>
    /// Proporciona métodos de extensión para convertir entidades Cita
    /// en objetos de transferencia de datos.
    /// </summary>
    public static class MapeadorExtensions
    {
        /// <summary>
        /// Convierte una entidad Cita en un DTO con la información
        /// necesaria para mostrar su detalle.
        /// </summary>
        /// <param name="cita">
        /// Entidad de cita que contiene la información que se desea convertir.
        /// </param>
        /// <returns>
        /// DTO con los datos de la cita, incluyendo fechas, consultorio,
        /// dentista, paciente y estado.
        /// </returns>
        public static CitaDetalleDTO ADto(this Cita cita)
        {
            var dto = new CitaDetalleDTO
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
