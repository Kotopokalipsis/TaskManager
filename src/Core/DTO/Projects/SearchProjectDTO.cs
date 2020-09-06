#nullable enable
using System;

namespace Core.DTO.Projects
{
    public class SearchProjectDTO
    {
        public string? SearchQuery { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PriorityLow { get; set; }
        public int? PriorityHigh { get; set; }
        public int? PriorityNormal { get; set; }
    }
}