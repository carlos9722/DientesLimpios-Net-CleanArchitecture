using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Comandos.CrearConsultorio
{
    /// <summary>
    /// Define las reglas de validación para el comando de creación de consultorios.
    /// </summary>
    public class ValidadorComandoCrearConsultorio : AbstractValidator<ComandoCrearConsultorio>
    {
        public ValidadorComandoCrearConsultorio()
        {
            // Exige que el nombre tenga un valor distinto de nulo, vacío o espacios en blanco.
            RuleFor(p => p.Nombre)
                .NotEmpty()
                .WithMessage("El campo {PropertyName} es requerido");
        }
    }
}

