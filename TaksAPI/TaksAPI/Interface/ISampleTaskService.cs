using TaksAPI.Model;

namespace TaksAPI.Interface
{
    /// <summary>
    /// The interface for sample task service.
    /// </summary>
    public interface ISampleTaskService
    {
        Task<IEnumerable<SampleTask>> GetSampleTasksAsync();
        Task<SampleTask> GetSampleTaskAsync(Guid id);
        Task<SampleTask> AddSampleTaskAsync(SampleTask task);
        Task<bool> UpdateSampleTaskAsync(SampleTask task);
        Task<bool> DeleteSampleTaskAsync(Guid id);
    }
}
