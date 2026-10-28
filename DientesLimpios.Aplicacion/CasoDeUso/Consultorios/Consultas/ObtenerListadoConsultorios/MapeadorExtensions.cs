
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerListadoConsultorios
{
    /// <summary>
    /// Contiene métodos de extensión para mapear entidades a DTOs.
    /// </summary>
    public static class MapeadorExtensions
    {
        /// <summary>
        /// Convierte una entidad Consultorio en un ConsultorioListadoDTO.
        /// </summary>
        /// <param name="consultorio">Entidad que se desea convertir.</param>
        /// <returns>DTO con el identificador y nombre del consultorio.</returns>
        public static ConsultorioListadoDTO ADto(this Consultorio consultorio)
        {
            var dto = new ConsultorioListadoDTO { Id = consultorio.Id, Nombre = consultorio.Nombre };
            return dto;
        }
    }
}
