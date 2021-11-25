using AutoMapper;
using Dwh.Data;
using Dwh.Domain.Commands;
using Dwh.Domain.Models;
using Dwh.Domain.Objects;
using Dwh.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dwh.Core.Services
{
    public class FactService : IFactService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public FactService(ApplicationContext context,
                           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateFactCommand command)
        {
            Fact fact = _mapper.Map<Fact>(command);

            await _context.Facts.AddAsync(fact);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(EditFactCommand command)
        {
            Fact fact = await _context.Facts
                                      .FirstOrDefaultAsync(x => x.Id == command.Fact.Id);

            fact.Name = command.Fact.Name;
            fact.Order = command.Fact.Order;
            fact.IsActive = command.Fact.IsActive;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ViewFactModel>> GetAsync(GetFactsQuery query)
        {
            return await _mapper.ProjectTo<ViewFactModel>(_context.Facts
                                                                  .AsNoTracking()
                                                                  .Where(x => (query.ActiveFacts == null || x.IsActive == query.ActiveFacts.Value))
                                                                  .OrderBy(x => x.Order)
                                                                  )
                                .ToListAsync();
        }

        public async Task<ViewFactModel> GetAsync(Guid id)
        {
            var fact = await _context.Facts
                                      .AsNoTracking()
                                      .FirstOrDefaultAsync(x => x.Id == id);

            return new ViewFactModel
            {
                Id = fact.Id,
                Name = fact.Name,
                IsActive = fact.IsActive,
                Order = fact.Order
            };

        }
    }
}
