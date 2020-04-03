using SecuritySystem.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Systems = SecuritySystem.Domain.Entities.System;

namespace SecuritySystem.Repositories.Interfaces
{
    public interface ISystemRepository
    {
        Task CreateAsync(Systems system);
        Task UpdateAsync(Systems system);
        Task<SystemDTO> GetAllAsync(int page);
        Task<SystemDTO> GetAllWithFilterAsync(string description, string initials, string email, int page);
        Task<Systems> GetByIdAsync(int id);
    }
}
