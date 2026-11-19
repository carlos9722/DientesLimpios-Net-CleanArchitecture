
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ObtenerDetalleDentista
{
    /// <summary>
    /// Proporciona métodos de extensión para transformar entidades del dominio en DTO.
    /// </summary>
    /// <remarks>
    /// Centraliza la conversión de la entidad Dentista a DentistaDTO,
    /// permitiendo transferir información entre las capas de la aplicación
    /// sin exponer directamente la entidad del dominio.
    /// </remarks>
    public static class MapeadorExtensions
    {
        /// <summary>
        /// Convierte una entidad Dentista en un objeto de transferencia de datos (DTO).
        /// </summary>
        /// <param name="dentista">
        /// Entidad del dominio que contiene la información del dentista que se desea transformar.
        /// </param>
        /// <returns>
        /// Un objeto DentistaDTO con el identificador, nombre y correo electrónico del dentista.
        /// </returns>
        /// <remarks>
        /// Extrae los valores de la entidad y los asigna a las propiedades correspondientes
        /// del DTO. El correo electrónico se obtiene mediante la propiedad Valor del objeto de valor Email.
        /// </remarks>
        public static DentistaDTO ADto(this Dentista dentista)
        {
            // Crea el DTO asignando los datos correspondientes de la entidad Dentista.
            var dto = new DentistaDTO { Id = dentista.Id, Nombre = dentista.Nombre, Email = dentista.Email.Valor };

            // Devuelve el DTO con la información transformada.
            return dto;
        }
    }
}
