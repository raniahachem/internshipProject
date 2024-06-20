namespace ProjectTrackerAPI.Models
{
    public class ProjectUserDTO
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }

}
