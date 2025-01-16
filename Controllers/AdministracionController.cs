using Microsoft.AspNetCore.Mvc;
using SolucionesResidenciales.Domain.Entities;
using SolucionesResidenciales.Infrastructure.Repository;

namespace SolucionesRecidencialesApi.Controllers
{
    [Route("api/administraciones")]
    [ApiController]
    public class AdministracionController : GenericController<Administracion>
    {
        public AdministracionController(IRepository<Administracion> repository) : base(repository)
        {
        }
    }
}
