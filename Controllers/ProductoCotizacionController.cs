using Microsoft.AspNetCore.Mvc;
using SolucionesResidenciales.Domain.Entities;
using SolucionesResidenciales.Infrastructure.Repository;

namespace SolucionesRecidencialesApi.Controllers;

[Route("api/productos-cotizacion")]
[ApiController]
public class ProductoCotizacionController : ControllerBase
{
    private readonly ProductoCotizacionRepository _repository;

    public ProductoCotizacionController(ProductoCotizacionRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductoCotizacion>>> GetAll()
    {
        var result = await _repository.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{idProducto}/{idCotizacion}")]
    public async Task<ActionResult<ProductoCotizacion>> GetById(int idProducto, int idCotizacion)
    {
        var result = await _repository.GetByIdAsync(idProducto, idCotizacion);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProductoCotizacion>> Create([FromBody] ProductoCotizacion entity)
    {
        var result = await _repository.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { idProducto = result.IdProducto, idCotizacion = result.IdCotizacion }, result);
    }

    [HttpPut("{idProducto}/{idCotizacion}")]
    public async Task<IActionResult> Update(int idProducto, int idCotizacion, [FromBody] ProductoCotizacion entity)
    {
        if (idProducto != entity.IdProducto || idCotizacion != entity.IdCotizacion)
        {
            return BadRequest();
        }

        await _repository.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{idProducto}/{idCotizacion}")]
    public async Task<IActionResult> Delete(int idProducto, int idCotizacion)
    {
        await _repository.DeleteAsync(idProducto, idCotizacion);
        return NoContent();
    }
}
