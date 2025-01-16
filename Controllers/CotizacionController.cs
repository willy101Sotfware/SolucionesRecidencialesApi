using Microsoft.AspNetCore.Mvc;
using SolucionesResidenciales.Domain.Entities;
using SolucionesResidenciales.Infrastructure.Repository;

namespace SolucionesRecidencialesApi.Controllers
{
    [Route("api/cotizaciones")]
    [ApiController]
    public class CotizacionController : GenericController<Cotizacion>
    {
        public CotizacionController(IRepository<Cotizacion> repository) : base(repository)
        {
        }
    }
}
