using SecuritySystem.Repositories.Context;
using SecuritySystem.Repositories.Interfaces;
using System;
using System.Linq;
using Systems = SecuritySystem.Domain.Entities.System;
using System.Collections.Generic;
using SecuritySystem.Domain.DTO;
using Microsoft.EntityFrameworkCore;

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

        public void Create(Systems system)
        {
            system.Initials = system.Initials.ToUpper();
            _ctx.Systems.Add(system);
            _ctx.SaveChanges();
        }

        public Systems GetById(int id)
        {
            return _ctx.Systems.AsNoTracking().FirstOrDefault(c => c.Id == id);
        }

        public SystemDTO GetAll(int page)
        {
            return new SystemDTO()
            {
                TotalResults = _ctx.Systems.AsNoTracking().Count(),
                Page = page,
                Result = _ctx.Systems.AsNoTracking().Skip(50 * (page - 1)).Take(50),
            };
        }

        public SystemDTO GetAllWithFilter(string description, string initials, string email, int page)
        {
            int numeroItens = 0;

            numeroItens = (from c in _ctx.Systems.AsNoTracking()
                           where c.Description.Contains(description ?? "") &&
                                 c.Initials.Contains(initials ?? "") &&
                                 c.Email.Contains(email ?? "")
                          select c).Count();

            return new SystemDTO()
            {
                TotalResults = numeroItens,
                Page = page,
                Result = (from c in _ctx.Systems.AsNoTracking()
                          where c.Description.Contains(description ?? "") &&
                                c.Initials.Contains(initials ?? "") &&
                                c.Email.Contains(email ?? "")
                         select c).Skip(50 * (page - 1)).Take(50)
            };
        }

        public void Update(Systems system)
        {
            system.Initials = system.Initials.ToUpper();
            system.JustificationForTheLastUpdate = system.NewJustification;
            system.NewJustification = null;

            _ctx.Systems.Update(system);
            _ctx.SaveChanges();
        }
    }
}
