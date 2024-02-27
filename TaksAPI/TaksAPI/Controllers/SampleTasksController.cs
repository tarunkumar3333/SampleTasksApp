using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaksAPI.Interface;
using TaksAPI.Model;

namespace TaksAPI.Controllers
{
    /// <summary>
    /// SampleTasksController.
    /// </summary>
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SampleTasksController : ControllerBase
    {
        private readonly ISampleTaskService _sampleTaskService;

        /// <summary>
        /// constructore for SampleTasksController.
        /// </summary>
        /// <param name="sampleTaskService"></param>
        public SampleTasksController(ISampleTaskService sampleTaskService)
        {
            _sampleTaskService = sampleTaskService;
        }

        /// <summary>
        /// Get Sample Tasks.
        /// </summary>
        /// <returns>The sample tasks.</returns>
        [HttpGet("GetSampleTasks")]
        public async Task<ActionResult<IEnumerable<SampleTask>>> GetSampleTasks()
        {
            var tasks = await _sampleTaskService.GetSampleTasksAsync();
            return Ok(tasks);
        }

        /// <summary>
        /// Get sample task by id.
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <returns>The task.</returns>
        [HttpGet("GetSampleTaskById")]
        [ProducesResponseType(200, Type = typeof(SampleTask))]
        public async Task<ActionResult<SampleTask>> GetSampleTask(Guid sampleTaskId)
        {
            var task = await _sampleTaskService.GetSampleTaskAsync(sampleTaskId);

            // return back of no task found with input id.
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        /// <summary>
        /// AddSampleTask.
        /// </summary>
        /// <param name="task">The input task to be added.</param>
        /// <returns>The task for sample task.</returns>
        [HttpPost("AddSampleTask")]
        public async Task<ActionResult<SampleTask>> AddSampleTask(SampleTask task)
        {
            var addedTask = await _sampleTaskService.AddSampleTaskAsync(task);
            return CreatedAtAction(nameof(GetSampleTask), new { id = addedTask.Id }, addedTask);
        }

        /// <summary>
        /// Updated a task.
        /// </summary>
        /// <param name="task">The sample task to be updated.</param>
        /// <returns>The task.</returns>
        [HttpPut("UpdateTask")]
        public async Task<IActionResult> UpdateTask(SampleTask task)
        {
            // return back if input task is invalid
            if (task.Id == null || task.Id == Guid.Empty)
            {
                return BadRequest();
            }

            var updated = await _sampleTaskService.UpdateSampleTaskAsync(task);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Delete task by id.
        /// </summary>
        /// <param name="sampleTaskId">The sample task id to be deleted.</param>
        /// <returns>The task.</returns>
        [HttpDelete("DeleteTaskById")]
        public async Task<ActionResult> DeleteSampleTask(Guid sampleTaskId)
        {
            var deleted = await _sampleTaskService.DeleteSampleTaskAsync(sampleTaskId);
            if (deleted)
            {
                return NoContent();
            }
            else
            {
                // provided input do not exist in the system
                return NotFound();
            }
        }
    }
}
