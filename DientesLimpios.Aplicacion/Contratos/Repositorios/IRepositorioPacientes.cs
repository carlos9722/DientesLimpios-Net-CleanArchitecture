using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Contratos.Repositorios
{
    /// <summary>
    /// Define el contrato para las operaciones de acceso a datos
    /// relacionadas con la entidad Paciente.
    /// </summary>
    /// <remarks>
    /// Hereda las operaciones genéricas de IRepositorio para permitir
    /// la gestión de pacientes sin depender de una implementación específica
    /// de persistencia.
    /// </remarks>
    public interface IRepositorioPacientes : IRepositorio<Paciente>
    {
    }
}
