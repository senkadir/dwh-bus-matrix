using Dwh.Data;
using Dwh.Domain.Commands;
using Dwh.Domain.Models;
using Dwh.Domain.Objects;
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
                Order = command.Order,
                IsActive = command.IsActive,
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

        public async Task EditAsync(EditFactCommand command)
        {
            Fact fact = await _context.Facts
                                      .FirstOrDefaultAsync(x => x.Id == command.Fact.Id);

            fact.Name = command.Fact.Name;
            fact.Order = command.Fact.Order;
            fact.IsActive = command.Fact.IsActive;
            fact.Dimensions = (command.SelectedDimensions ??= new())
                                     .Select(x => new FactDimension
                                     {
                                         Id = Guid.NewGuid(),
                                         DimensionId = x,
                                         FactId = fact.Id
                                     })
                                     .ToList();


            var factDimensions = await _context.FactDimension
                                               .Where(x => x.FactId == fact.Id)
                                               .ToListAsync();

            _context.RemoveRange(factDimensions);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ViewFactModel>> GetAsync()
        {
            var facts = await _context.Facts
                                      .AsNoTracking()
                                      .Include(x => x.Dimensions)
                                      .ThenInclude(x => x.Dimension)
                                      .ToListAsync();

            return facts.Select(x => new ViewFactModel
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive,
                Order = x.Order,
                Dimensions = x.Dimensions.Select(y => new ViewDimensionModel
                {
                    Id = y.Dimension.Id,
                    Name = y.Dimension.Name,
                    Order = y.Dimension.Order,
                    IsActive = y.Dimension.IsActive
                })
            });
        }

        public async Task<ViewFactModel> GetAsync(Guid id)
        {
            var fact = await _context.Facts
                                      .AsNoTracking()
                                      .Include(x => x.Dimensions)
                                      .ThenInclude(x => x.Dimension)
                                      .FirstOrDefaultAsync(x => x.Id == id);

            return new ViewFactModel
            {
                Id = fact.Id,
                Name = fact.Name,
                IsActive = fact.IsActive,
                Order = fact.Order,
                Dimensions = fact.Dimensions.Select(y => new ViewDimensionModel
                {
                    Id = y.Dimension.Id,
                    Name = y.Dimension.Name,
                    Order = y.Dimension.Order,
                    IsActive = y.Dimension.IsActive
                })
            };

        }
    }
}
