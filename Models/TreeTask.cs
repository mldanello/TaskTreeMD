namespace TaskTreeMD.Models
{
    public class TreeTask
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public Person? AssignedTo { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
