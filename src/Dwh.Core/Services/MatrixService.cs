using AutoMapper;
using Dwh.Common.Exceptions;
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
    public class MatrixService : IMatrixService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public MatrixService(ApplicationContext context,
                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ViewMatrixModel>> GetAsync()
        {
            return await _mapper.ProjectTo<ViewMatrixModel>(_context.Matrixes
                                                                    .AsNoTracking())
                                .ToListAsync();
        }

        public async Task CreateAsync(CreateMatrixCommand command)
        {
            Matrix matrix = new()
            {
                Id = Guid.NewGuid(),
                Name = command.Name
            };

            await _context.Matrixes.AddAsync(matrix);

            await _context.SaveChangesAsync();
        }

        public async Task<ViewMatrixModel> GetAsync(GetMatrixQuery query)
        {
            var matrixItemsModel = await _mapper.ProjectTo<ViewMatrixItemModel>(_context.MatrixItems
                                                                                        .AsNoTracking()
                                                                                        .Include(x => x.Dimension)
                                                                                        .Include(x => x.Fact)
                                                                                        .Where(x => (query.ActiveDimensions == null || x.Dimension.IsActive == query.ActiveDimensions.Value)
                                                                                                 && (query.ActiveFacts == null || x.Fact.IsActive == query.ActiveFacts.Value)
                                                                                                 && x.MatrixId == query.MatrixId))
                                                .ToListAsync();

            var matrixModel = await _mapper.ProjectTo<ViewMatrixModel>(_context.Matrixes
                                                                               .AsNoTracking()
                                                                               .Where(x => x.Id == query.MatrixId))
                                           .FirstOrDefaultAsync();

            matrixModel.Items = matrixItemsModel;

            return matrixModel;
        }

        public async Task CreateAsync(CreateMatrixItemCommand command)
        {
            var exists = await _context.MatrixItems
                                       .AnyAsync(x => x.MatrixId == command.MatrixId && x.DimensionId == command.DimensionId && x.FactId == command.FactId);

            if (exists)
            {
                throw new DomainException(errorCode: "matrix_item_already_exists", error: "Item already exists");
            }

            MatrixItem item = new()
            {
                Id = Guid.NewGuid(),
                DimensionId = command.DimensionId,
                FactId = command.FactId,
                MatrixId = command.MatrixId
            };

            await _context.MatrixItems.AddAsync(item);

            await _context.SaveChangesAsync();
        }
    }
}
