using Microsoft.EntityFrameworkCore;
using System.Linq;
using TaskTreeMD.Models;

namespace TaskTreeMD.Repository
{
    public class TaskTreeDBSeeder
    {
        readonly ILogger _logger;

        public TaskTreeDBSeeder(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TaskTreeDBSeeder>();
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            // Derived from Dan Wahlin example at: https://github.com/DanWahlin/AngularCLI-ASPNET-Core-CustomersService
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var taskTreeDB = serviceScope.ServiceProvider.GetService<TaskTreeDBContext>();
                if (await taskTreeDB.Database.EnsureCreatedAsync())
                {
                    if (!await taskTreeDB.TreeTask.AnyAsync())
                    {
                        await InsertTaskTreeSampleData(taskTreeDB);
                    }
                }
            }

        }

        public async Task InsertTaskTreeSampleData(TaskTreeDBContext db)
        {
            // Person
            var persons = GetPersons();
            db.Person.AddRange(persons);
            try
            {
                int numAffected = await db.SaveChangesAsync();
                _logger.LogInformation($"Saved {numAffected} persons");
                var readPersons = await db.Person.Select(x => x).ToListAsync();  
                foreach(var p in readPersons)
                {
                    _logger.LogInformation($"Person - Id: {p.Id} Fn: {p.FirstName} Ln: {p.LastName}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ({nameof(TaskTreeDBSeeder)}: " + ex.Message);
                throw;
            }

            // TreeTask
            var treeTasks = GetTreeTasks(persons);
            db.TreeTask.AddRange(treeTasks);
            try
            {
                int numAffected = await db.SaveChangesAsync();
                _logger.LogInformation($"Saved {numAffected} treeTasks");
                var readTreeTasks = await db.TreeTask.Select(x => x).ToListAsync();
                foreach (var p in readTreeTasks)
                {
                    _logger.LogInformation($"Task - Id: {p.Id} Title: {p.Title}");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ({nameof(TaskTreeDBSeeder)}: " + ex.Message);
                throw;
            }

            // Activity
            var activity = GetActivity();
            db.Activity.AddRange(activity);
            try
            {
                int numAffected = await db.SaveChangesAsync();
                _logger.LogInformation(@"Saved {numAffected} activity");
                var readActivities = await db.Activity.Select(x => x).ToListAsync();
                foreach (var p in readActivities)
                {
                    _logger.LogInformation($"Activity - Id: {p.Id} Desc: {p.Description}");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ({nameof(TaskTreeDBSeeder)}: " + ex.Message);
                throw;
            }
        }

        private List<Person> GetPersons()
        {
            var persons = new List<Person>();
            persons.Add(new Person { FirstName = "Donald", LastName = "Duck" });
            persons.Add(new Person { FirstName = "Micheal", LastName = "Mouse" });
            persons.Add(new Person { FirstName = "Samual", LastName = "Snead" });
            persons.Add(new Person { FirstName = "Linda", LastName = "Jones" });
            persons.Add(new Person { FirstName = "Nancy", LastName = "Smith" });
            persons.Add(new Person { FirstName = "Mike", LastName = "Magical" });
            return persons;
        }

        private List<TreeTask> GetTreeTasks(List<Person> persons)
        {
            var treeTasks = new List<TreeTask>();
            treeTasks.Add(new TreeTask { Title = "There To Here", SubTitle = "Record Project", AssignedTo = persons[0] });
            treeTasks.Add(new TreeTask { Title = "Tree Task MD", SubTitle = "Sample Code Project", AssignedTo = persons[1] });
            treeTasks.Add(new TreeTask { Title = "Have To Say", SubTitle = "Song", ParentId = 1 });
            treeTasks.Add(new TreeTask { Title = "Classical Guitar", SubTitle="Tracking", ParentId = 3, AssignedTo = persons[5] });
            treeTasks.Add(new TreeTask { Title = "Electric Guitar", SubTitle = "Tracking", ParentId = 3, AssignedTo = persons[5] });
            return treeTasks;
        }

        private List<Activity> GetActivity()
        {
            var activities = new List<Activity>();
            activities.Add(new Activity { TaskId = 1, Description = "Ordered new Cornerstone Vibe" });
            activities.Add(new Activity { TaskId = 1, Description = "Updated Tracking Document" });
            activities.Add(new Activity { TaskId = 2, Description = "Created Git Project Repo" });
            activities.Add(new Activity { TaskId = 2, Description = "Created Visual Studio Project" });
            activities.Add(new Activity { TaskId = 2, Description = "Add Entity Models" });
            return activities;
        }
    }
}
