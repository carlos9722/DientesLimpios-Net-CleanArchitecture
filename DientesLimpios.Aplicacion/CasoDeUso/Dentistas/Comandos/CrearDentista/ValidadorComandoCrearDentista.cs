
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.CrearDentista
{
    /// <summary>
    /// Define las reglas de validación para el comando de creación de un dentista.
    /// </summary>
    /// <remarks>
    /// Utiliza FluentValidation para validar que el nombre y el correo electrónico
    /// cumplan con los requisitos establecidos antes de ejecutar el caso de uso.
    /// </remarks>
    public class ValidadorComandoCrearDentista : AbstractValidator<ComandoCrearDentista>
    {
        /// <summary>
        /// Inicializa el validador y establece las reglas de validación del comando.
        /// </summary>
        /// <remarks>
        /// Verifica que el nombre y el correo electrónico sean obligatorios,
        /// respeten la longitud máxima permitida y que el correo tenga un formato válido.
        /// </remarks>
        public ValidadorComandoCrearDentista()
        {
            // Valida que el nombre sea obligatorio y no supere los 250 caracteres.
            RuleFor(p => p.Nombre)
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .MaximumLength(250).WithMessage("La lontigud del campo {PropertyName} debe ser menor o igual a {MaxLength}");

            // Valida que el correo electrónico sea obligatorio, no supere los 254 caracteres
            // y tenga una estructura de dirección de correo electrónico válida.
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .MaximumLength(254).WithMessage("La lontigud del campo {PropertyName} debe ser menor o igual a {MaxLength}")
                .EmailAddress().WithMessage("El formato del email no es válido");
        }
    }
}
