using Microsoft.EntityFrameworkCore;
using TaskTreeMD.Models;

namespace TaskTreeMD.Repository
{
    public class TaskTreeDBContext : DbContext
    {
        public DbSet<TreeTask> TreeTask { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Activity> Activity { get; set; }

        // TODO: Lookup in the Ang-Core integration class , how he handled the 
        // non-nullable property must contain non-null value error ....
        public TaskTreeDBContext(DbContextOptions<TaskTreeDBContext> options) : base(options) { }
    }
}
