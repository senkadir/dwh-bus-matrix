using Dwh.Common.Core;
using Dwh.Domain.Commands;
using Dwh.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dwh.Core.Services
{
    public interface IDimensionService : IServiceBase
    {
        Task CreateAsync(CreateDimensionCommand command);

        Task<List<ViewDimensionModel>> GetAsync();

        Task<ViewDimensionModel> GetAsync(Guid id);

        Task EditAsync(EditDimensionCommand model);
    }
}
