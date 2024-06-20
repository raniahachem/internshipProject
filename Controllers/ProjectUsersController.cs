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
    public class ProjectUsersController : ControllerBase
    {
        private readonly ProjectTrackerContext _context;

        public ProjectUsersController(ProjectTrackerContext context)
        {
            _context = context;
        }

        // GET: api/projectusers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectUserDTO>>> GetProjectUsers()
        {
            var projectUsersEntities = await _context.ProjectUsers
                .Include(pu => pu.Project) // Inclure l'entité Project liée
                .Include(pu => pu.User)    // Inclure l'entité User liée
                .ToListAsync();

            if (projectUsersEntities == null)
            {
                return NotFound();
            }

            var projectUsersDTO = projectUsersEntities.Select(pu => new ProjectUserDTO
            {
                ProjectId = pu.ProjectId,
                ProjectName = pu.Project.Name,
                ProjectDescription = pu.Project.Description,
                ProjectStartDate = pu.Project.StartDate,
                ProjectEndDate = pu.Project.EndDate,
                UserId = pu.UserId,
                Username = pu.User.Username,
                Email = pu.User.Email
            }).ToList();

            return projectUsersDTO;
        }


        // GET: api/projectusers/{projectId}/{userId}
        [HttpGet("{projectId}/{userId}")]
        public async Task<ActionResult<ProjectUserDTO>> GetProjectUser(int projectId, int userId)
        {
            var projectUserEntity = await _context.ProjectUsers
                .Include(pu => pu.Project)
                .Include(pu => pu.User)
                .FirstOrDefaultAsync(pu => pu.ProjectId == projectId && pu.UserId == userId);

            if (projectUserEntity == null)
            {
                return NotFound();
            }

            var projectUserDTO = new ProjectUserDTO
            {
                ProjectId = projectUserEntity.ProjectId,
                ProjectName = projectUserEntity.Project.Name,
                ProjectDescription = projectUserEntity.Project.Description,
                ProjectStartDate = projectUserEntity.Project.StartDate,
                ProjectEndDate = projectUserEntity.Project.EndDate,
                UserId = projectUserEntity.UserId,
                Username = projectUserEntity.User.Username,
                Email = projectUserEntity.User.Email
            };

            return projectUserDTO;
        }


        // POST: api/projectusers
        [HttpPost]
        public async Task<ActionResult<ProjectUser>> PostProjectUser(ProjectUserCreateDTO projectUserCreateDTO)
        {
            var projectUser = new ProjectUser
            {
                ProjectId = projectUserCreateDTO.ProjectId,
                UserId = projectUserCreateDTO.UserId,
                Project = await _context.Projects.FindAsync(projectUserCreateDTO.ProjectId),
                User = await _context.Users.FindAsync(projectUserCreateDTO.UserId)
            };

            if (projectUser.Project == null || projectUser.User == null)
            {
                return BadRequest("Invalid ProjectId or UserId");
            }

            _context.ProjectUsers.Add(projectUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjectUser), new { projectId = projectUser.ProjectId, userId = projectUser.UserId }, projectUser);
        }



        // DELETE: api/projectusers/5
        [HttpDelete("{projectId}/{userId}")]
        public async Task<IActionResult> DeleteProjectUser(int projectId, int userId)
        {
            var projectUser = await _context.ProjectUsers.FindAsync(projectId, userId);
            if (projectUser == null)
            {
                return NotFound();
            }

            _context.ProjectUsers.Remove(projectUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
