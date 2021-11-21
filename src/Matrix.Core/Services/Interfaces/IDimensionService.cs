﻿using Matrix.Common.Core;
using Matrix.Domain.Commands.Dimensions;
using Matrix.Domain.Models.Dimensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Core.Services
{
    public interface IDimensionService : IServiceBase
    {
        Task CreateAsync(CreateDimensionCommand command);

        Task<List<ViewDimensionModel>> GetAsync();
    }
}
