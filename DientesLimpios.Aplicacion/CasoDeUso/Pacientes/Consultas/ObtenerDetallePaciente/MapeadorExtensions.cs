
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerDetallePaciente
{
    /// <summary>
    /// Contiene métodos de extensión para transformar entidades del dominio
    /// en objetos de transferencia de datos (DTO).
    /// </summary>
    public static class MapeadorExtensions
    {
        /// <summary>
        /// Convierte una entidad Paciente en un objeto PacienteDetalleDTO.
        /// </summary>
        /// <param name="paciente">
        /// Entidad del dominio que contiene la información del paciente
        /// que se desea transformar.
        /// </param>
        /// <returns>
        /// Un objeto PacienteDetalleDTO con el identificador, nombre
        /// y correo electrónico del paciente.
        /// </returns>
        public static PacienteDetalleDTO ADto(this Paciente paciente)
        {
            // Crea el DTO asignando los valores correspondientes
            // desde la entidad del dominio.
            var dto = new PacienteDetalleDTO
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,

                // Obtiene el valor del correo electrónico desde su objeto de valor.
                Email = paciente.Email.Valor
            };

            // Retorna el DTO construido a partir de la entidad.
            return dto;
        }
    }
}
