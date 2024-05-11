using Data;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace csharp_crud_api.Controllers;

[ApiController]
[Route("lists/{listId}/tasks")]
[Produces("application/json")]
[Consumes("application/json")]
public class TasksController : MyControllerBase
{
  private readonly ApplicationDbContext _applicationContext;
  public TasksController(ApplicationDbContext applicationContext)
  {
    _applicationContext = applicationContext;
  }

  private bool IsListByIdExists(string listId)
  {
    return _applicationContext.Lists.Any(e => e.Id == listId);
  }

  private bool IsTaskByIdExists(string id)
  {
    return _applicationContext.Tasks.Any(e => e.Id == id);
  }

  [HttpGet(Name = "Get tasks")]
  [ProducesResponseType(typeof(List<ResponseTaskDto>), 200)]
  [ProducesResponseType(404)]
  [ProducesResponseType(500)]
  public async Task<ActionResult<List<ResponseTaskDto>>> GetTasks([FromRoute] string listId)
  {
    if (!IsListByIdExists(listId))
    {
      ModelState.AddModelError("List not found", "A list with this Id not found.");

      return ValidationProblem(ModelState);
    }

    var tasks = await _applicationContext.Tasks.Where(t => t.ListId == listId).Select(task => new ResponseTaskDto
    {
      Id = task.Id,
      Title = task.Title,
      Description = task.Description,
      DueAt = task.DueAt,
      Priority = task.Priority,
      CreatedAt = task.CreatedAt,
      UpdatedAt = task.UpdatedAt,
      ListId = task.ListId
    }).OrderByDescending(task => task.CreatedAt).ToListAsync();

    return tasks;
  }

  [HttpGet("{id}", Name = "Get task")]
  [ProducesResponseType(typeof(ResponseTaskDto), 200)]
  [ProducesResponseType(404)]
  [ProducesResponseType(500)]
  public async Task<ActionResult<ResponseTaskDto>> GetTask([FromRoute] string listId, [FromRoute] string id)
  {
    if (!IsListByIdExists(listId))
    {
      ModelState.AddModelError("List not found", "A list with this Id not found.");

      return ValidationProblem(ModelState);
    }

    var task = await _applicationContext.Tasks.Where(t => t.Id == id && t.ListId == listId).Select(task => new ResponseTaskDto
    {
      Id = task.Id,
      Title = task.Title,
      Description = task.Description,
      DueAt = task.DueAt,
      Priority = task.Priority,
      CreatedAt = task.CreatedAt,
      UpdatedAt = task.UpdatedAt,
      ListId = task.ListId
    }).FirstOrDefaultAsync();

    if (task == null)
    {
      return NotFound();
    }

    return task;
  }

  [HttpPost(Name = "Create task")]
  [ProducesResponseType(typeof(ResponseTaskDto), 201)]
  [ProducesResponseType(400)]
  [ProducesResponseType(500)]
  public async Task<ActionResult<ResponseTaskDto>> CreateTask([FromRoute] string listId, [FromBody] CreateTaskDto body)
  {
    if (!IsListByIdExists(listId))
    {
      ModelState.AddModelError("List not found", "A list with this Id not found.");

      return ValidationProblem(ModelState);
    }

    var task = new Models.Task
    {
      Id = Guid.NewGuid().ToString(),
      Title = body.Title,
      Description = body.Description,
      DueAt = body.DueAt,
      Priority = body.Priority,
      ListId = listId,
      CreatedAt = DateTime.UtcNow,
      UpdatedAt = DateTime.UtcNow
    };

    _applicationContext.Tasks.Add(task);

    await _applicationContext.SaveChangesAsync();

    var response = _applicationContext.Tasks.Where(t => t.Id == task.Id && t.ListId == listId).Select(task => new ResponseTaskDto
    {
      Id = task.Id,
      Title = task.Title,
      Description = task.Description,
      DueAt = task.DueAt,
      Priority = task.Priority,
      CreatedAt = task.CreatedAt,
      UpdatedAt = task.UpdatedAt,
      ListId = task.ListId
    }).FirstOrDefault();

    return CreatedAtAction(nameof(CreateTask), new { id = task.Id }, response);
  }

  [HttpPatch("{id}", Name = "Edit task")]
  [ProducesResponseType(typeof(ResponseTaskDto), 200)]
  [ProducesResponseType(400)]
  [ProducesResponseType(404)]
  [ProducesResponseType(500)]
  public async Task<ActionResult<ResponseTaskDto>> PatchTask([FromRoute] string listId, [FromRoute] string id, [FromBody] PatchTaskDto body)
  {
    if (!IsListByIdExists(listId))
    {
      ModelState.AddModelError("List not found", "A list with this Id not found.");

      return ValidationProblem(ModelState);
    }

    if (body.ListId != null)
    {
      if (!IsListByIdExists(body.ListId))
      {
        ModelState.AddModelError("List not found", "A new list with this Id not found.");

        return ValidationProblem(ModelState);
      }
    }

    var task = await _applicationContext.Tasks.Where(t => t.Id == id && t.ListId == listId).FirstOrDefaultAsync();

    if (task == null)
    {
      return NotFound();
    }

    task.ListId = body.ListId ?? task.ListId;
    task.UpdatedAt = DateTime.UtcNow;

    foreach (var property in typeof(PatchTaskDto).GetProperties())
    {

      var newValue = property.GetValue(body);

      if (newValue != null)
      {
        var taskProperty = task.GetType().GetProperty(property.Name);

        if (property.Name != "DueAt" && taskProperty != null && taskProperty.CanWrite)
        {
          taskProperty.SetValue(task, newValue);
        }
        else if (property.Name == "DueAt" && taskProperty != null && taskProperty.CanWrite)
        {
          taskProperty.SetValue(task, body.DueAt?.Year == 1 ? task.DueAt : body.DueAt);
        }
      }
    }

    _applicationContext.Entry(task).Property(x => x.CreatedAt).IsModified = false;

    try
    {
      await _applicationContext.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!IsTaskByIdExists(id))
      {
        return NotFound();
      }
      else
      {
        throw;
      }
    }

    var searchListId = body.ListId ?? listId;

    var response = _applicationContext.Tasks.Where(t => t.Id == id && t.ListId == searchListId).Select(task => new ResponseTaskDto
    {
      Id = task.Id,
      Title = task.Title,
      Description = task.Description,
      DueAt = task.DueAt,
      Priority = task.Priority,
      CreatedAt = task.CreatedAt,
      UpdatedAt = task.UpdatedAt,
      ListId = task.ListId
    }).FirstOrDefault();

    return UpdatedAtAction(nameof(PatchTask), new { id = task.Id }, response!);
  }

  [HttpDelete("{id}", Name = "Delete task")]
  [ProducesResponseType(typeof(void), 204)]
  [ProducesResponseType(400)]
  [ProducesResponseType(500)]
  public async Task<IActionResult> DeleteTask([FromRoute] string listId, [FromRoute] string id)
  {
    if (!IsListByIdExists(listId))
    {
      ModelState.AddModelError("List not found", "A list with this Id not found.");

      return ValidationProblem(ModelState);
    }

    var task = await _applicationContext.Tasks.Where(t => t.Id == id && t.ListId == listId).FirstOrDefaultAsync();

    if (task == null)
    {
      return NotFound();
    }

    _applicationContext.Tasks.Remove(task);

    await _applicationContext.SaveChangesAsync();

    return NoContent();
  }
}