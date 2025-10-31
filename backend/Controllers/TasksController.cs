using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Models;
using TaskManager.Api.Services;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repo;

        public TasksController(ITaskRepository repo)
        {
            _repo = repo;
        }

        // GET /api/tasks
        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> Get()
        {
            return Ok(_repo.GetAll());
        }

        // POST /api/tasks
        [HttpPost]
        public ActionResult<TaskItem> Post([FromBody] TaskItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Description))
                return BadRequest("Description is required.");

            var created = _repo.Create(new TaskItem
            {
                Description = item.Description,
                IsCompleted = item.IsCompleted
            });

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT /api/tasks/{id}
        [HttpPut("{id:guid}")]
        public IActionResult Put(Guid id, [FromBody] TaskItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Description))
                return BadRequest("Description is required.");

            var success = _repo.Update(id, item);
            if (!success) return NotFound();
            return NoContent();
        }

        // DELETE /api/tasks/{id}
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var success = _repo.Delete(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
