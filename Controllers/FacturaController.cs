using Microsoft.AspNetCore.Mvc;
using SolucionesResidenciales.Domain.Entities;
using SolucionesResidenciales.Infrastructure.Repository;

namespace SolucionesRecidencialesApi.Controllers
{
    [Route("api/facturas")]
    [ApiController]
    public class FacturaController : GenericController<Factura>
    {
        public FacturaController(IRepository<Factura> repository) : base(repository)
        {
        }
    }
}
