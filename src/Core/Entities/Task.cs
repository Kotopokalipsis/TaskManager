using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel;

namespace Core.Entities
{
    public class Task : BaseEntity
    {
        public string Name { get; set; }
        
        [Column(TypeName = "nvarchar(24)")]
        public StatusStates Status { get; set; }
        
        public string Comment { get; set; }
        public int Priority { get; set; }
        
        public int? AuthorId { get; set; }
        public int? PerformerId { get; set; }
        public Employer Author { get; set; }
        public Employer Performer { get; set; }

        public enum StatusStates
        {
            ToDo = 1,
            InProgress = 2,
            Done = 3
        }
    }
}