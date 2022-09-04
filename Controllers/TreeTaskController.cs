using Microsoft.AspNetCore.Mvc;
using TaskTreeMD.Models;
using TaskTreeMD.Repository;

namespace TaskTreeMD.Controllers
{
    [Route("api/treetasks")]
    public class TreeTaskApiController : ControllerBase
    {
        ITreeTaskRepository _treeTaskRepository;
        ILogger _Logger;

        public TreeTaskApiController(ITreeTaskRepository treeTaskRepository, ILoggerFactory loggerFactory)
        {
            _treeTaskRepository = treeTaskRepository;
            _Logger = loggerFactory.CreateLogger<TreeTaskApiController>();
        }


        // GET api/treetasks
        [HttpGet]
        //[NoCache]  TODO: move over custom attribute to force no cache on the api call
        [ProducesResponseType(typeof(List<TreeTask>), 200)]  // Add Attributes for Swagger
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult<List<TreeTask>>> TreeTasks()
        {
            try
            {
                var treeTasks = await _treeTaskRepository.GetTreeTasksAsync();
                return Ok(treeTasks);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }
    }
}
