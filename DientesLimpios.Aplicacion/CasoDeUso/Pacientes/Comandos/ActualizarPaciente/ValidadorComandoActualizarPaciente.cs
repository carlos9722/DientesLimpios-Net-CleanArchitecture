
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Comandos.ActualizarPaciente
{
    /// <summary>
    /// Define las reglas de validación para el comando encargado
    /// de actualizar la información de un paciente.
    /// </summary>
    /// <remarks>
    /// Utiliza FluentValidation para verificar que los datos recibidos
    /// cumplan los requisitos establecidos antes de ejecutar el caso de uso.
    /// </remarks>
    public class ValidadorComandoActualizarPaciente : AbstractValidator<ComandoActualizarPaciente>
    {
        /// <summary>
        /// Inicializa las reglas de validación del comando de actualización de pacientes.
        /// </summary>
        public ValidadorComandoActualizarPaciente()
        {
            // Valida que el nombre sea obligatorio y no supere los 250 caracteres.
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .MaximumLength(250).WithMessage("La lontigud del campo {PropertyName} debe ser menor o igual a {MaxLength}");

            // Valida que el correo sea obligatorio, tenga una longitud máxima
            // de 254 caracteres y cumpla con un formato de email válido.
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .MaximumLength(254).WithMessage("La lontigud del campo {PropertyName} debe ser menor o igual a {MaxLength}")
                .EmailAddress().WithMessage("El formato del email no es válido");

        }
    }
}
