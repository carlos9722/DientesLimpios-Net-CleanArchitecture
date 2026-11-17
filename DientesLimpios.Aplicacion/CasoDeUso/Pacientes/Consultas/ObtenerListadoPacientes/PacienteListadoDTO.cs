using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    /// <summary>
    /// Representa los datos de un paciente que se muestran
    /// al consultar el listado de pacientes registrados.
    /// </summary>
    /// <remarks>
    /// Contiene únicamente la información necesaria para presentar
    /// cada paciente en los resultados de la consulta, evitando
    /// exponer directamente la entidad del dominio.
    /// </remarks>
    public class PacienteListadoDTO
    {
        /// <summary>
        /// Obtiene o establece el identificador único del paciente.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del paciente.
        /// </summary>
        public required string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece el correo electrónico del paciente.
        /// </summary>
        public required string Email { get; set; }
    }
}
