using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTrackerAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

        public List<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
    }
}
