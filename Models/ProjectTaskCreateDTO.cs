namespace ProjectTrackerAPI.Models
{
    public class ProjectTaskCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}
