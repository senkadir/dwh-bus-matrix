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
    public class DimensionService : IDimensionService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public DimensionService(ApplicationContext context,
                                IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateDimensionCommand command)
        {
            Dimension dimension = _mapper.Map<Dimension>(command);

            await _context.Dimensions.AddAsync(dimension);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(EditDimensionCommand model)
        {
            Dimension dimension = await _context.Dimensions
                                                .FirstOrDefaultAsync(x => x.Id == model.Id);

            dimension.Name = model.Name;
            dimension.Order = model.Order;
            dimension.IsActive = model.IsActive;

            await _context.SaveChangesAsync();
        }

        public async Task<List<ViewDimensionModel>> GetAsync(GetDimensionsQuery query)
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
                                 .Where(x => (query.ActiveDimensions == null || x.IsActive == query.ActiveDimensions.Value))
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
