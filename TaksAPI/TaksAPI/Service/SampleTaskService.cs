using TaksAPI.Interface;
using TaksAPI.Model;
using TaksAPI.Utility;
using TasksAPI.Utility;

namespace TaksAPI.Service
{
    /// <summary>
    /// Service layer for Sample task.
    /// </summary>
    public class SampleTaskService : ISampleTaskService
    {
        private readonly ILogger<SampleTaskService> _logger;
        private static readonly List<SampleTask> _sampleTasks = new List<SampleTask>();

        public SampleTaskService(ILogger<SampleTaskService> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<SampleTask>> GetSampleTasksAsync()
        {
            return await Task.FromResult(_sampleTasks);
        }

        /// <summary>
        /// GetSampleTaskAsync.
        /// </summary>
        /// <param name="sampleTaskId"></param>
        /// <returns></returns>
        public async Task<SampleTask> GetSampleTaskAsync(Guid sampleTaskId)
        {
            /// get and return teh first matching task based on input task id.
            var task = _sampleTasks.FirstOrDefault(t => t.Id == sampleTaskId);

            // return back if no task found with given id.
            if (task == null)
            {
                _logger.LogWarning($" {TaskConstants.NoTaskAvailableWithGivenID} {sampleTaskId}");
                return null;
            }
            return await Task.FromResult(task);
        }

        /// <summary>
        /// AddSampleTaskAsync
        /// </summary>
        /// <param name="task">the inout task.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<SampleTask> AddSampleTaskAsync(SampleTask task)
        {
            // return back if inout is null
            if (task == null)
            {
                _logger.LogError($"{TaskConstants.NullTask}");
                throw new ArgumentNullException(nameof(task));
            }

            // update new task as Created
            task.Status = SampleTaskStatus.Created;

            _sampleTasks.Add(task);

            _logger.LogInformation($"{TaskConstants.TaskAddeddSuccessfully} {task.Id}");
            return await Task.FromResult(task);
        }

        /// <summary>
        /// UpdateSampleTaskAsync.
        /// </summary>
        /// <param name="task">the sample input task.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> UpdateSampleTaskAsync(SampleTask task)
        {
            // return back if input is invalid.
            if (task == null)
            {
                _logger.LogError($"{TaskConstants.NullTask}");
                throw new ArgumentNullException(nameof(task));
            }

            var existingTask = _sampleTasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask == null)
            {
                _logger.LogWarning($"{TaskConstants.NoTaskAvailableWithGivenID} {task.Id}");
                return false;
            }

            // update the existing task if found.
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;

            _logger.LogInformation($"{TaskConstants.TaskUpdatedSuccessfully} {task.Id}");
            return true;
        }

        /// <summary>
        /// DeleteSampleTaskAsync.
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <returns></returns>
        public async Task<bool> DeleteSampleTaskAsync(Guid id)
        {
            var task = _sampleTasks.FirstOrDefault(t => t.Id == id);

            // return back if no task exist with given task id.
            if (task == null)
            {
                _logger.LogWarning($"{TaskConstants.NoTaskAvailableWithGivenID} {id}");
                return false;
            }

            // delete teh task from the list
            _sampleTasks.Remove(task);
            _logger.LogInformation($"{TaskConstants.TaskDeletedSuccessfully}: {id}");
            return true;
        }
    }
}
