using Microsoft.AspNetCore.Mvc;
using SolucionesResidenciales.Infrastructure.Repository;
using SolucionesResidenciales.Domain; // Asegúrate de incluir el espacio de nombres correcto para IEntity

namespace SolucionesRecidencialesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenericController<T> : ControllerBase where T : class, IEntity // Asegúrate de que T implemente IEntity
{
    private readonly IRepository<T> _repository;

    public GenericController(IRepository<T> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<T>>> GetAll()
    {
        var result = await _repository.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<T>> GetById(int id)
    {
        var result = await _repository.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<T>> Create([FromBody] T entity)
    {
        var result = await _repository.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] T entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }

        await _repository.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
