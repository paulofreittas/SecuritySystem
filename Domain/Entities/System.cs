using SecuritySystem.Domain.Enums;
using System;

namespace SecuritySystem.Domain.Entities
{
    public class System
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Initials { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public Status Status { get; set; }
        public string UserResponsibleForLastUpdate { get; set; }
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public string JustificationForTheLastUpdate { get; set; }
        public string NewJustification { get; set; }
    }
}
