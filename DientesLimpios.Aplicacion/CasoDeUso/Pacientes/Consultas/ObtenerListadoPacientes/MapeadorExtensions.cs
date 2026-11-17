using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    /// <summary>
    /// Proporciona métodos de extensión para convertir entidades del dominio
    /// en objetos de transferencia de datos utilizados en las consultas de pacientes.
    /// </summary>
    /// <remarks>
    /// Centraliza la transformación de las entidades en sus respectivos DTO,
    /// evitando repetir la asignación de propiedades en los casos de uso.
    /// </remarks>
    public static class MapeadorExtensions
    {
        /// <summary>
        /// Convierte una entidad Paciente en un objeto PacienteListadoDTO.
        /// </summary>
        /// <param name="paciente">
        /// Entidad del dominio que contiene la información del paciente.
        /// </param>
        /// <returns>
        /// Un objeto PacienteListadoDTO con el identificador, nombre
        /// y correo electrónico del paciente.
        /// </returns>
        public static PacienteListadoDTO ADto(this Paciente paciente)
        {
            // Construye el DTO utilizando los datos de la entidad del dominio.
            var dto = new PacienteListadoDTO
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,

                // Obtiene el valor del correo electrónico desde su objeto de valor.
                Email = paciente.Email.Valor
            };

            // Retorna el DTO preparado para ser utilizado en la respuesta de la consulta.
            return dto;
        }
    }

}
