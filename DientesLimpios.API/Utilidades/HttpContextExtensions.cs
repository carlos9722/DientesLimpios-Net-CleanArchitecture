namespace DientesLimpios.API.Utilidades
{
    /// <summary>
    /// Proporciona métodos de extensión para trabajar con el contexto HTTP.
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Agrega a las cabeceras de la respuesta HTTP la cantidad total de registros.
        /// </summary>
        /// <param name="httpContext">Contexto HTTP de la solicitud actual.</param>
        /// <param name="cantidadTotalRegistros">Número total de registros disponibles.</param>
        public static void InsertarPaginacionEnCabecera(this HttpContext httpContext, int cantidadTotalRegistros)
        {
            httpContext.Response.Headers.Append("cantidad-total-registros", cantidadTotalRegistros.ToString());
        }
    }
}