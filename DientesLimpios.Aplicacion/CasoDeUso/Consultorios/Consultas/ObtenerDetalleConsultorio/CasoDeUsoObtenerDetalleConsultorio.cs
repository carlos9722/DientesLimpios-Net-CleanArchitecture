using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio
{
    /// <summary>
    /// Gestiona la consulta del detalle de un consultorio por su identificador.
    /// </summary>
    public class CasoDeUsoObtenerDetalleConsultorio
        : IRequestHandler<ConsultaObtenerDetalleConsultorio, ConsultorioDetalleDTO>
    {
        private readonly IRepositorioConsultorios repositorio;

        /// <summary>
        /// Inicializa el caso de uso con el repositorio de consultorios.
        /// </summary>
        /// <param name="repositorio">Permite consultar los consultorios almacenados.</param>
        public CasoDeUsoObtenerDetalleConsultorio(IRepositorioConsultorios repositorio)
        {
            this.repositorio = repositorio;
        }

        /// <summary>
        /// Obtiene el detalle de un consultorio y lo transforma en un DTO.
        /// </summary>
        /// <param name="request">Contiene el identificador del consultorio que se desea consultar.</param>
        /// <returns>El detalle del consultorio encontrado.</returns>
        /// <exception cref="ExcepcionNoEncontrado">
        /// Se lanza cuando no existe un consultorio con el identificador solicitado.
        /// </exception>
        public async Task<ConsultorioDetalleDTO> Handle(ConsultaObtenerDetalleConsultorio request)
        {
            var consultorio = await repositorio.ObtenerPorId(request.Id);

            if (consultorio is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            // Convierte la entidad de dominio al DTO que consume la aplicación.
            return consultorio.ADto();
        }
    }
}

