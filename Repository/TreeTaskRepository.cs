using Microsoft.EntityFrameworkCore;
using TaskTreeMD.Models;

namespace TaskTreeMD.Repository
{
    public class TreeTaskRepository : ITreeTaskRepository
    {
        private readonly TaskTreeDBContext _Context;
        private readonly ILogger _Logger;

        public TreeTaskRepository(TaskTreeDBContext context, ILoggerFactory loggerFactory )
        {
            _Context = context;
            _Logger = loggerFactory.CreateLogger<TreeTaskRepository>();
        }

        public async Task<List<TreeTask>> GetTreeTasksAsync()
        {
            try
            {
                return await _Context.TreeTask.OrderBy(t => t.Id)
                                              .ToListAsync();
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Error in {nameof(GetTreeTasksAsync)}: " + ex.Message);
                throw;
            }
        }
        public async Task<TreeTask> GetTreeTaskAsync(int id)
        {
            try
            {
                return await _Context.TreeTask
                                     .SingleOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Error in {nameof(GetTreeTaskAsync)}: " + ex.Message);
                throw;
            }

        }

        public Task<TreeTask> InsertTreeTaskAsync(TreeTask treeTask)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTreeTaskAsync(TreeTask treeTask)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteTreeTaskAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
