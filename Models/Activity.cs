namespace TaskTreeMD.Models
{
    public class Activity 
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
