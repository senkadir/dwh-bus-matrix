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
    public class DimensionService : IDimensionService
    {
        private readonly ApplicationContext _context;

        public DimensionService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateDimensionCommand command)
        {
            Dimension dimension = new()
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                IsActive = command.IsActive,
                Order = command.Order
            };

            await _context.Dimensions.AddAsync(dimension);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(EditDimensionModel model)
        {
            Dimension dimension = await _context.Dimensions
                                                .FirstOrDefaultAsync(x => x.Id == model.Id);

            dimension.Name = model.Name;
            dimension.Order = model.Order;
            dimension.IsActive = model.IsActive;

            await _context.SaveChangesAsync();
        }

        public async Task<List<ViewDimensionModel>> GetAsync()
        {
            return await _context.Dimensions
                                 .AsNoTracking()
                                 .Select(x => new ViewDimensionModel
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     IsActive = x.IsActive,
                                     Order = x.Order
                                 })
                                 .OrderBy(x => x.Order)
                                 .ToListAsync();
        }

        public async Task<ViewDimensionModel> GetAsync(Guid id)
        {
            return await _context.Dimensions
                                 .AsNoTracking()
                                 .Select(x => new ViewDimensionModel
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     IsActive = x.IsActive,
                                     Order = x.Order
                                 })
                                 .Where(x => x.Id == id)
                                 .FirstOrDefaultAsync();
        }
    }
}
