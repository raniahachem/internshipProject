using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectTrackerAPI.Models
{
    public class ProjectTask
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de la tâche est requis.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "La date d'échéance est requise.")]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        [Required(ErrorMessage = "Le ProjectId est requis.")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [Required(ErrorMessage = "Le UserId est requis.")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
