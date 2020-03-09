using SecuritySystem.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Systems = SecuritySystem.Domain.Entities.System;

namespace SecuritySystem.Repositories.Interfaces
{
    public interface ISystemRepository
    {
        void Create(Systems system);
        void Update(Systems system);
        SystemDTO GetAll(int page);
        SystemDTO GetAllWithFilter(string description, string initials, string email, int page);
        Systems GetById(int id);
    }
}
