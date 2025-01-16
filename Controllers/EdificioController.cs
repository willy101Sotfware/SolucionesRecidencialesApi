using Microsoft.AspNetCore.Mvc;
using SolucionesResidenciales.Domain.Entities;
using SolucionesResidenciales.Infrastructure.Repository;

namespace SolucionesRecidencialesApi.Controllers
{
    [Route("api/edificios")]
    [ApiController]
    public class EdificioController : GenericController<Edificio>
    {
        public EdificioController(IRepository<Edificio> repository) : base(repository)
        {
        }
    }
}
