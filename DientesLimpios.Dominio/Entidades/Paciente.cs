using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Dominio.Entidades
{
    /// <summary>
    /// Representa a un paciente dentro del dominio del negocio.
    /// </summary>
    public class Paciente
    {
        // El identificador y los datos se modifican únicamente desde la propia entidad.
        public Guid Id { get; private set; }
        /// Indica que el valor se asignará después, evitando advertencias del compilador.
        public string Nombre { get; private set; } = null!;
        /// Indica que el valor se asignará después, evitando advertencias del compilador.
        public Email Email { get; private set; } = null!;

        /// <summary>
        /// Crea un paciente validando los datos obligatorios del negocio.
        /// </summary>
        /// <param name="nombre">Nombre del paciente.</param>
        /// <param name="email">Correo electrónico del paciente.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza si el nombre está vacío o el correo es nulo.
        /// </exception>
        public Paciente(string nombre, Email email)
        {
            // El nombre es obligatorio y no puede contener solo espacios.
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio");
            }

            // El correo debe existir para crear un paciente válido.
            if (email is null)
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} es obligatorio");
            }

            // Genera un identificador único y asigna los datos validados.
            Id = Guid.CreateVersion7();
            Nombre = nombre;
            Email = email;
        }
    }
}