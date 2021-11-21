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
                Name = command.Name
            };

            await _context.Dimensions.AddAsync(dimension);

            await _context.SaveChangesAsync();
        }

        public async Task<List<ViewDimensionModel>> GetAsync()
        {
            return await _context.Dimensions
                                 .Select(x => new ViewDimensionModel
                                 {
                                     Id = x.Id,
                                     Name = x.Name
                                 })
                                 .ToListAsync();
        }
    }
}
