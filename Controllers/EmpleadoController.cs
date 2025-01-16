using Microsoft.AspNetCore.Mvc;
using SolucionesResidenciales.Domain.Entities;
using SolucionesResidenciales.Infrastructure.Repository;

namespace SolucionesRecidencialesApi.Controllers
{
    [Route("api/empleados")]
    [ApiController]
    public class EmpleadoController : GenericController<Empleado>
    {
        public EmpleadoController(IRepository<Empleado> repository) : base(repository)
        {
        }
    }
}
