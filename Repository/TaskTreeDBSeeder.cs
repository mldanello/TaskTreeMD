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
                    Console.WriteLine($"Id: {p.Id} Fn: {p.FirstName} Ln: {p.LastName}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ({nameof(TaskTreeDBSeeder)}: " + ex.Message);
                throw;
            }

            //// TreeTask
            //var treeTask = GetTreeTasks();
            //db.TreeTask.AddRange(treeTask);
            //try
            //{
            //    int numAffected = await db.SaveChangesAsync();
            //    _logger.LogInformation(@"Saved {numAffected} treeTasks");
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Error in ({nameof(TaskTreeDBSeeder)}: " + ex.Message);
            //    throw;
            //}

            //// Activity
            //var activity = GetActivity();
            //db.TreeTask.AddRange(activity);
            //try
            //{
            //    int numAffected = await db.SaveChangesAsync();
            //    _logger.LogInformation(@"Saved {numAffected} activity");
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Error in ({nameof(TaskTreeDBSeeder)}: " + ex.Message);
            //    throw;
            //}
        }

        private List<Person> GetPersons()
        {
            var persons = new List<Person>();
            persons.Add(new Person { FirstName = "Donald", LastName = "Duck" });
            persons.Add(new Person { FirstName = "Micheal", LastName = "Mouse" });
            persons.Add(new Person { FirstName = "Samual", LastName = "Snead" });
            persons.Add(new Person { FirstName = "Linda", LastName = "Jones" });
            persons.Add(new Person { FirstName = "Nancy", LastName = "Smith" });
            return persons;
        }



    }
}
