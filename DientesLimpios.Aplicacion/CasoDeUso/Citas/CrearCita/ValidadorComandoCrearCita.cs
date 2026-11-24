
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.CrearCita
{
    /// <summary>
    /// Define las reglas de validación para el comando de creación de citas.
    /// </summary>
    public class ValidadorComandoCrearCita : AbstractValidator<ComandoCrearCita>
    {
        /// <summary>
        /// Inicializa las reglas para validar las fechas de inicio y finalización
        /// de una cita.
        /// </summary>
        public ValidadorComandoCrearCita()
        {
            RuleFor(x => x.FechaInicio)
            .GreaterThan(x => x.FechaFin).WithMessage("La fecha fin debe ser posterior a la fecha de inicio")
            .GreaterThan(DateTime.UtcNow).WithMessage("La fecha inicio no puede estar en el pasado");

        }
    }
}
