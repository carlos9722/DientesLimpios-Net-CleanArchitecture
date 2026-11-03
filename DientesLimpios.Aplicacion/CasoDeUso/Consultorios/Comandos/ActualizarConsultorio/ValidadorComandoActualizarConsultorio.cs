
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Comandos.ActualizarConsultorio
{
    /// <summary>
    /// Define las reglas de validación para el comando encargado de actualizar
    /// la información de un consultorio.
    /// </summary>
    public class ValidadorComandoActualizarConsultorio : AbstractValidator<ComandoActualizarConsultorio>
    {
        /// <summary>
        /// Inicializa las reglas que debe cumplir el comando antes de ejecutar
        /// el proceso de actualización del consultorio.
        /// </summary>
        public ValidadorComandoActualizarConsultorio()
        {
            // Valida que el nombre sea obligatorio y no supere los 150 caracteres.
            RuleFor(p => p.Nombre)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .MaximumLength(150).WithMessage("La lontigud del campo {PropertyName} debe ser menor o igual a {MaxLength}");
        }

    }
}
