using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTrackerAPI.Data;
using ProjectTrackerAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTasksController : ControllerBase
    {
        private readonly ProjectTrackerContext _context;

        public ProjectTasksController(ProjectTrackerContext context)
        {
            _context = context;
        }

        // GET: api/projecttasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetProjectTasks()
        {
            return await _context.ProjectTasks.ToListAsync();
        }

        // GET: api/projecttasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTask>> GetProjectTask(int id)
        {
            var projectTask = await _context.ProjectTasks.FindAsync(id);

            if (projectTask == null)
            {
                return NotFound();
            }

            return projectTask;
        }

        // POST: api/projecttasks
        [HttpPost]
        public async Task<ActionResult<ProjectTask>> PostProjectTask(ProjectTaskCreateDTO projectTaskCreateDTO)
        {
            var projectTask = new ProjectTask
            {
                Name = projectTaskCreateDTO.Name,
                Description = projectTaskCreateDTO.Description,
                DueDate = projectTaskCreateDTO.DueDate,
                IsCompleted = projectTaskCreateDTO.IsCompleted,
                ProjectId = projectTaskCreateDTO.ProjectId,
                UserId = projectTaskCreateDTO.UserId,
                Project = await _context.Projects.FindAsync(projectTaskCreateDTO.ProjectId),
                User = await _context.Users.FindAsync(projectTaskCreateDTO.UserId)
            };

            if (projectTask.Project == null || projectTask.User == null)
            {
                return BadRequest("Invalid ProjectId or UserId");
            }

            _context.ProjectTasks.Add(projectTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjectTask), new { id = projectTask.Id }, projectTask);
        }

        // PUT: api/projecttasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectTask(int id, ProjectTask projectTask)
        {
            if (id != projectTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/projecttasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTask(int id)
        {
            var projectTask = await _context.ProjectTasks.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }

            _context.ProjectTasks.Remove(projectTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectTaskExists(int id)
        {
            return _context.ProjectTasks.Any(e => e.Id == id);
        }
    }
}
