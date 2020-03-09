using System;
using System.Collections.Generic;
using System.Text;
using Systems = SecuritySystem.Domain.Entities.System;

namespace SecuritySystem.Domain.DTO
{
    public class SystemDTO
    {
        public int TotalResults { get; set; }
        public int Page { get; set; }
        public IEnumerable<Systems> Result { get; set; }
    }
}
