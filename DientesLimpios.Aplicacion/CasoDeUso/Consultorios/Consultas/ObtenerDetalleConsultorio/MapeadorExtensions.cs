using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio
{
    /// <summary>
    /// Proporciona métodos de extensión para convertir entidades en DTOs.
    /// </summary>
    public static class MapeadorExtensions
    {
        /// <summary>
        /// Convierte un consultorio en su DTO de detalle.
        /// </summary>
        /// <param name="consultorio">Entidad que se desea convertir.</param>
        /// <returns>Un DTO con el identificador y el nombre del consultorio.</returns>
        public static ConsultorioDetalleDTO ADto(this Consultorio consultorio)
        {
            var dto = new ConsultorioDetalleDTO
            {
                Id = consultorio.Id,
                Nombre = consultorio.Nombre
            };

            return dto;
        }
    }
}

