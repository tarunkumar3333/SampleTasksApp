using TaksAPI.Utility;

namespace TaksAPI.Model
{
    /// <summary>
    /// The model class for sample task.
    /// </summary>
    public class SampleTask
    {
        /// <summary>
        /// The identifier.
        /// </summary>
        public Guid? Id { get; set; } // Make id nullable

        /// <summary>
        /// constrcutor for sample task.
        /// </summary>
        public SampleTask()
        {
            if (Id == null)
            {
                Id = Guid.NewGuid();
            }
        }

        /// <summary>
        /// The title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description of task.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The status of task.
        /// </summary>
        public SampleTaskStatus Status { get; set; }

    }
}
