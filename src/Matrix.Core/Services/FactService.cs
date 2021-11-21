using Matrix.Data;
using Matrix.Domain.Commands;
using Matrix.Domain.Models;
using Matrix.Domain.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matrix.Core.Services
{
    public class FactService : IFactService
    {
        private readonly ApplicationContext _context;

        public FactService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateFactCommand command)
        {
            Guid id = Guid.NewGuid();

            Fact fact = new()
            {
                Id = id,
                Name = command.Name,
                Dimensions = (command.SelectedDimensions ??= new())
                                    .Select(x =>
                                        new FactDimension
                                        {
                                            Id = Guid.NewGuid(),
                                            DimensionId = x,
                                            FactId = id
                                        })
                                    .ToList()
            };

            await _context.Facts.AddAsync(fact);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ViewFactModel>> GetAsync()
        {
            var facts = await _context.Facts
                                      .Include(x => x.Dimensions)
                                      .ThenInclude(x => x.Dimension)
                                      .ToListAsync();

            return facts.Select(x => new ViewFactModel
            {
                Id = x.Id,
                Name = x.Name,
                Dimensions = x.Dimensions.Select(y => new ViewDimensionModel
                {
                    Id = y.Dimension.Id,
                    Name = y.Dimension.Name
                })
            });
        }
    }
}
