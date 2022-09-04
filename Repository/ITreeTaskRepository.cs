using TaskTreeMD.Models;

namespace TaskTreeMD.Repository
{
    public interface ITreeTaskRepository
    {
        Task<List<TreeTask>> GetTreeTasksAsync();
        Task<TreeTask> GetTreeTaskAsync(int id);

        Task<TreeTask> InsertTreeTaskAsync(TreeTask treeTask);
        Task<bool> UpdateTreeTaskAsync(TreeTask treeTask);
        Task<bool> DeleteTreeTaskAsync(int id);

    }
}
