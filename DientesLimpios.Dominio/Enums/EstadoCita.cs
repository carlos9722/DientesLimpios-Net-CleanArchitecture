using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Dominio.Enums
{
    /// <summary>
    /// Define los estados posibles de una cita odontológica.
    /// </summary>
    public enum EstadoCita
    {
        Programada = 1,  // La cita está agendada y pendiente de atención.
        Cancelada = 2,   // La cita fue cancelada.
        Completada = 3   // La atención de la cita finalizó.
    }
}
