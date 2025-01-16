using Microsoft.AspNetCore.Mvc;
using SolucionesResidenciales.Domain.Entities;
using SolucionesResidenciales.Infrastructure.Repository;

namespace SolucionesRecidencialesApi.Controllers
{
    [Route("api/vehiculos")]
    [ApiController]
    public class VehiculoController : GenericController<Vehiculo>
    {
        public VehiculoController(IRepository<Vehiculo> repository) : base(repository)
        {
        }
    }
}
