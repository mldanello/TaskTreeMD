namespace TaskTreeMD.Models
{
    public class TreeTask
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string SubTitle { get; set; }
        public string Description { get; set; } = string.Empty;

        public int ParentId { get; set; }       // Self Reference
        public string? Notes { get; set; }
        public Person? AssignedTo { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime DueDate { get; set; }

        public ICollection<Activity> Activities { get; set; }
    }
}
