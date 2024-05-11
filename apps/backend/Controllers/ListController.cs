using Data;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dto;
namespace csharp_crud_api.Controllers;

[ApiController]
[Route("lists")]
[Produces("application/json")]
[Consumes("application/json")]
public class ListsController : MyControllerBase
{
  private readonly ApplicationDbContext _applicationContext;

  public ListsController(ApplicationDbContext applicationContext)
  {
    _applicationContext = applicationContext;
  }

  public bool IsListByIdExists(string id)
  {
    return _applicationContext.Lists.Any(e => e.Id == id);
  }

  [HttpGet(Name = "Get lists")]
  [ProducesResponseType(typeof(List<ResponseListDto>), 200)]
  [ProducesResponseType(400)]
  [ProducesResponseType(500)]
  public async Task<ActionResult<List<ResponseListDto>>> GetLists()
  {
    var lists = await _applicationContext.Lists.Select(list => new ResponseListDto
    {
      Id = list.Id,
      Name = list.Name,
      CreatedAt = list.CreatedAt,
      UpdatedAt = list.UpdatedAt,
      Task = list.Tasks.Where(task => task.ListId == list.Id).Count()
    }).OrderByDescending(list => list.CreatedAt).ToListAsync();

    return lists;
  }

  [HttpGet("{id}", Name = "Get list")]
  [ProducesResponseType(typeof(ResponseListDto), 200)]
  [ProducesResponseType(404)]
  [ProducesResponseType(500)]
  public async Task<ActionResult<ResponseListDto>> GetList([FromRoute] string id)
  {
    var list = await _applicationContext.Lists.Select(list => new ResponseListDto
    {
      Id = list.Id,
      Name = list.Name,
      CreatedAt = list.CreatedAt,
      UpdatedAt = list.UpdatedAt,
      Task = list.Tasks.Where(task => task.ListId == list.Id).Count()
    }).FirstOrDefaultAsync();

    if (list == null)
    {
      return NotFound();
    }

    return list;
  }

  [HttpPost(Name = "Create list")]
  [ProducesResponseType(typeof(ResponseListDto), 201)]
  [ProducesResponseType(400)]
  [ProducesResponseType(500)]
  public async Task<ActionResult<ResponseListDto>> CreateList([FromBody] CreateListDto body)
  {
    var IsNameAlreadyTaken = _applicationContext.Lists.Any(l => l.Name.ToLower() == body.Name.ToLower());

    if (IsNameAlreadyTaken)
    {
      ModelState.AddModelError("Unique name", "A list by that name already exists.");

      return ValidationProblem(ModelState);
    }

    var list = new List
    {
      Id = Guid.NewGuid().ToString(),
      Name = body.Name,
      CreatedAt = DateTime.UtcNow,
      UpdatedAt = DateTime.UtcNow
    };

    _applicationContext.Lists.Add(list);

    await _applicationContext.SaveChangesAsync();

    var response = _applicationContext.Lists.Where(l => l.Id == list.Id).Select(list => new ResponseListDto
    {
      Id = list.Id,
      Name = list.Name,
      CreatedAt = list.CreatedAt,
      UpdatedAt = list.UpdatedAt,
      Task = 0
    }).FirstOrDefault();

    return CreatedAtAction(nameof(CreateList), new { id = list.Id }, response);
  }

  [HttpPatch("{id}", Name = "Edit list")]
  [ProducesResponseType(typeof(ResponseListDto), 200)]
  [ProducesResponseType(400)]
  [ProducesResponseType(404)]
  [ProducesResponseType(500)]
  public async Task<ActionResult<ResponseListDto>> PatchList([FromRoute] string id, [FromBody] PatchListDto body)
  {
    var list = await _applicationContext.Lists.FindAsync(id);

    if (list == null)
    {
      return NotFound();
    }

    if (list.Name == body.Name)
    {
      ModelState.AddModelError("Unique name", "New list name cannot be as the current name.");

      return ValidationProblem(ModelState);
    }

    list.Name = body.Name;
    list.UpdatedAt = DateTime.UtcNow;

    _applicationContext.Entry(list).Property(x => x.CreatedAt).IsModified = false;

    try
    {
      await _applicationContext.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!IsListByIdExists(id))
      {
        return NotFound();
      }
      else
      {
        throw;
      }
    }

    var response = _applicationContext.Lists.Where(l => l.Id == list.Id).Select(list => new ResponseListDto
    {
      Id = list.Id,
      Name = list.Name,
      CreatedAt = list.CreatedAt,
      UpdatedAt = list.UpdatedAt,
      Task = list.Tasks.Where(task => task.ListId == list.Id).Count()
    }).FirstOrDefault();

    return UpdatedAtAction(nameof(PatchList), new { id = list.Id }, response!);
  }

  [HttpDelete("{id}", Name = "Delete list")]
  [ProducesResponseType(typeof(void), 204)]
  [ProducesResponseType(400)]
  [ProducesResponseType(404)]
  [ProducesResponseType(500)]
  public async Task<IActionResult> DeleteList([FromRoute] string id)
  {
    var list = await _applicationContext.Lists.FindAsync(id);

    if (list == null)
    {
      return NotFound();
    }

    _applicationContext.Lists.Remove(list);

    await _applicationContext.SaveChangesAsync();

    return NoContent();
  }
}