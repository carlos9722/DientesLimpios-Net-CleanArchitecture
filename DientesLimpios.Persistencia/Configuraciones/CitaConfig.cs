
using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Configuraciones
{
    /// <summary>
    /// Configura el mapeo de la entidad Cita en la base de datos
    /// mediante Entity Framework Core.
    /// </summary>
    public class CitaConfig : IEntityTypeConfiguration<Cita>
    {
        /// <summary>
        /// Define la configuración de las propiedades de la entidad Cita,
        /// incluyendo el mapeo de su intervalo de tiempo a las columnas correspondientes.
        /// </summary>
        /// <param name="builder">
        /// Constructor utilizado para configurar el mapeo de la entidad Cita.
        /// </param>
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            builder.ComplexProperty(prop => prop.IntervaloDeTiempo, accion =>
            {
                accion.Property(e => e.Inicio).HasColumnName("FechaInicio");
                accion.Property(e => e.Fin).HasColumnName("FechaFin");
            });
        }
    }
}
