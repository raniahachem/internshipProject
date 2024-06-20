using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTrackerAPI.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du projet est requis.")]
        public string Name { get; set; }
                                                                               
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

        public List<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
    }
}
