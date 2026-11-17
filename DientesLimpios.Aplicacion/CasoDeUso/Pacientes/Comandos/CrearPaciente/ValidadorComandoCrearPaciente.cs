using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente
{
    /// <summary>
    /// Define las reglas de validación para el comando de creación de pacientes.
    /// </summary>
    /// <remarks>
    /// Utiliza FluentValidation para comprobar que los datos recibidos
    /// cumplan los requisitos establecidos antes de ejecutar el caso de uso.
    /// </remarks>
    public class ValidadorComandoCrearPaciente : AbstractValidator<ComandoCrearPaciente>
    {
        /// <summary>
        /// Inicializa el validador y establece las reglas que deben cumplir
        /// las propiedades del comando de creación de pacientes.
        /// </summary>
        public ValidadorComandoCrearPaciente()
        {
            // Valida que el nombre sea obligatorio y no supere los 250 caracteres.
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .MaximumLength(250).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}");

            // Valida que el correo electrónico sea obligatorio,
            // no supere los 254 caracteres y tenga un formato válido.
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .MaximumLength(254).WithMessage("La longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}")
                .EmailAddress().WithMessage("El formato del email no es válido");

        }
    }
}
