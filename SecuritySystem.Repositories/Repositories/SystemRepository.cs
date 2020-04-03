using SecuritySystem.Repositories.Context;
using SecuritySystem.Repositories.Interfaces;
using System;
using System.Linq;
using Systems = SecuritySystem.Domain.Entities.System;
using System.Collections.Generic;
using SecuritySystem.Domain.DTO;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SecuritySystem.Repositories.Repositories
{
    // Responsável por fazer as interações com o banco de dados.
    public class SystemRepository : ISystemRepository
    {
        private readonly SSContext _ctx;

        public SystemRepository(SSContext ctx)
        {
            _ctx = ctx;
        }

        public async Task CreateAsync(Systems system)
        {
            system.Initials = system.Initials.ToUpper();
            _ctx.Systems.Add(system);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Systems> GetByIdAsync(int id)
        {
            return await _ctx.Systems.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<SystemDTO> GetAllAsync(int page)
        {
            return new SystemDTO()
            {
                TotalResults = await _ctx.Systems.AsNoTracking().CountAsync(),
                Page = page,
                Result = await _ctx.Systems.AsNoTracking().Skip(50 * (page - 1)).Take(50).ToListAsync(),
            };
        }

        public async Task<SystemDTO> GetAllWithFilterAsync(string description, string initials, string email, int page)
        {
            int numeroItens = 0;

            numeroItens = await (from c in _ctx.Systems.AsNoTracking()
                           where c.Description.Contains(description ?? "") &&
                                 c.Initials.Contains(initials ?? "") &&
                                 c.Email.Contains(email ?? "")
                          select c).CountAsync();

            return new SystemDTO()
            {
                TotalResults = numeroItens,
                Page = page,
                Result = await (from c in _ctx.Systems.AsNoTracking()
                          where c.Description.Contains(description ?? "") &&
                                c.Initials.Contains(initials ?? "") &&
                                c.Email.Contains(email ?? "")
                          select c).Skip(50 * (page - 1)).Take(50).ToListAsync()
            };
        }

        public async Task UpdateAsync(Systems system)
        {
            system.Initials = system.Initials.ToUpper();
            system.JustificationForTheLastUpdate = system.NewJustification;
            system.NewJustification = null;

            //_ctx.Entry<Systems>(system).State = EntityState.Modified;
            _ctx.Systems.Update(system);
            await _ctx.SaveChangesAsync();
        }
    }
}
