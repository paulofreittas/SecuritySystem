using System;
using System.Collections.Generic;
using System.Text;
using Systems = SecuritySystem.Domain.Entities.System;

namespace SecuritySystem.Domain.DTO
{
    // Objeto responsavel pela transferência de informações para o frontend de forma mais eficaz e objetiva.
    // DTO - Data Transfer Object - Objeto de transferência de dados
    public class SystemDTO
    {
        public int TotalResults { get; set; }
        public int Page { get; set; }
        public IEnumerable<Systems> Result { get; set; }
    }
}
