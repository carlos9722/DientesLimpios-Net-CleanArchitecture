using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente
{
    /// <summary>
    /// Representa la solicitud para crear un nuevo paciente en la aplicación.
    /// </summary>
    /// <remarks>
    /// Contiene los datos necesarios para ejecutar el caso de uso de creación
    /// de pacientes y define un identificador Guid como resultado de la operación.
    /// </remarks>
    public class ComandoCrearPaciente : IRequest<Guid>
    {
        /// <summary>
        /// Obtiene o establece el nombre del paciente que se desea registrar.
        /// </summary>
        public required string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece el correo electrónico del paciente que se desea registrar.
        /// </summary>
        public required string Email { get; set; }
    }
}
