﻿using Matrix.Common.Core;
using Matrix.Domain.Commands;
using Matrix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Core.Services
{
    public interface IDimensionService : IServiceBase
    {
        Task CreateAsync(CreateDimensionCommand command);

        Task<List<ViewDimensionModel>> GetAsync();

        Task<ViewDimensionModel> GetAsync(Guid id);

        Task EditAsync(EditDimensionModel model);
    }
}
