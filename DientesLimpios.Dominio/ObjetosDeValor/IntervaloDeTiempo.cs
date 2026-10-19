using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Text;

using DientesLimpios.Dominio.Excepciones;

namespace DientesLimpios.Dominio.ObjetosDeValor
{
    /// <summary>
    /// Representa un intervalo de tiempo con una fecha de inicio y otra de fin.
    /// </summary>
    public record IntervaloDeTiempo
    {
        public DateTime Inicio { get; }
        public DateTime Fin { get; }

        /// <summary>
        /// Crea un intervalo validando que el inicio sea anterior al fin.
        /// </summary>
        /// <param name="inicio">Fecha y hora de inicio.</param>
        /// <param name="fin">Fecha y hora de finalización.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza si el inicio es posterior o igual al fin.
        /// </exception>
        public IntervaloDeTiempo(DateTime inicio, DateTime fin)
        {
            // Garantiza que el intervalo tenga una duración positiva.
            if (inicio >= fin)
            {
                throw new ExcepcionDeReglaDeNegocio(
                    "La hora de inicio debe ser anterior a la hora de fin.");
            }

            Inicio = inicio;
            Fin = fin;
        }
    }
}