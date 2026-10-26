using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Persistencia.Configuraciones
{
    /// <summary>
    /// Define la configuración de persistencia de la entidad Consultorio.
    /// </summary>
    /// <remarks>
    /// Centraliza las reglas de mapeo de la entidad Consultorio hacia la base
    /// de datos mediante la API Fluent de Entity Framework Core.
    /// </remarks>
    public class ConsultorioConfig : IEntityTypeConfiguration<Consultorio>
    {
        /// <summary>
        /// Configura las propiedades y restricciones de la entidad Consultorio.
        /// </summary>
        /// <param name="builder">
        /// Objeto que permite definir cómo se mapea la entidad Consultorio
        /// y sus propiedades en la base de datos.
        /// </param>
        public void Configure(EntityTypeBuilder<Consultorio> builder)
        {
            // Configura la propiedad Nombre para que tenga una longitud máxima
            // de 150 caracteres y sea obligatoria en la base de datos.
            builder.Property(prop => prop.Nombre)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
